using NUnit.Framework;
using SFA.DAS.Registration.UITests.Project.Helpers.SqlDbHelpers;
using SFA.DAS.Registration.UITests.Project.Pages.InterimPages;
using SFA.DAS.Registration.UITests.Project.Pages.StubPages;
using static SFA.DAS.Registration.UITests.Project.Helpers.EnumHelper;
using static SFA.DAS.Registration.UITests.Project.Pages.YouveLoggedOutPage;

namespace SFA.DAS.Registration.UITests.Project.Pages;

public class YourOrganisationsAndAgreementsPage(ScenarioContext context, bool navigate = false) : InterimEmployerBasePage(context, navigate)
{
    private readonly RegistrationSqlDataHelper _registrationSqlDataHelper = context.GetValue<RegistrationSqlDataHelper>();

    #region Locators
    //private static By TransferStatus => By.CssSelector("p.govuk-body");
    //private static By AddNewOrganisationButton => By.CssSelector(".govuk-button");
    //private static By TableCells => By.XPath("//td");
    //private static By ViewAgreementLink() => By.PartialLinkText("View all agreements");
    //private static By ViewAgreementLink(string accountLegalEntityPublicHashedId) => By.CssSelector($"[href*='{accountLegalEntityPublicHashedId}/agreements']");
    //private static By OrgRemovedMessageInHeader => By.XPath("//h3");
    //private static By RemoveLinkBesideNewlyAddedOrg => By.LinkText($"Remove organisation");

    #endregion

    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Your organisations and agreements");
    }

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

    //public YourAgreementsWithTheEducationAndSkillsFundingAgencyPage ClickViewAgreementLink() =>
    //    ClickViewAgreementLink(() => formCompletionHelper.ClickElement(() => pageInteractionHelper.FindElement(ViewAgreementLink())));

    //public YourAgreementsWithTheEducationAndSkillsFundingAgencyPage ClickViewAgreementLink(string orgName)
    //{
    //    var accountLegalEntityPublicHashedId = _registrationSqlDataHelper.GetAccountLegalEntityPublicHashedId(objectContext.GetDBAccountId(), orgName);

    //    void action() => formCompletionHelper.ClickElement(() =>
    //    {
    //        var elements = pageInteractionHelper.FindElements(ViewAgreementLink(accountLegalEntityPublicHashedId));

    //        return elements.FirstOrDefault(x => x.Text == "View all agreements");
    //    });

    //    return ClickViewAgreementLink(action);
    //}

    //private YourAgreementsWithTheEducationAndSkillsFundingAgencyPage ClickViewAgreementLink(Action action)
    //{
    //    action.Invoke();

    //    return new YourAgreementsWithTheEducationAndSkillsFundingAgencyPage(context, action);
    //}

    //public AreYouSureYouWantToRemovePage ClickOnRemoveAnOrgFromYourAccountLink()
    //{
    //    tableRowHelper.SelectRowFromTable("Remove organisation", $"{objectContext.GetOrganisationName()}");
    //    return new AreYouSureYouWantToRemovePage(context);
    //}

    public async Task<AccessDeniedPage> ClickToRemoveAnOrg()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Remove organisation" }).First.ClickAsync();

        return await VerifyPageAsync(() => new AccessDeniedPage(context));
    }

    //public bool IsRemoveLinkBesideNewlyAddedOrg() => pageInteractionHelper.IsElementDisplayed(RemoveLinkBesideNewlyAddedOrg);

    //public bool VerifyOrgRemovedMessageInHeader() => pageInteractionHelper.VerifyText(OrgRemovedMessageInHeader, $"You have removed {objectContext.GetOrganisationName()}");
}

