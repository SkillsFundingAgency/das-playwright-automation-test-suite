namespace SFA.DAS.AparAdmin.UITests.Project.Tests.Pages.AddJourney;

    public class SuccessfullyAddedPage(ScenarioContext context) : BasePage(context)
    {
        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1"))
                .ToContainTextAsync("New training provider added");
        }
    public async Task<ManageTrainingProviderInformationPage> GoBackToDashboard()
    {
        var editLink = page.GetByRole(AriaRole.Link, new() { Name = "back to the dashboard" });

        await Assertions.Expect(editLink).ToBeVisibleAsync(new() { Timeout = 10000 });
        await editLink.ClickAsync();

        return await VerifyPageAsync(() => new ManageTrainingProviderInformationPage(context));
    }

}

