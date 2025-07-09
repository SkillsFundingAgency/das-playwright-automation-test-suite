namespace SFA.DAS.EPAO.UITests.Project.Tests.Pages.Admin;

public class OrganisationSearchPage(ScenarioContext context) : EPAOAdmin_BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("label")).ToContainTextAsync("Organisation search");

    public async Task<OrganisationSearchResultsPage> SearchForAnOrganisation()
    {
        await page.GetByRole(AriaRole.Textbox, new() { Name = "Organisation search" }).FillAsync(objectContext.GetOrganisationIdentifier());

        await page.GetByRole(AriaRole.Button, new() { Name = "Search for an organisation" }).ClickAsync();

        return await VerifyPageAsync(() => new OrganisationSearchResultsPage(context));
    }
}

public class OrganisationSearchResultsPage(ScenarioContext context) : EPAOAdmin_BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.GetByRole(AriaRole.Heading)).ToContainTextAsync("Organisation search results");

    public async Task<OrganisationDetailsPage> SelectAnOrganisation()
    {
        await page.GetByRole(AriaRole.Row, new() { Name = objectContext.GetOrganisationIdentifier() }).GetByRole(AriaRole.Link).ClickAsync();

        return await VerifyPageAsync(() => new OrganisationDetailsPage(context));
    }
}