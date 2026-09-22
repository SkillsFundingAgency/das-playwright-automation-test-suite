using SFA.DAS.Approvals.UITests.Project.Helpers;
using SFA.DAS.Approvals.UITests.Project.Helpers.API;
using SFA.DAS.Approvals.UITests.Project.Helpers.DataHelpers.ApprenticeshipModel;
using SFA.DAS.Approvals.UITests.Project.Helpers.SqlHelpers;
using SFA.DAS.Approvals.UITests.Project.Helpers.TestDataHelpers;
using SFA.DAS.Approvals.UITests.Project.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SFA.DAS.Approvals.UITests.Project.Steps
{
    [Binding]
    internal class CommitmentsApiRelatedSteps
    {
        protected readonly ScenarioContext context;
        protected readonly FeatureContext featureContext;
        private readonly CommitmentsDbSqlHelper commitmentsDbSqlHelper;
        private readonly CommitmentsInnerApiClient commitmentsInnerApiClient;
        private List<Apprenticeship> listOfApprenticeship;

        public CommitmentsApiRelatedSteps(ScenarioContext context, FeatureContext featureContext)
        {
            this.context = context;
            this.featureContext = featureContext;
            commitmentsDbSqlHelper = context.Get<CommitmentsDbSqlHelper>();
            commitmentsInnerApiClient = context.Get<CommitmentsInnerApiClient>();
            listOfApprenticeship = new List<Apprenticeship>();
        }

        [When(@"^Learning domain sends CoC request via approvals endpoint for below ""(.*)"": ""(.*)"", ""(.*)"", ""(.*)"" and ""(.*)""$")]
        public async Task WhenLearningDomainSendsCoCRequestViaApprovalsEndpointForBelowAnd(string category, string changeType, string oldValue, string newValue, string effectiveFromDate)
        {
            var apprenticeship = context.Get<List<Apprenticeship>>(ScenarioKeys.ListOfApprenticeship).FirstOrDefault();
            var apprenticeshipId = apprenticeship.ApprenticeDetails.ApprenticeshipId;
            var learningType = apprenticeship.TrainingDetails.LearningType;
            var learningKey = "9df21641-c3b2-4af3-acd3-d066243bf7e7";
            var uln = apprenticeship.ApprenticeDetails.ULN;
            var ukprn = apprenticeship.ProviderDetails.Ukprn;


            var request = new CoCApprovalRequest
            {
                learningKey = learningKey,
                uln = uln,
                learningType = Enum.GetName(typeof(LearningType), apprenticeshipId),
                ukprn = ukprn.ToString(),
                approvedUri = $@"/learning/{learningKey}",
                changes = new CoCApprovalFieldRequest
                {
                    changeType = changeType,
                    data = new CoCData
                    {
                        old = oldValue,
                        @new = newValue,
                        effectiveFromDate = effectiveFromDate
                    }
                }
            };

            var response = await commitmentsInnerApiClient.PutCoCApprovalRequest(request, learningKey);
        }

    }
}
