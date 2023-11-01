using WebApplication7.Dtos;
using WebApplication7.Entities;
using WebApplication7.WorkflowDefninitions;

namespace WebApplication7.Activities.ApplicantWorkflow
{
    public class ApplicationReview3Step : IWorkflowActivity
    {
        public WfStep Step => WfStep.ApplicationReview3Step;
        public WfDesicion AvaliableDesicions => WfDesicion.Approve3 | WfDesicion.Reject3;
        public WfDesicion AllowedInComes => WfDesicion.Approve2;
        public WfAuthor Authors { get; }
        public WfDesicion InCome { get; set; }
        public List<IWorkflowActivity> NextSteps { get; set; } = new();
        public void Executing(UserTask currentTask, StepPayloadBase? data = null)
        {

        }
    }
}
