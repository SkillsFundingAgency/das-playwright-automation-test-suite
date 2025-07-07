

namespace SFA.DAS.AODP.UITests.Project.Tests.Pages.DfE
{
    public class DfeAdminHomePage(ScenarioContext context) : DfeAdminStartPage(context)
    {
        private ILocator Dashboard => page.Locator("//h1[.=\"Dashboard\"]");
        private ILocator SaveBtn => page.GetByText("Save");
        private ILocator CancelBtn => page.GetByText("Cancel");
        private ILocator NavigateBackBtn => page.Locator(".govuk-back-link");

        public async Task ClickContinue() => await page.Locator("#continue").ClickAsync();
        public async Task ClickSaveButton() => await SaveBtn.ClickAsync();
        public async Task ClickCancelButton() => await CancelBtn.ClickAsync();

        public async Task NavigateBack() => await ClickOn(NavigateBackBtn);

        public override async Task VerifyPage() => await Assertions.Expect(Dashboard).ToBeVisibleAsync();


        public async Task ClickStartNow()
        {
            await page.Locator("a.govuk-button--start").ClickAsync();
        }

    }
}
