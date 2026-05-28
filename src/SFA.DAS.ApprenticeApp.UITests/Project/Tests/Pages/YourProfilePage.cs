using Reqnroll;

namespace SFA.DAS.ApprenticeApp.UITests.Project.Tests.Pages
{
    public class YourProfilePage(ScenarioContext context) : AppBasePage(context)
    {
        private const string YourProfileHeader = "h1.govuk-heading-xl";

        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator(YourProfileHeader))
                .ToContainTextAsync("Your apprenticeship details");
        }

        public async Task<string> YourProfilePageTitleAsync()
            => await page.Locator(YourProfileHeader).InnerTextAsync();
    }
}
