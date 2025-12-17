using SFA.DAS.ProviderLogin.Service.Project.Pages;

namespace SFA.DAS.Approvals.UITests.Project.Pages.Provider
{
    internal class YouNeedPermissionToDoThisPage(ScenarioContext context) : ApprovalsBasePage(context)
    {
        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("You need permission to do this");
        }

        internal async Task<ProviderHomePage> ClickOnGoToHomepageButton()
        {
            await page.GetByRole(AriaRole.Button, new() { Name = "Go to homepage" }).ClickAsync();
            return await VerifyPageAsync(() => new ProviderHomePage(context));
        }


    }
}
