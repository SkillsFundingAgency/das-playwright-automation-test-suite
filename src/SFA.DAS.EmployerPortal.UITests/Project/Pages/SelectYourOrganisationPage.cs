using static SFA.DAS.EmployerPortal.UITests.Project.Helpers.EnumHelper;

namespace SFA.DAS.EmployerPortal.UITests.Project.Pages;

public class SelectYourOrganisationPage(ScenarioContext context) : EmployerPortalBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator(".govuk-heading-xl")).ToContainTextAsync("Select your organisation");
    }

    public async Task<CheckYourDetailsPage> SelectYourOrganisation(OrgType orgType = OrgType.Default)
    {
        switch (orgType)
        {
            case OrgType.Company:
                await SelectOrg("Company", employerPortalDataHelper.CompanyTypeOrg);
                break;
            case OrgType.Company2:
                await SelectOrg("Company", employerPortalDataHelper.CompanyTypeOrg2);
                break;
            case OrgType.PublicSector:
                await SelectOrg("Public sector", employerPortalDataHelper.PublicSectorTypeOrg);
                break;
            case OrgType.Charity:
                await SelectOrg("Charity", employerPortalDataHelper.CharityTypeOrg1Name);
                break;
            case OrgType.Charity2:
                await SelectOrg("Charity", employerPortalDataHelper.CharityTypeOrg2Name);
                break;
            case OrgType.Default:
                await SelectOrg("Show all", objectContext.GetOrganisationName());
                break;
        }

        return await VerifyPageAsync(() => new CheckYourDetailsPage(context));
    }

    public async Task GetSearchResultsText(string resultMessage) => await Assertions.Expect(page.Locator("#main-content")).ToContainTextAsync(resultMessage, new LocatorAssertionsToContainTextOptions { IgnoreCase = true });

    public async Task VerifyOrgAlreadyAddedMessage() => await Assertions.Expect(page.Locator("ol")).ToContainTextAsync("Already added - view my organisations");
    //pageInteractionHelper.VerifyText(pageInteractionHelper.GetText(TextBelowOrgNameInResults(objectContext.GetOrganisationName())), "Already added");

    private async Task SelectOrg(string orgType, string orgName)
    {
        objectContext.SetRecentlyAddedOrganisationName(orgName);

        await page.GetByRole(AriaRole.Radio, new() { Name = orgType }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = orgName, Exact = true }).First.ClickAsync();
    }
}
