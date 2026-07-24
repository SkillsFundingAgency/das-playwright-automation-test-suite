using SFA.DAS.Campaigns.UITests.Project.Tests.Pages.Apprentices;
using System.Text.RegularExpressions;
using Microsoft.Playwright;

namespace SFA.DAS.Campaigns.UITests.Project.Tests.StepDefinitions
{
    [Binding]
    public class CampaignsCommonSteps(ScenarioContext context)
    {
        [Then(@"the user is taken to the external find an apprenticeship page")]
        public async Task ThenTheUserIsTakenToTheExternalFindAnApprenticeshipPage()
        {
            var page = context.Get<IPage>();

            var externalPage = await page.RunAndWaitForPopupAsync(async () =>
            {
                await page.GetByRole(AriaRole.Link, new() { Name = "Find an apprenticeship" }).ClickAsync();
            });

            await Assertions.Expect(externalPage).ToHaveURLAsync(new Regex("https://www.gov.uk/apply-apprenticeship"));

            var cookieBannerButton = externalPage.GetByRole(AriaRole.Button, new() { Name = "Accept additional cookies" });
            if (await cookieBannerButton.IsVisibleAsync())
            {
                await cookieBannerButton.ClickAsync();
            }

            var heading = externalPage.GetByRole(AriaRole.Heading, new() { Name = "Find an apprenticeship", Exact = true });
            await Assertions.Expect(heading).ToBeVisibleAsync();
        }
    }
}