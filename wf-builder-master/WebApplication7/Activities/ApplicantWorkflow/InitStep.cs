using WebApplication7.Dtos;
using WebApplication7.Entities;
using WebApplication7.WorkflowDefninitions;

namespace WebApplication7.Activities.ApplicantWorkflow
{
    public class InitStep : IWorkflowActivity
    {
        public WfStep Step => WfStep.Init;
        public WfDesicion AvaliableDesicions => WfDesicion.Init;
        public WfDesicion AllowedInComes => WfDesicion.Empty;
        public WfAuthor Authors { get; }
        public WfDesicion InCome { get; set; }
        public List<IWorkflowActivity> NextSteps { get; set; } = new();
        public void Executing(UserTask currentTask, StepPayloadBase? data = null)
        {

        }
        public UserTask SendTask(Guid wfId, WfType workflowType, WfDesicion income)
        {
            if (AllowedInComes.HasFlag(income))
                return new UserTask
                {
                    AssignTo = WfAuthor.NoOne,
                    RequestId = wfId,
                    CurrentWorkflowStep = WfStep.ApplicationReview1Step,
                    WorkflowType = workflowType,
                    AllowedDesicions = WfDesicion.Approve1 | WfDesicion.Reject1
                };

            throw new Exception("this action is not avaliable!!");
        }
    }
}
