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
        private readonly ApprenticeDataHelper apprenticeDataHelper;
        private List<Apprenticeship> listOfApprenticeship;

        public CommonSteps(ScenarioContext context, FeatureContext featureContext)
        {
            this.context = context;
            this.featureContext = featureContext;
            approvalsEmailsHelper = new ApprovalsEmailsHelper(context);
            commitmentsDbSqlHelper = context.Get<CommitmentsDbSqlHelper>();
            apprenticeDataHelper = new ApprenticeDataHelper(context);
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

        [Given("A live apprentice record exists for an apprentice on Foundation level course")]
        public async Task GivenALiveApprenticeRecordExistsForAnApprenticeOnFoundationLevelCourse()
        {

            var additionalWhereFilter = @"AND a.TrainingCode IN('803','804','805','806','807','808','809', '810', '811')";
            await FindEditableApprenticeFromDbAndSaveItInContext(EmployerType.Levy, additionalWhereFilter);
        }

        [Given(@"a live apprentice record exists with startdate of <(.*)> months and endDate of <\+(.*)> months from current date")]
        public async Task GivenALiveApprenticeRecordExistsWithStartdateOfMonthsAndEndDateOfMonthsFromCurrentDate(int startDateFromNow, int endDateFromNow)
        {
            var additionalWhereFilter = @$"AND a.StartDate < DATEADD(month, {startDateFromNow}, GETDATE()) 
                                            AND a.EndDate > DATEADD(month, {endDateFromNow}, GETDATE())
                                            AND a.TrainingCode < 800";
            await FindEditableApprenticeFromDbAndSaveItInContext(EmployerType.Levy, additionalWhereFilter);

        }

        private async Task FindEditableApprenticeFromDbAndSaveItInContext(EmployerType employerType, string additionalWhereFilter)
        {
            Apprenticeship apprenticeship = await apprenticeDataHelper.CreateEmptyCohortAsync(employerType);
            apprenticeship = await commitmentsDbSqlHelper.GetEditableApprenticeDetails(apprenticeship, additionalWhereFilter);
            listOfApprenticeship.Add(apprenticeship);
            context.Set(listOfApprenticeship);
        }

    }
}
