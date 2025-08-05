
using SFA.DAS.Approvals.UITests.Project.Pages;
using TechTalk.SpecFlow;
using Azure;
using SFA.DAS.Approvals.UITests.Project.Helpers.DataHelpers;
using SFA.DAS.Approvals.UITests.Project.Pages.Employer;
using SFA.DAS.Approvals.UITests.Project.Helpers.DataHelpers.ApprenticeshipModel;



namespace SFA.DAS.Approvals.UITests.Project.Pages.Employer
{
    internal class StartAddingApprenticesPage(ScenarioContext context) : ApprovalsBasePage(context)
    {

        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Start adding apprentices");
        }

        public async Task<CohortSentYourTrainingProviderPage> EmployerSendsToProviderToAddApprenticesAsync(ApprenticeDataHelper apprenticeDataHelper)
        {
            await EmployerSendsToProviderToAdd();
            await page.Locator("#MessageBox").FillAsync(apprenticeDataHelper.MessageToProvider);
            await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();
            return new CohortSentYourTrainingProviderPage(context);
        }
        internal async Task<StartAddingApprenticesPage> EmployerSendsToProviderToAdd()

        {
            await page.GetByRole(AriaRole.Radio, new() { Name = "I would like my training provider to add apprentices" }).CheckAsync();

            return this;
            
        }
    }
}



