namespace SFA.DAS.AparAdmin.UITests.Project.Tests.Pages.AddJourney;

    public class OrganisationsPage(ScenarioContext context) : BasePage(context)
    {
        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1"))
                .ToContainTextAsync("Choose the type of organisation for");
        }
    public async Task<ConfirmDetailsPage> SelectTypeofOrganisation_School()
    {
        await page.GetByLabel("School").CheckAsync();
        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();
        return await VerifyPageAsync(() => new ConfirmDetailsPage(context));
    }
}

