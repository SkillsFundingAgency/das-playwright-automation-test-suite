using static SFA.DAS.EmployerPortal.UITests.Project.Helpers.EnumHelper;

namespace SFA.DAS.EmployerPortal.UITests.Project.Pages;

public class SearchForYourOrganisationPage(ScenarioContext context) : EmployerPortalBasePage(context)
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
                orgName = (employerPortalDataHelper.CompanyTypeOrg);
                break;
            case OrgType.Company2:
                orgName = (employerPortalDataHelper.CompanyTypeOrg2);
                break;
            case OrgType.PublicSector:
                orgName = (employerPortalDataHelper.PublicSectorTypeOrg);
                break;
            case OrgType.Charity:
                orgName = (employerPortalDataHelper.CharityTypeOrg1Name);
                break;
            case OrgType.Charity2:
                orgName = (employerPortalDataHelper.CharityTypeOrg2Name);
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

        if (employerPortalDataHelper.SetAccountNameAsOrgName) objectContext.SetOrganisationName(orgName);
    }
}
