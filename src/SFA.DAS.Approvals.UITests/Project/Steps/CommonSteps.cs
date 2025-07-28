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
            //GET Apprenticeship details from the database
            var ukprn = Convert.ToInt32(context.GetProviderConfig<ProviderConfig>().Ukprn);            
            EasAccountUser employerUser = context.GetUser<LevyUser>();
            
            var accountLegalEntityId = Convert.ToInt32(await context.Get<AccountsDbSqlHelper>().GetAccountLegalEntityId(employerUser.Username, employerUser.OrganisationName[..3] + "%"));            
            var details = await commitmentsDbSqlHelper.GetEditableApprenticeDetails(ukprn, accountLegalEntityId);

            var uln = details[0].ToString();
            var firstName = details[1].ToString();
            var LastName = details[2].ToString();
            var dob = Convert.ToDateTime(details[3].ToString());

            //SET Apprenticeship details in the context
            Apprenticeship apprenticeship = new Apprenticeship();
            
            apprenticeship.UKPRN = ukprn;
            apprenticeship.EmployerDetails.EmployerType = EmployerType.Levy;
            apprenticeship.EmployerDetails.Email = employerUser.Username;
            apprenticeship.EmployerDetails.EmployerName = employerUser.OrganisationName;
            apprenticeship.ApprenticeDetails.ULN = uln;
            apprenticeship.ApprenticeDetails.FirstName = firstName;
            apprenticeship.ApprenticeDetails.LastName = LastName;
            apprenticeship.ApprenticeDetails.DateOfBirth = dob;

            listOfApprenticeship.Add(apprenticeship);
            context.Set(listOfApprenticeship);

        }

        [Given(@"a live apprentice record exists with startdate of <(.*)> months and endDate of <\+(.*)> months from current date")]
        public void GivenALiveApprenticeRecordExistsWithStartdateOfMonthsAndEndDateOfMonthsFromCurrentDate(int startDateFromNow, int endDateFromNow)
        {
            throw new PendingStepException();
        }


    }
}
