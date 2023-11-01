using WebApplication7.Dtos;
using WebApplication7.Entities;
using WebApplication7.WorkflowDefninitions;

namespace WebApplication7.Activities.PrepareStudentWorkflow
{
    public class AddNewStudentReview1Step : IWorkflowActivity
    {
        public WfStep Step { get => WfStep.AddNewStudentReview1Step; }
        public WfDesicion InCome { get; set; }
        public List<IWorkflowActivity> NextSteps { get; set; } = new();
        public WfDesicion AvaliableDesicions { get => WfDesicion.Approve1 | WfDesicion.Reject1; }
        public WfDesicion AllowedInComes { get => WfDesicion.Init; }
        public WfAuthor Authors { get => WfAuthor.R2; }

        public void Executing(UserTask currentTask, StepPayloadBase? data = null)
        {

        }
    }
}
