using Azure;
using Polly;
using SFA.DAS.EPAO.UITests.Project.Helpers.DataHelpers;

namespace SFA.DAS.EPAO.UITests.Project.Tests.Pages.Admin;

public class AddOrganisationPage(ScenarioContext context) : EPAOAdmin_BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.GetByRole(AriaRole.Heading)).ToContainTextAsync("Add an organisation");

    public async Task<OrganisationDetailsPage> EnterDetails()
    {
        await page.GetByRole(AriaRole.Textbox, new() { Name = "Organisation name" }).FillAsync(ePAOAdminDataHelper.NewOrganisationName);

        await page.GetByRole(AriaRole.Textbox, new() { Name = "Legal name (optional)" }).FillAsync(ePAOAdminDataHelper.NewOrganisationLegalName);

        await page.GetByRole(AriaRole.Textbox, new() { Name = "Company number (optional)" }).FillAsync(ePAOAdminDataHelper.CompanyNumber);
        
        await page.GetByRole(AriaRole.Textbox, new() { Name = "Charity number (optional)" }).FillAsync(ePAOAdminDataHelper.CharityNumber);
        
        await page.GetByRole(AriaRole.Textbox, new() { Name = "UKPRN (optional)" }).FillAsync(ePAOAdminDataHelper.NewOrganisationUkprn);

        await page.GetByRole(AriaRole.Radio, new() { Name = "Awarding Organisations" }).CheckAsync();

        await page.GetByRole(AriaRole.Textbox, new() { Name = "Email" }).FillAsync(ePAOAdminDataHelper.RandomEmail);
        
        await page.GetByRole(AriaRole.Textbox, new() { Name = "PhoneNumber" }).FillAsync(EPAOAdminDataHelper.PhoneNumber);
        
        await page.GetByRole(AriaRole.Textbox, new() { Name = "Website" }).FillAsync(ePAOAdminDataHelper.RandomWebsiteAddress);

        await page.GetByRole(AriaRole.Textbox, new() { Name = "Street address" }).FillAsync(EPAOAdminDataHelper.StreetAddress1);
        
        await page.GetByRole(AriaRole.Textbox, new() { Name = "Address 2" }).FillAsync(EPAOAdminDataHelper.StreetAddress2);
        
        await page.GetByRole(AriaRole.Textbox, new() { Name = "Address 3" }).FillAsync(EPAOAdminDataHelper.StreetAddress3);
        
        await page.GetByRole(AriaRole.Textbox, new() { Name = "City / Town" }).FillAsync(EPAOAdminDataHelper.TownName);
        
        await page.GetByRole(AriaRole.Textbox, new() { Name = "Postcode" }).FillAsync(EPAOAdminDataHelper.PostCode);

        await page.GetByRole(AriaRole.Button, new() { Name = "Save" }).ClickAsync();

        return await VerifyPageAsync(() => new OrganisationDetailsPage(context));
    }
}


public class OrganisationDetailsPage(ScenarioContext context) : EPAOAdmin_BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("#main-content")).ToContainTextAsync("Organisation details");

    public async Task<EditOrganisationPage> EditOrganisation()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Edit" }).ClickAsync();

        return await VerifyPageAsync(() => new EditOrganisationPage(context));
    }

    public async Task VerifyOrganisationStatus(string status) => await VerifyOrganisationDetails("Organisation status", status);

    public async Task VerifyOrganisationCharityNumber() => await VerifyOrganisationDetails("Charity number", ePAOAdminDataHelper.CharityNumber);

    public async Task VerifyOrganisationCompanyNumber() => await VerifyOrganisationDetails("Company number", ePAOAdminDataHelper.CompanyNumber);

    public async Task<AddContactPage> AddNewContact()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Add new contact" }).ClickAsync();

        return await VerifyPageAsync(() => new AddContactPage(context));
    }

    public async Task<ContactDetailsPage> SelectContact()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = ePAOAdminDataHelper.FullName }).ClickAsync();

        return await VerifyPageAsync(() => new ContactDetailsPage(context));
    }

    private async Task VerifyOrganisationDetails(string headerName, string value)
    {
        await Assertions.Expect(page.Locator(".govuk-summary-list__row")).ToContainTextAsync($"{headerName} {value}");
    }
}

public class EditOrganisationPage(ScenarioContext context) : EPAOAdmin_BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Edit organisation");

    public async Task<OrganisationDetailsPage> MakeOrgLive()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Make Live" }).ClickAsync();

        return await VerifyPageAsync(() => new OrganisationDetailsPage(context));
    }

    public async Task<OrganisationDetailsPage> EditDetails()
    {
        await page.GetByRole(AriaRole.Textbox, new() { Name = "Company number (optional)" }).FillAsync(ePAOAdminDataHelper.CompanyNumber);

        await page.GetByRole(AriaRole.Textbox, new() { Name = "Charity number (optional)" }).FillAsync(ePAOAdminDataHelper.CharityNumber);

        await page.GetByRole(AriaRole.Button, new() { Name = "Save" }).ClickAsync();

        return await VerifyPageAsync(() => new OrganisationDetailsPage(context));
    }
}

public class AddContactPage(ScenarioContext context) : EPAOAdmin_BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.GetByRole(AriaRole.Heading)).ToContainTextAsync("Add a contact");

    public async Task<ContactDetailsPage> AddContact()
    {
        await page.GetByRole(AriaRole.Textbox, new() { Name = "Given name" }).FillAsync(ePAOAdminDataHelper.GivenNames);
        
        await page.GetByRole(AriaRole.Textbox, new() { Name = "Family name" }).FillAsync(ePAOAdminDataHelper.FamilyName);
        
        await page.GetByRole(AriaRole.Textbox, new() { Name = "Email" }).FillAsync(ePAOAdminDataHelper.Email);
        
        await page.GetByRole(AriaRole.Textbox, new() { Name = "Phone number" }).FillAsync(EPAOAdminDataHelper.PhoneNumber);

        await page.GetByRole(AriaRole.Button, new() { Name = "Save" }).ClickAsync();

        return await VerifyPageAsync(() => new ContactDetailsPage(context));
    }
}

public class ContactDetailsPage(ScenarioContext context) : EPAOAdmin_BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.GetByRole(AriaRole.Heading)).ToContainTextAsync("View contact");

    public async Task<OrganisationDetailsPage> ReturnToOrganisationDetailsPage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Return to organisation" }).ClickAsync();

        return await VerifyPageAsync(() => new OrganisationDetailsPage(context));
    }
}