public class SearchForYourOrganisationPage(ScenarioContext context) : RegistrationBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("label")).ToContainTextAsync("Search for your organisation");
    }

    public async Task<SelectYourOrganisationPage> SearchForAnOrganisation(OrgType orgType = OrgType.Default)
    {
        string orgName = string.Empty;

        switch (orgType)
        {
            case OrgType.Company:
                orgName = (registrationDataHelper.CompanyTypeOrg);
                break;
            case OrgType.Company2:
                orgName = (registrationDataHelper.CompanyTypeOrg2);
                break;
            case OrgType.PublicSector:
                orgName = (registrationDataHelper.PublicSectorTypeOrg);
                break;
            case OrgType.Charity:
                orgName = (registrationDataHelper.CharityTypeOrg1Name);
                break;
            case OrgType.Charity2:
                orgName = (registrationDataHelper.CharityTypeOrg2Name);
                break;
            case OrgType.Default:
                orgName = (objectContext.GetOrganisationName());
                break;
        }

        await EnterAndSetOrgName(orgName);

        await Search();

        return await VerifyPageAsync(() => new SelectYourOrganisationPage(context));
    }

    public async Task<SelectYourOrganisationPage> SearchForAnOrganisation(string orgName)
    {
        await EnterAndSetOrgName(orgName);

        await Search();

        return await VerifyPageAsync(() => new SelectYourOrganisationPage(context));
    }

    private async Task Search() => await page.GetByRole(AriaRole.Button, new() { Name = "Search" }).ClickAsync();

    private async Task EnterAndSetOrgName(string orgName)
    {
        await page.GetByRole(AriaRole.Textbox, new() { Name = "Search for your organisation" }).FillAsync(orgName);

        if (registrationDataHelper.SetAccountNameAsOrgName) objectContext.SetOrganisationName(orgName);
    }
}

public class SelectYourOrganisationPage(ScenarioContext context) : RegistrationBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Select your organisation");
    }

    public async Task<CheckYourDetailsPage> SelectYourOrganisation(OrgType orgType = OrgType.Default)
    {
        switch (orgType)
        {
            case OrgType.Company:
                await SelectOrg("Company", registrationDataHelper.CompanyTypeOrg);
                break;
            case OrgType.Company2:
                await SelectOrg("Company", registrationDataHelper.CompanyTypeOrg2);
                break;
            case OrgType.PublicSector:
                await SelectOrg("Public sector", registrationDataHelper.PublicSectorTypeOrg);
                break;
            case OrgType.Charity:
                await SelectOrg("Charity", registrationDataHelper.CharityTypeOrg1Name);
                break;
            case OrgType.Charity2:
                await SelectOrg("Charity", registrationDataHelper.CharityTypeOrg2Name);
                break;
            case OrgType.Default:
                await SelectOrg("Show all", objectContext.GetOrganisationName());
                break;
        }

        return await VerifyPageAsync(() => new CheckYourDetailsPage(context));
    }

    //public string GetSearchResultsText() => pageInteractionHelper.GetText(SearchResultsText);

    //public bool VerifyOrgAlreadyAddedMessage() => pageInteractionHelper.VerifyText(pageInteractionHelper.GetText(TextBelowOrgNameInResults(objectContext.GetOrganisationName())), "Already added");

    private async Task SelectOrg(string orgType, string orgName)
    {
        objectContext.SetRecentlyAddedOrganisationName(orgName);

        await page.GetByRole(AriaRole.Radio, new() { Name = orgType }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = orgName, Exact = true }).ClickAsync();
    }
}

public class CheckYourDetailsPage(ScenarioContext context) : RegistrationBasePage(context)
{
    public override async Task VerifyPage()
    {
        var list = await page.Locator("h1").AllTextContentsAsync();

        CollectionAssert.Contains(list, "Check your details");
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


}

public class YouHaveAddedYourOrgAndPAYEScheme(ScenarioContext context) : RegistrationBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("You've added your organisation and PAYE scheme");
    }

    public async Task<CreateYourEmployerAccountPage> ContinueToConfirmationPage()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return new CreateYourEmployerAccountPage(context);
    }
}


public class AccessDeniedPage(ScenarioContext context) : RegistrationBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Access denied");
    }

    public async Task<HomePage> GoBackToTheServiceHomePage(string orgName)
    {
        objectContext.SetOrganisationName(orgName);

        await page.GetByRole(AriaRole.Link, new() { Name = "Go back to the service home" }).ClickAsync();

        return await VerifyPageAsync(() => new HomePage(context));
    }
}