using NUnit.Framework;
using SFA.DAS.Registration.UITests.Project.Helpers.SqlDbHelpers;
using SFA.DAS.Registration.UITests.Project.Pages.InterimPages;
using SFA.DAS.Registration.UITests.Project.Pages.StubPages;
using System.Reflection.PortableExecutable;
using System;
using static SFA.DAS.Registration.UITests.Project.Helpers.EnumHelper;
using static SFA.DAS.Registration.UITests.Project.Pages.YouveLoggedOutPage;
using Azure;

namespace SFA.DAS.Registration.UITests.Project.Pages;

public class ReviewYourDetailsPage(ScenarioContext context) : RegistrationBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Review your details");
    }

    public async Task VerifyInfoTextInReviewYourDetailsPage(string expectedText)
    {
        await Assertions.Expect(page.Locator("#main-content")).ToContainTextAsync(expectedText, new LocatorAssertionsToContainTextOptions { IgnoreCase = true});
    }

    public async Task<DetailsUpdatedPage> SelectUpdateMyDetailsRadioOptionAndContinueInReviewYourDetailsPage()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "Yes, update my details using" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new DetailsUpdatedPage(context));
    }
}

public class DetailsUpdatedPage(ScenarioContext context) : RegistrationBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("#main-content")).ToContainTextAsync("Details updated");
    }

    public async Task<HomePage> SelectGoToHomePageOptionAndContinueInDetailsUpdatedPage()
    {
        await page.GetByRole(AriaRole.Radio).Nth(1).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new HomePage(context));

    }
}


public class YourAgreementsWithTheEducationAndSkillsFundingAgencyPage(ScenarioContext context) : RegistrationBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Your agreements with the Department for Education (DfE)");
    }

    public async Task<ReviewYourDetailsPage> ClickUpdateTheseDetailsLinkInReviewYourDetailsPage()
    {
        await ShowSection();

        await page.GetByRole(AriaRole.Link, new() { Name = "Update these details" }).ClickAsync();

        return await VerifyPageAsync(() => new ReviewYourDetailsPage(context));
    }

    public async Task VerifyIfUpdateTheseDetailsLinkIsHidden()
    {
        await Assertions.Expect(page.GetByRole(AriaRole.Link, new() { Name = "Update these details" })).ToBeHiddenAsync();
    }

    private async Task ShowSection()
    {
        if (await page.GetByRole(AriaRole.Button, new() { Name = "Show all sections" }).IsVisibleAsync())
        {
            await page.GetByRole(AriaRole.Button, new() { Name = "Show all sections" }).ClickAsync();
        }
    }
}


public class YourOrganisationsAndAgreementsPage(ScenarioContext context, bool navigate = false) : InterimEmployerBasePage(context, navigate)
{

    //private static By TransferStatus => By.CssSelector("p.govuk-body");

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

public class AreYouSureYouWantToRemovePage(ScenarioContext context) : RegistrationBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Are you sure you want to remove");
    }

    public async Task<YourOrganisationsAndAgreementsPage> SelectYesRadioOptionAndClickContinueInRemoveOrganisationPage()
    {
        await page.Locator("#remove-yes").CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new YourOrganisationsAndAgreementsPage(context));
    }
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

    public async Task GetSearchResultsText(string resultMessage) => await Assertions.Expect(page.Locator("#main-content")).ToContainTextAsync(resultMessage, new LocatorAssertionsToContainTextOptions { IgnoreCase = true});

    public async Task VerifyOrgAlreadyAddedMessage() => await Assertions.Expect(page.Locator("ol")).ToContainTextAsync("Already added - view my organisations");
    //pageInteractionHelper.VerifyText(pageInteractionHelper.GetText(TextBelowOrgNameInResults(objectContext.GetOrganisationName())), "Already added");

    private async Task SelectOrg(string orgType, string orgName)
    {
        objectContext.SetRecentlyAddedOrganisationName(orgName);

        await page.GetByRole(AriaRole.Radio, new() { Name = orgType }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = orgName, Exact = true }).First.ClickAsync();
    }
}

public class OrganisationHasBeenAddedPage(ScenarioContext context) : InterimHomeBasePage(context, false)
{
    public override async Task VerifyPage()
    {
        var list = await page.Locator(".das-notification__heading").AllTextContentsAsync();

        CollectionAssert.Contains(list, $"{objectContext.GetRecentlyAddedOrganisationName()} has been added");
    }
}


public class CheckYourDetailsPage(ScenarioContext context) : RegistrationBasePage(context)
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

    public async Task VerifyDetails(string message) => await Assertions.Expect(page.GetByRole(AriaRole.Rowgroup)).ToContainTextAsync(message, new LocatorAssertionsToContainTextOptions{IgnoreCase = true});

    public async Task VerifyInvalidAornAndPayeErrorMessage(string message) => await Assertions.Expect(page.GetByRole(AriaRole.Rowgroup)).ToContainTextAsync(message);

    public async Task VerifyPayeScheme(string message) => await Assertions.Expect(page.GetByRole(AriaRole.Rowgroup)).ToContainTextAsync(message);
}

public class YouHaveAddedYourOrgAndPAYEScheme(ScenarioContext context) : RegistrationBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.GetByRole(AriaRole.Alert)).ToContainTextAsync("Organisation and PAYE scheme added");

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