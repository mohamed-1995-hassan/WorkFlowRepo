using WebApplication7.Activities;
using WebApplication7.Activities.PrepareStudentWorkflow;
using WebApplication7.Entities;

namespace WebApplication7
{
    public interface IWorkflowDefiniation
    {
        WfType Type { get; }
        IWorkflowActivity? InitActivity { get; set; }
    }

    public class PrepareStudentWorkflowDefiniation : IWorkflowDefiniation
    {
        private static PrepareStudentWorkflowDefiniation _instance = null;
        public WfType Type { get => WfType.PrepareStudent; }
        public IWorkflowActivity InitActivity { get; set; } = null!;
        protected PrepareStudentWorkflowDefiniation()
        {
            this.StartWith<InitStep>()
                .Then<AddNewStudentReview1Step>()
                    .Branch(WfDesicion.Approve1,
                        (d, fl) =>
                        {
                            fl.Then<AddNewStudentReview2Step>(d)
                              .Branch(WfDesicion.Approve2,
                                (dd, ffl) =>
                                {
                                    ffl.EndWith<FinishedStep>(dd);
                                })
                              .Branch(WfDesicion.Reject2,
                                (dd, ffl) =>
                                {
                                    ffl.EndWith<FinishedStep>(dd);
                                });
                        })
                    .Branch(WfDesicion.Reject1,
                        (d, fl) =>
                        {
                            fl.EndWith<FinishedStep>(d);
                        });
        }
        public static PrepareStudentWorkflowDefiniation Build()
        {
            if (_instance == null)
                _instance = new PrepareStudentWorkflowDefiniation();

            return _instance;
        }
    }

    public static class WorkflowDefinitionExt
    {
        public static IWorkflowActivity GetNextStep(this IWorkflowDefiniation definiation, UserTask task = null, IWorkflowActivity activity = null, List<IWorkflowActivity> steps = null)
        {
            if (task == null)
                return definiation.InitActivity;

            if (activity?.Step == task.CurrentWorkflowStep)
                return activity;

            if (steps == null)
                steps = definiation.InitActivity.NextSteps;

            foreach (var step in steps)
            {
                var found = definiation.GetNextStep(task, step, step.NextSteps);

                if (found != null)
                {
                    var workflowActivity = found.NextSteps.FirstOrDefault(c => c.InCome == task.Desicion);

                    if (workflowActivity is null)
                        return found;

                    return workflowActivity;
                }
            }

            return default;
        }

        public static IWorkflowActivity GetCurrentStep(this IWorkflowDefiniation definiation, UserTask task = null, IWorkflowActivity activity = null, List<IWorkflowActivity> steps = null)
        {
            if (task == null)
                return definiation.InitActivity;

            if (activity?.Step == task.CurrentWorkflowStep)
                return activity;

            if (steps == null)
                steps = definiation.InitActivity.NextSteps;

            foreach (var step in steps)
            {
                var found = definiation.GetCurrentStep(task, step, step.NextSteps);

                if (found != null)
                {
                    return found;
                }
            }

            return default;
        }
    }


}
