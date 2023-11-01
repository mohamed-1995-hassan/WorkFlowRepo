using WebApplication7.Dtos;
using WebApplication7.Entities;
using WebApplication7.WorkflowDefninitions;

namespace WebApplication7.Activities.ApplicantWorkflow
{
    public class ApplicationReview2ConfrimStep : IWorkflowActivity
    {
        public WfStep Step => WfStep.ApplicationReview2ConfrimStep;
        public WfDesicion AvaliableDesicions => WfDesicion.Approve2 | WfDesicion.Reject2;
        public WfDesicion AllowedInComes => WfDesicion.Submit;
        public WfAuthor Authors { get; }
        public WfDesicion InCome { get; set; }
        public List<IWorkflowActivity> NextSteps { get; set; } = new();
        public void Executing(UserTask currentTask, StepPayloadBase? data = null)
        {

        }
    }
}
