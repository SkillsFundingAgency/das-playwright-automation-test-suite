namespace SFA.DAS.AparAdmin.UITests.Project.Tests.Pages.AddJourney;

    public class ConfirmDetailsPage(ScenarioContext context) : BasePage(context)
    {
        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1"))
                .ToContainTextAsync("Confirm details");
        }
    public async Task<SuccessfullyAddedPage> ConfirmDetailsAndContinue()
        {
            await page.GetByRole(AriaRole.Button, new() { Name = "Confirm and add organisation"}).ClickAsync();
            return await VerifyPageAsync(() => new SuccessfullyAddedPage(context));
        }
}

