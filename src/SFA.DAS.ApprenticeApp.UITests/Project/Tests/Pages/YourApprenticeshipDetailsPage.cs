
namespace SFA.DAS.ApprenticeApp.UITests.Project.Tests.Pages
{
    public class YourApprenticeshipDetailsPage(ScenarioContext context) : AppBasePage(context)
    {
        private const string YourProfileHeader = "h1.govuk-heading-xl";

        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator(YourProfileHeader))
                .ToContainTextAsync("Your apprenticeship details");
        }

        public async Task<string> YourApprenticeshipDetailsPageTitleAsync()
            => await page.Locator(YourProfileHeader).InnerTextAsync();
    }
}
