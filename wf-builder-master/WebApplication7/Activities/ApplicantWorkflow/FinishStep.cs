using WebApplication7.Dtos;
using WebApplication7.Entities;
using WebApplication7.WorkflowDefninitions;

namespace WebApplication7.Activities.ApplicantWorkflow
{
    public class FinishStep : IWorkflowActivity
    {
        public WfStep Step => WfStep.Finished;
        public WfDesicion AvaliableDesicions => WfDesicion.Empty;
        public WfDesicion AllowedInComes => WfDesicion.Reject1 | WfDesicion.Reject2 | WfDesicion.Reject3 | WfDesicion.Approve3;
        public WfAuthor Authors { get; }
        public WfDesicion InCome { get; set; }
        public List<IWorkflowActivity> NextSteps { get; set; } = new();
        public void Executing(UserTask currentTask, StepPayloadBase? data = null)
        {

        }
        public UserTask SendTask(Guid wfId, WfType workflowType, WfDesicion income)
        {
            if (AllowedInComes.HasFlag(income))
                return new UserTask();

            throw new Exception("this action is not avaliable!!");
        }
    }
}
