using Newtonsoft.Json;
using WebApplication7.Entities;
using WebApplication7.WorkflowDefninitions;

namespace WebApplication7.Dtos
{
    public interface ITaskCommand
    {
        Guid WfId { get; set; }
        Guid TaskId { get; set; }
        WfDesicion Desicion { get; set; }
        string? Comment { get; set; }
    }
    public interface ITaskCommand<TContent> where TContent : class
    {
        Guid WfId { get; set; }
        Guid TaskId { get; set; }
        WfDesicion Desicion { get; set; }
        string? Comment { get; set; }
        TContent Data { get; set; }
    }
    public abstract class TaskCommand<TStepPayloadBase> : ITaskCommand<TStepPayloadBase>
        where TStepPayloadBase: StepPayloadBase
    {
        public Guid WfId { get; set; }
        public Guid TaskId { get; set; }
        public WfDesicion Desicion { get; set; }
        public string? Comment { get; set; }
        public TStepPayloadBase Data { get; set; }
    }

    public class StudentTaskCommand : TaskCommand<StepPayloadBase>
    {
        public Guid WfId { get; set; }
        public Guid TaskId { get; set; }
        public WfDesicion Desicion { get; set; }
        public string? Comment { get; set; }

        [JsonConverter(typeof(WfStepsJsonConverter))]
        public StepPayloadBase Data { get; set; }
    }

    public class StudentDto : StepPayloadBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public override WfStep Step { get; set; }
    }

    public class AddNewStudentReview1StepDto : StepPayloadBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public override WfStep Step { get; set; }
    }
    public class AddNewStudentReview2StepDto : StepPayloadBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public override WfStep Step { get; set; }
    }
}
