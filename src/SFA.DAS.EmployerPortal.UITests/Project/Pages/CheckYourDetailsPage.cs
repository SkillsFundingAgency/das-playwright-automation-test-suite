namespace SFA.DAS.EmployerPortal.UITests.Project.Pages;

public class CheckYourDetailsPage(ScenarioContext context) : EmployerPortalBasePage(context)
{
    public override async Task VerifyPage()
    {
        var list = await page.Locator("h1").AllTextContentsAsync();

        VerifyPage(list, "Check your details");
    }

    public async Task<OrganisationHasBeenAddedPage> ClickYesContinueButton()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Yes, continue" }).ClickAsync();

        return await VerifyPageAsync(() => new OrganisationHasBeenAddedPage(context));
    }

    public async Task<AccessDeniedPage> ClickYesContinueButtonAndRedirectedToAccessDeniedPage()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Yes, continue" }).ClickAsync();

        return await VerifyPageAsync(() => new AccessDeniedPage(context));
    }

    public async Task<YouHaveAddedYourOrgAndPAYEScheme> ClickYesThisIsMyOrg()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "Yes, this is my organisation", Exact = true }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new YouHaveAddedYourOrgAndPAYEScheme(context));
    }

    public async Task<SearchForYourOrganisationPage> ClickOrganisationChangeLink()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Change   organisation" }).ClickAsync();

        return await VerifyPageAsync(() => new SearchForYourOrganisationPage(context));
    }

    public async Task<EnterYourPAYESchemeDetailsPage> ClickAornChangeLink()
    {
        await page.GetByRole(AriaRole.Row, new() { Name = "Account office reference" }).GetByRole(AriaRole.Link).ClickAsync();

        return await VerifyPageAsync(() => new EnterYourPAYESchemeDetailsPage(context));
    }

    public async Task<AddAPAYESchemePage> ClickPayeSchemeChangeLink()
    {
        await page.GetByRole(AriaRole.Row, new() { Name = "Employer PAYE reference" }).GetByRole(AriaRole.Link).ClickAsync();

        return await VerifyPageAsync(() => new AddAPAYESchemePage(context));
    }

    public async Task VerifyDetails(string message) => await Assertions.Expect(page.GetByRole(AriaRole.Rowgroup)).ToContainTextAsync(message, new LocatorAssertionsToContainTextOptions { IgnoreCase = true });

    public async Task VerifyInvalidAornAndPayeErrorMessage(string message) => await Assertions.Expect(page.GetByRole(AriaRole.Rowgroup)).ToContainTextAsync(message);

    public async Task VerifyPayeScheme(string message) => await Assertions.Expect(page.GetByRole(AriaRole.Rowgroup)).ToContainTextAsync(message);
}
