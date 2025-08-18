using Microsoft.VisualBasic;
using SFA.DAS.Approvals.UITests.Project.Helpers;
using SFA.DAS.Approvals.UITests.Project.Helpers.DataHelpers;
using SFA.DAS.Approvals.UITests.Project.Helpers.DataHelpers.ApprenticeshipModel;
using SFA.DAS.Approvals.UITests.Project.Helpers.SqlHelpers;
using System;


namespace SFA.DAS.Approvals.UITests.Project.Steps
{
    [Binding]
    public class CommonSteps
    {
        protected readonly ScenarioContext context;
        protected readonly FeatureContext featureContext;
        private readonly ApprovalsEmailsHelper approvalsEmailsHelper;
        private readonly CommitmentsDbSqlHelper commitmentsDbSqlHelper;        
        private List<Apprenticeship> listOfApprenticeship;

        public CommonSteps(ScenarioContext context, FeatureContext featureContext)
        {
            this.context = context;
            this.featureContext = featureContext;
            approvalsEmailsHelper = new ApprovalsEmailsHelper(context);
            commitmentsDbSqlHelper = context.Get<CommitmentsDbSqlHelper>();            
            listOfApprenticeship = new List<Apprenticeship>();
        }


        [Given("previous test has been completed successfully")]
        public async Task GivenPreviousTestHasBeenCompletedSuccessfully()
        {
            await Task.CompletedTask;
            
            if (!featureContext.ContainsKey("ResultOfPreviousScenario"))
            {
                throw new Exception("Result of previous scenario is missing. Please run the test at Feature level!");
            }

            var scenarioAStatus = (ScenarioExecutionStatus)featureContext["ResultOfPreviousScenario"];

            Assume.That(scenarioAStatus == ScenarioExecutionStatus.OK, ($"Cannot run this test as previous test in the same feature file failed with status: '{scenarioAStatus}'"));
            {
                var previousScenarioContext = (ScenarioContext)featureContext["ScenarioContextofPreviousScenario"];

                var x = previousScenarioContext.GetValue<List<Apprenticeship>>();

                foreach (var apprenticeship in x)
                {
                    listOfApprenticeship.Add(apprenticeship);
                }

                context.Set(listOfApprenticeship);
            }

            /*
            if (scenarioAStatus == ScenarioExecutionStatus.OK)
            {
                var previousScenarioContext = (ScenarioContext)featureContext["ScenarioContextofPreviousScenario"];

                var x = previousScenarioContext.GetValue<List<Apprenticeship>>();

                foreach (var apprenticeship in x)
                {
                    listOfApprenticeship.Add(apprenticeship);
                }
                
                context.Set(listOfApprenticeship);
            }
            else
            {
                throw new Exception($"Cannot run this test as previous test in the same feature file failed with status: '{scenarioAStatus}'");
            } 
            */

        }

        [Then(@"Verify the ""(.*)"" receive ""(.*)"" email")]
        public async Task ThenVerifyTheReceiveEmail(string recipient, string notificationType)
        {
            await new ApprovalsEmailsHelper(context).VerifyEmailAsync(recipient, notificationType);
        }

        [Then(@"the '(.*)' receives '(.*)' email notification")]
        public async Task ThenTheReceivesEmailNotification(string recipient, string notificationType)
        {
            await approvalsEmailsHelper.VerifyEmailAsync(recipient, notificationType);
        }

        

    }
}
