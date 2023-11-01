using WebApplication7.Dtos;
using WebApplication7.Entities;
using WebApplication7.WorkflowDefninitions;

namespace WebApplication7.Activities.ApplicantWorkflow
{
    public class ApplicationReview2Step : IWorkflowActivity
    {
        public WfStep Step => WfStep.ApplicationReview2Step;
        public WfDesicion AvaliableDesicions => WfDesicion.Submit;
        public WfDesicion AllowedInComes => WfDesicion.Approve1;
        public WfAuthor Authors { get; }
        public WfDesicion InCome { get; set; }
        public List<IWorkflowActivity> NextSteps { get; set; } = new();
        public void Executing(UserTask currentTask, StepPayloadBase? data = null)
        {

        }
    }
}
