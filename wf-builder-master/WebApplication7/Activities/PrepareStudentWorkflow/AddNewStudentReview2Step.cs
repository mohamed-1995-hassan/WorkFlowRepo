using WebApplication7.Dtos;
using WebApplication7.Entities;
using WebApplication7.WorkflowDefninitions;

namespace WebApplication7.Activities.PrepareStudentWorkflow
{
    public class AddNewStudentReview2Step : IWorkflowActivity
    {
        public WfStep Step { get => WfStep.AddNewStudentReview2Step; }
        public WfDesicion InCome { get; set; }
        public List<IWorkflowActivity> NextSteps { get; set; } = new();
        public WfDesicion AvaliableDesicions { get => WfDesicion.Approve2 | WfDesicion.Reject2; }
        public WfDesicion AllowedInComes { get => WfDesicion.Approve1; }
        public WfAuthor Authors { get => WfAuthor.R3; }
        public void Executing(UserTask currentTask, StepPayloadBase? data = null)
        {

        }
    }
}
