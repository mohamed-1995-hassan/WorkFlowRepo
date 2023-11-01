using WebApplication7.Dtos;
using WebApplication7.Entities;
using WebApplication7.WorkflowDefninitions;

namespace WebApplication7.Activities.PrepareStudentWorkflow
{
    public class FinishedStep : IWorkflowActivity
    {
        public WfStep Step { get => WfStep.Finished; }
        public WfDesicion InCome { get; set; }
        public List<IWorkflowActivity> NextSteps { get; set; } = new();
        public WfDesicion AvaliableDesicions { get => WfDesicion.Empty; }
        public WfDesicion AllowedInComes { get => WfDesicion.Reject1 | WfDesicion.Reject2 | WfDesicion.Approve2; }
        public WfAuthor Authors { get => WfAuthor.NoOne; }
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
