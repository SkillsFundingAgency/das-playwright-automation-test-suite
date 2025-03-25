namespace SFA.DAS.ProviderLogin.Service.Project.Pages;

public partial class ProviderHomePage(ScenarioContext context) : InterimProviderBasePage(context)
{
    public static string ProviderHomePageIdentifierCss => "#main-content .govuk-hint";

    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("#main-content")).ToContainTextAsync($"UKPRN: {ukprn}", new() { Timeout = 300000 });
    }

    public async Task ClickAddAnEmployerLink() => await page.GetByRole(AriaRole.Link, new() { Name = "Add an employer" }).ClickAsync();

    public async Task ClickFundingLink() => await page.GetByRole(AriaRole.Link, new() { Name = "Get funding for non-levy" }).ClickAsync();

    public async Task ClickViewEmployersAndManagePermissionsLink() => await page.GetByRole(AriaRole.Link, new() { Name = "View employers and manage" }).ClickAsync();

}
