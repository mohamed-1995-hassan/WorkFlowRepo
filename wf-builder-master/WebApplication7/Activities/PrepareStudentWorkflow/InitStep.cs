using WebApplication7.Dtos;
using WebApplication7.Entities;
using WebApplication7.WorkflowDefninitions;

namespace WebApplication7.Activities.PrepareStudentWorkflow
{
    public class InitStep : IWorkflowActivity
    {
        public WfStep Step { get => WfStep.Init; }
        public WfDesicion InCome { get; set; }
        public List<IWorkflowActivity> NextSteps { get; set; } = new();
        public WfDesicion AvaliableDesicions { get => WfDesicion.Init; }
        public WfDesicion AllowedInComes { get => WfDesicion.Empty; }
        public WfAuthor Authors { get => WfAuthor.R1; }
        public void Executing(UserTask currentTask, StepPayloadBase? data = null)
        {

        }
        public UserTask SendTask(Guid wfId, WfType workflowType, WfDesicion income)
        {
            if (AllowedInComes.HasFlag(income))
                return new UserTask
                {
                    AssignTo = WfAuthor.R2,
                    RequestId = wfId,
                    CurrentWorkflowStep = WfStep.AddNewStudentReview1Step,
                    WorkflowType = workflowType,
                    AllowedDesicions = WfDesicion.Approve1 | WfDesicion.Reject1
        };

            throw new Exception("this action is not avaliable!!");
        }
    }
}
