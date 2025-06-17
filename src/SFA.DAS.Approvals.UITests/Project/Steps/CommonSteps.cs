using SFA.DAS.Approvals.UITests.Project.Helpers.DataHelpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace SFA.DAS.Approvals.UITests.Project.Steps
{
    [Binding]
    public class CommonSteps
    {
        protected readonly ScenarioContext context;
        protected readonly FeatureContext featureContext;
        private List<Apprenticeship> listOfApprenticeship;

        public CommonSteps(ScenarioContext context, FeatureContext featureContext)
        {
            this.context = context;
            this.featureContext = featureContext;
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



            

        }

        [Then("Verify following email notifications")]
        public void ThenVerifyFollowingEmailNotifications(Table table)
        {
            Console.WriteLine(listOfApprenticeship);
        }


    }
}
