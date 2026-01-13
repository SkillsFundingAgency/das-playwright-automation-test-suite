namespace SFA.DAS.EmployerPortal.UITests.Project.Pages;


public class YourOrganisationsAndAgreementsPage(ScenarioContext context, bool navigate = false) : InterimYourOrganisationsAndAgreementsPage(context, navigate)
{

    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator(".govuk-heading-xl")).ToContainTextAsync("Your organisations and agreements");
    }

    public async Task VerifyTransfersStatus(string expected) => await Assertions.Expect(page.Locator("#main-content")).ToContainTextAsync($"Transfers status: {expected}");

    public async Task<SearchForYourOrganisationPage> ClickAddNewOrganisationButton()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Add an organisation" }).ClickAsync();

        return await VerifyPageAsync(() => new SearchForYourOrganisationPage(context));
    }

    public async Task VerifyNewlyAddedOrgIsPresent()
    {
        await Assertions.Expect(page.Locator("tbody")).ToContainTextAsync(objectContext.GetOrganisationName());
    }

    public async Task<YourAgreementsWithTheEducationAndSkillsFundingAgencyPage> ClickViewAgreementLink()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "View all agreements" }).ClickAsync();

        return await VerifyPageAsync(() => new YourAgreementsWithTheEducationAndSkillsFundingAgencyPage(context));
    }

    public async Task<AreYouSureYouWantToRemovePage> ClickOnRemoveAnOrgFromYourAccountLink()
    {
        await page.GetByRole(AriaRole.Row, new() { Name = objectContext.GetOrganisationName() }).GetByRole(AriaRole.Link, new() { Name = "Remove organisation" }).ClickAsync();

        return await VerifyPageAsync(() => new AreYouSureYouWantToRemovePage(context));
    }

    public async Task<AccessDeniedPage> ClickToRemoveAnOrg()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Remove organisation" }).First.ClickAsync();

        return await VerifyPageAsync(() => new AccessDeniedPage(context));
    }

    public async Task VerifyRemoveLinkHidden()
    {
        await Assertions.Expect(page.GetByRole(AriaRole.Link, new() { Name = "Remove organisation" })).ToBeHiddenAsync();
    }

    public async Task VerifyOrgRemovedMessageInHeader() => await Assertions.Expect(page.Locator("#main-content")).ToContainTextAsync("You have removed");
}
