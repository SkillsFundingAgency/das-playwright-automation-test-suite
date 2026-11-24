using SFA.DAS.Approvals.UITests.Project.Helpers;
using SFA.DAS.Approvals.UITests.Project.Helpers.API;
using SFA.DAS.Approvals.UITests.Project.Helpers.DataHelpers.ApprenticeshipModel;
using SFA.DAS.Approvals.UITests.Project.Helpers.TestDataHelpers;
using SFA.DAS.Approvals.UITests.Project.Models;
using System;


namespace SFA.DAS.Approvals.UITests.Project.Steps
{
    [Binding]
    internal class CocSteps
    {
        protected readonly ScenarioContext context;
        protected readonly ObjectContext objectContext;
        private readonly CommitmentsInnerApiClient commitmentsInnerApiClient;
        
        public CocSteps(ScenarioContext context, FeatureContext featureContext)
        {
            this.context = context;
            this.objectContext = context.Get<ObjectContext>();
            commitmentsInnerApiClient = context.Get<CommitmentsInnerApiClient>();            
        }

        [When(@"a CoC request is made with below payload")]
        public async Task WhenACoCRequestIsMadeWithBelowPayload()
        {
            //implemented in next step
        }

        [Then(@"the response is as per table below")]
        public Task ThenTheResponseIsAsPerTableBelow(DataTable dataTable) => ValidateRequestResponse(dataTable);

        public async Task ValidateRequestResponse(Table table)
        {            
            var apprenticeship = context.Get<List<Apprenticeship>>(ScenarioKeys.ListOfApprenticeship).FirstOrDefault();
            var learningKey = Guid.NewGuid().ToString();
            int counter = 0;

            var request = new CoCApprovalRequest
            {
                learningKey = learningKey,
                apprenticeshipId = apprenticeship.ApprenticeDetails.ApprenticeshipId,
                uln = apprenticeship.ApprenticeDetails.ULN,
                learningType = Enum.GetName(typeof(LearningType), apprenticeship.TrainingDetails.LearningType),
                ukprn = apprenticeship.ProviderDetails.Ukprn.ToString(),
                approvedUri = $@"/learning/{learningKey}"
                
            };

            foreach (var row in table.Rows)
            {
                request.changes = new List<CoCApprovalFieldRequest>
                    {
                        new CoCApprovalFieldRequest
                        {
                            changeType = row["changeType"],
                            data = new CoCData
                            {
                                old = row["old"],
                                @new = row["new"]                                
                            }
                        }
                    
                    };

                if (row["changeType"]=="StartDate") //price episode(s) always accompny startDate change request
                {
                    request.newPrices = new List<PriceRecord>
                    {
                        new PriceRecord 
                        {
                            trainingPrice = apprenticeship.TrainingDetails.TrainingPrice,
                            assessmentPrice = apprenticeship.TrainingDetails.EpaoPrice,
                            effectiveFrom = row["new"],
                        }
                    };
                }

                var actualResponse = await commitmentsInnerApiClient.PutCoCApprovalRequest(request, learningKey);
                var expectedResponse = Enum.GetName(typeof(RequestResponse), int.Parse(row["responseCode"]));
                Assert.AreEqual(expectedResponse, actualResponse.StatusCode.ToString(), $"Expected response code: {expectedResponse}, but got: {actualResponse.StatusCode}");
                Assert.IsTrue(actualResponse.Content.Contains(row["responseBody"]));
                objectContext.SetConsoleAndDebugInformation($"successfully verified row #{counter} for response code: {row["responseCode"]} and message: {row["responseBody"]}");

                counter++;
            }

        }

        
       
        


    }
}
