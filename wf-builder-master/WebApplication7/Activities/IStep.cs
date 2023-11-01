using WebApplication7.Dtos;
using WebApplication7.Entities;
using WebApplication7.WorkflowDefninitions;

namespace WebApplication7.Activities
{
    public interface IWorkflowActivity
    {
        WfStep Step { get; }
        WfDesicion InCome { get; set; }
        List<IWorkflowActivity> NextSteps { get; set; }
        WfDesicion AvaliableDesicions { get; }
        WfDesicion AllowedInComes { get; }
        WfAuthor Authors { get; }
        void Executing(UserTask currentTask, StepPayloadBase? data = null);
        UserTask SendTask(Guid wfId, WfType workflowType, WfDesicion income)
        {
            if (AllowedInComes.HasFlag(income))
                return new UserTask
                {
                    AssignTo = Authors,
                    RequestId = wfId,
                    CurrentWorkflowStep = Step,
                    WorkflowType = workflowType,
                    AllowedDesicions = AvaliableDesicions
                };

            throw new Exception("this action is not avaliable!!");
        }
    }

    public static class StepExtensions
    {
        public static IWorkflowActivity StartWith<T>(this IWorkflowDefiniation wf)
            where T : IWorkflowActivity
        {
            var activity = Activator.CreateInstance<T>();
            activity.InCome = WfDesicion.Empty;

            if (activity == null)
                throw new Exception("Init activity is not null.");

            wf.InitActivity = activity;
            return activity;
        }

        public static IWorkflowActivity Then<T>(this IWorkflowActivity currentStep, WfDesicion desicion = WfDesicion.Empty)
            where T : IWorkflowActivity
        {
            var activity = Activator.CreateInstance<T>();
            activity.InCome = desicion;
            currentStep.NextSteps.Add(activity);
            return activity;
        }

        public static void EndWith<T>(this IWorkflowActivity currentStep, WfDesicion desicion = WfDesicion.Empty)
            where T : IWorkflowActivity
        {
            var activity = Activator.CreateInstance<T>();
            activity.InCome = desicion;
            currentStep.NextSteps.Add(activity);
        }

        public static IWorkflowActivity Branch(this IWorkflowActivity wf, WfDesicion desicion, Action<WfDesicion, IWorkflowActivity> branch = null)
        {
            if (branch != null) branch(desicion, wf);
            return wf;
        }
    }
}
