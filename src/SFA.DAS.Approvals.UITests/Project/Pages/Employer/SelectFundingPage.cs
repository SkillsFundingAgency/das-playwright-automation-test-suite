
using Azure;
using SFA.DAS.Approvals.UITests.Project.Pages;
using SFA.DAS.Approvals.UITests.Project.Pages.Employer;
using TechTalk.SpecFlow;


namespace SFA.DAS.Approvals.UITests.Project.Pages.Employer
{
    internal class SelectFundingPage(ScenarioContext context) : ApprovalsBasePage(context)
    {
        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Select funding");
        }

        internal async Task<AddTrainingProviderDetailsPage> SelectFundingType()
        {
            await page.GetByRole(AriaRole.Radio, new() { Name = "Current levy funds" }).CheckAsync();

            await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

            return await VerifyPageAsync(() => new AddTrainingProviderDetailsPage(context));
           
        }
    }
}