using WebApplication7.Activities;
using WebApplication7.Activities.ApplicantWorkflow;
using WebApplication7.Entities;

namespace WebApplication7.WorkflowDefninitions
{
    public class ApplicantWorkflowDefiniation : IWorkflowDefiniation
    {
        private static ApplicantWorkflowDefiniation _instance = null;
        public WfType Type => WfType.Applicant;
        public IWorkflowActivity? InitActivity { get; set; }
        public static ApplicantWorkflowDefiniation Build()
        {
            if (_instance == null)
                _instance = new ApplicantWorkflowDefiniation();

            return _instance;
        }
        protected ApplicantWorkflowDefiniation()
        {
            this.StartWith<InitStep>()
                .Then<ApplicationReview1Step>(WfDesicion.Init)
                    .Branch(WfDesicion.Approve1,
                        (d, act) =>
                        {
                            act.Then<ApplicationReview2Step>(d)
                               .Then<ApplicationReview2ConfrimStep>(WfDesicion.Submit)
                               .Branch(WfDesicion.Approve2,
                                        (dd, act) =>
                                        {
                                            act.Then<ApplicationReview3Step>(dd)
                                                .Branch(WfDesicion.Approve3,
                                                        (ddd, act) =>
                                                        {
                                                            act.EndWith<FinishStep>(ddd);
                                                        })
                                                .Branch(WfDesicion.Reject3,
                                                        (dddd, act) =>
                                                        {
                                                            act.EndWith<FinishStep>(dddd);
                                                        });
                                        })
                               .Branch(WfDesicion.Reject2,
                                        (ddddd, act) =>
                                        {
                                            act.EndWith<FinishStep>(ddddd);
                                        });
                        })
                    .Branch(WfDesicion.Reject1,
                        (d, act) =>
                        {
                            act.EndWith<FinishStep>(d);
                        });

        }
    }
}
