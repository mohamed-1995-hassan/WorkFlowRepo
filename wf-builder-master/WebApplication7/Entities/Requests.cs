using Microsoft.AspNetCore.Http.HttpResults;
using System.Collections.Generic;
using WebApplication7.Dtos;
using WebApplication7.WorkflowDefninitions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace WebApplication7.Entities
{
    public class WorkflowInstance
    {
        public WorkflowInstance()
        {
            UserTasks = new();
        }
        public Guid Id { get; set; }
        public int StudentId { get; set; }
        public string RequestNo { get; set; }
        public WfType WorkflowType { get; set; }
        public string Initiator { get; set; }
        public DateTime CreationAt { get; set; }
        public WfStatus Status { get; set; }
        public WfStep CurrentStep { get; set; }
        public List<UserTask> UserTasks { get; set; }
        private static Dictionary<WfType, IWorkflowDefiniation> Wfds
        {
            get
            {
                return new Dictionary<WfType, IWorkflowDefiniation>
                {
                    { WfType.PrepareStudent, PrepareStudentWorkflowDefiniation.Build() },
                    { WfType.Applicant, ApplicantWorkflowDefiniation.Build() }
                };
            }
        }

        public void MoveNext(UserTask currentTask, WfDesicion desicion, StepPayloadBase? data = null)
        {
            var wfd = Wfds[currentTask.WorkflowType];

            currentTask.TakeDesicion(desicion);

            var currentActivity = wfd.GetCurrentStep(currentTask);

            currentActivity.Executing(currentTask, data);

            var nextActivity = wfd.GetNextStep(currentTask);

            if (nextActivity == null || !nextActivity.NextSteps.Any())
            {
                Status = WfStatus.Completed;
                CurrentStep = WfStep.Finished;
                return;
            }

            var nextTask = nextActivity.SendTask(currentTask.RequestId, WorkflowType, desicion);

            UserTasks.Add(nextTask);

            Status = WfStatus.InProgress;

            CurrentStep = nextTask.CurrentWorkflowStep;
        }

        public static WorkflowInstance Init(string initiator, string requestNo, int studentId, WfType wfType)
        {
            var wfd = Wfds[wfType];

            var wf = new WorkflowInstance
            {
                Id = Guid.NewGuid(),
                CreationAt = DateTime.Now,
                Initiator = initiator,
                RequestNo = requestNo,
                WorkflowType = wfType,
                StudentId = studentId,
                CurrentStep = WfStep.Init,
                Status = WfStatus.Initiated
            };

            var nextActivity = wfd.GetNextStep();

            //nextActivity.Executing(null, command);

            var nextTask = nextActivity.SendTask(wf.Id, wfType, WfDesicion.Empty);

            wf.UserTasks.Add(nextTask);

            return wf;
        }

    }

    public enum WfType
    {
        PrepareStudent = 1,
        Applicant = 2
    }

    public enum WfStep
    {
        Init = 1,
        AddNewStudentReview1Step,
        AddNewStudentReview2Step,
        Finished,
        ApplicationReview1Step,
        ApplicationReview2Step,
        ApplicationReview3Step,
        ApplicationReview2ConfrimStep
    }

    public enum WfStatus
    {
        Initiated = 1,
        InProgress,
        Completed
    }

    [Flags]
    public enum WfDesicion
    {
        Empty = 1,
        Init = 2,
        Approve1 = 4,
        Reject1 = 8,
        Approve2 = 16,
        Reject2 = 32,
        Approve3 = 64,
        Reject3 = 128,
        Submit = 256
    }

    [Flags]
    public enum WfAuthor
    {
        NoOne = 1,
        R1 = 2,
        R2 = 4,
        R3 = 8
    }


}
