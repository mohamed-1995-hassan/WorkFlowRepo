using WebApplication7.Dtos;
using WebApplication7.Entities;
using WebApplication7.WorkflowDefninitions;

namespace WebApplication7.Activities.ApplicantWorkflow
{
    public class ApplicationReview1Step : IWorkflowActivity
    {
        public WfStep Step => WfStep.ApplicationReview1Step;
        public WfDesicion AvaliableDesicions => WfDesicion.Approve1 | WfDesicion.Reject1;
        public WfDesicion AllowedInComes => WfDesicion.Init;
        public WfAuthor Authors { get; }
        public WfDesicion InCome { get; set; }
        public List<IWorkflowActivity> NextSteps { get; set; } = new();
        public void Executing(UserTask currentTask, StepPayloadBase? data = null)
        {

        }
    }
}
