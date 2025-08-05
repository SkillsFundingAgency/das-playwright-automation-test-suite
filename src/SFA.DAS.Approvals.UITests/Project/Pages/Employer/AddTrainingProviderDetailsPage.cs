
using Azure;
using SFA.DAS.Approvals.UITests.Project.Pages.Provider;
using SFA.DAS.Approvals.UITests.Project.Pages;
using TechTalk.SpecFlow;


namespace SFA.DAS.Approvals.UITests.Project.Pages.Employer
{
    internal class AddTrainingProviderDetailsPage(ScenarioContext context) : ApprovalsBasePage(context)
    {
        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Select your training provider");
        }

        internal async Task<ConfirmTrainingProviderPage> SubmitValidUkprn(string Ukprn)
        {
            await page.GetByRole(AriaRole.Row, new() { Name = Ukprn }).GetByRole(AriaRole.Link).ClickAsync();

            return await VerifyPageAsync(() => new ConfirmTrainingProviderPage(context));

        }


    }
}