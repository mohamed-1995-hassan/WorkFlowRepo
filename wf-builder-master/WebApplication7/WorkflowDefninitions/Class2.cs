using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebApplication7.Activities.PrepareStudentWorkflow;
using WebApplication7.Dtos;
using WebApplication7.Entities;

namespace WebApplication7.WorkflowDefninitions
{
    public abstract class WfJsonConverter<TStepPayload, TStep1, TStep2> : JsonConverter<TStepPayload>
        where TStepPayload : StepPayloadBase
        where TStep1 : TStepPayload
        where TStep2 : TStepPayload
    {
        private Dictionary<WfStep, Type> dic
        {
            get
            {
                return new Dictionary<WfStep, Type>()
                {
                    { WfStep.Init , typeof(AddNewStudentReview1StepDto)},
                    { WfStep.AddNewStudentReview1Step, typeof(AddNewStudentReview1Step) },
                    { WfStep.AddNewStudentReview2Step, typeof(AddNewStudentReview2StepDto) },
                    { WfStep.Finished , typeof(AddNewStudentReview1StepDto)},
                    { WfStep.ApplicationReview1Step , typeof(AddNewStudentReview1StepDto)},
                    { WfStep.ApplicationReview2Step , typeof(AddNewStudentReview1StepDto)},
                    { WfStep.ApplicationReview3Step , typeof(AddNewStudentReview1StepDto)},
                    { WfStep.ApplicationReview2ConfrimStep , typeof(AddNewStudentReview1StepDto)}
                };
            }
        }

        public override TStepPayload ReadJson(JsonReader reader, Type objectType, TStepPayload existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var stepPayload = JObject.Load(reader);

            var stepType = (WfStep)stepPayload.GetValue("Step", StringComparison.OrdinalIgnoreCase).Value<long>();

            return (TStepPayload)stepPayload.ToObject(dic[stepType]);
        }

        public override void WriteJson(JsonWriter writer, TStepPayload value, JsonSerializer serializer)
        {
            writer.WriteValue(JsonConvert.SerializeObject(value));
        }
    }


    public class WfStepsJsonConverter : WfJsonConverter<StepPayloadBase, AddNewStudentReview1StepDto, AddNewStudentReview2StepDto>
    {

    }

    public abstract class StepPayloadBase
    {
        public abstract WfStep Step { get; set; }
    }
}
