namespace SFA.DAS.EmployerPortal.UITests.Project.Pages;


public class YourOrganisationsAndAgreementsPage(ScenarioContext context, bool navigate = false) : InterimYourOrganisationsAndAgreementsPage(context, navigate)
{

    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Your organisations and agreements");
    }

    //private static By TransferStatus => By.CssSelector("p.govuk-body");


    //public bool VerifyTransfersStatus(string expected) => VerifyElement(() => pageInteractionHelper.FindElements(TransferStatus), $"Transfers status:  {expected}");

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
