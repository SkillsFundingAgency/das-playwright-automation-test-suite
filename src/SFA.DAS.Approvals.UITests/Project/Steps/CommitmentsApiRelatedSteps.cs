using RestSharp;
using SFA.DAS.Approvals.UITests.Project.Helpers;
using SFA.DAS.Approvals.UITests.Project.Helpers.API;
using SFA.DAS.Approvals.UITests.Project.Helpers.DataHelpers.ApprenticeshipModel;
using SFA.DAS.Approvals.UITests.Project.Helpers.SqlHelpers;
using SFA.DAS.Approvals.UITests.Project.Helpers.TestDataHelpers;
using SFA.DAS.Approvals.UITests.Project.Models;
using System;
using System.Collections.Generic;
using System.Net;
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
        private RestResponse response;

        public CommitmentsApiRelatedSteps(ScenarioContext context, FeatureContext featureContext)
        {
            this.context = context;
            this.featureContext = featureContext;
            commitmentsDbSqlHelper = context.Get<CommitmentsDbSqlHelper>();
            commitmentsInnerApiClient = context.Get<CommitmentsInnerApiClient>();
            listOfApprenticeship = new List<Apprenticeship>();
        }

        [When(@"^Learning domain sends CoC request via approvals endpoint for below ""(.*)"": ""(.*)"", ""(.*)"", ""(.*)"" and ""(.*)""$")]
        public async Task WhenLearningDomainSendsCoCRequestViaApprovalsEndpointForBelowAnd(string category, string changeType, string oldValue, string newValue, string effFromDateDiff)
        {
            var apprenticeship = context.Get<List<Apprenticeship>>(ScenarioKeys.ListOfApprenticeship).FirstOrDefault();
            var apprenticeshipId = apprenticeship.ApprenticeDetails.ApprenticeshipId;
            var learningType = apprenticeship.TrainingDetails.LearningType;
            var learningKey = "9df21641-c3b2-4af3-acd3-d066243bf7e7";
            var uln = apprenticeship.ApprenticeDetails.ULN;
            var ukprn = apprenticeship.ProviderDetails.Ukprn;
            var effectiveFromDate = DateTime.Now.AddDays(int.Parse(effFromDateDiff)).ToString("yyyy-MM-dd");

            var request = new CoCApprovalRequest
            {
                learningKey = learningKey,
                apprenticeshipId = apprenticeshipId,
                uln = uln,
                learningType = Enum.GetName(typeof(LearningType), learningType),
                ukprn = ukprn.ToString(),
                approvedUri = $@"/learning/{learningKey}",
                changes = new List<CoCApprovalFieldRequest>
                {
                    new CoCApprovalFieldRequest
                    {
                        changeType = changeType,
                        data = new CoCData
                        {
                            old = oldValue,
                            @new = newValue,
                            effectiveFromDate = effectiveFromDate
                        }
                    }
                }
            };

            this.response = await commitmentsInnerApiClient.PutCoCApprovalRequest(request, learningKey);
        }

        [Then(@"^Commitments responds with ""(.*)"" and ""(.*)""$")]
        public async Task ThenCommitmentsRespondsWithAnd(string expectedResponseCode, string expectedResponseBody)
        {
            var expectedResponseCodeValue = Enum.GetName(typeof(HttpStatusCode), int.Parse(expectedResponseCode));
            Assert.AreEqual(expectedResponseCodeValue, response.StatusCode.ToString(), $"Expected response code to be {expectedResponseCodeValue} but was {response.StatusCode}");
            Assert.IsTrue(response.Content.Contains(expectedResponseBody), $"Expected response body to contain {expectedResponseBody} but was {response.Content}");
        }


    }
}
