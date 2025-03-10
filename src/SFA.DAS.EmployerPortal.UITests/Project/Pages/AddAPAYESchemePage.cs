namespace SFA.DAS.EmployerPortal.UITests.Project.Pages;

public class AddAPAYESchemePage(ScenarioContext context) : EmployerPortalBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("#main-content")).ToContainTextAsync("Add a PAYE Scheme");
    }

    public async Task<UsingYourGovtGatewayDetailsPage> AddPaye()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "Use Government Gateway log in" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new UsingYourGovtGatewayDetailsPage(context));
    }

    public async Task<EnterYourPAYESchemeDetailsPage> AddAORN()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "Use Accounts Office reference" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new EnterYourPAYESchemeDetailsPage(context));
    }
}
