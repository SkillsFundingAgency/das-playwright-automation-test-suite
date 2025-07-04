namespace SFA.DAS.EPAO.UITests.Project.Tests.Pages.AssessmentService.OrganisationDetails;

public class AS_OrganisationDetailsPage(ScenarioContext context) : EPAO_BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Organisation details");

    public async Task<AS_ChangeContactNamePage> ClickContactNameChangeLink()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Change contact name" }).ClickAsync();

        return await VerifyPageAsync(() => new AS_ChangeContactNamePage(context));
    }

    public async Task<AS_ChangePhoneNumberPage> ClickPhoneNumberChangeLink()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Change phone number" }).ClickAsync();

        return await VerifyPageAsync(() => new AS_ChangePhoneNumberPage(context));
    }

    public async Task<AS_ChangeAddressPage> ClickAddressChangeLink()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Change address" }).ClickAsync();

        return await VerifyPageAsync(() => new AS_ChangeAddressPage(context));
    }

    public async Task<AS_ChangeEmailPage> ClickEmailChangeLink()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Change email" }).ClickAsync();

        return await VerifyPageAsync(() => new AS_ChangeEmailPage(context));
    }

    public async Task<AS_ChangeWebsitePage> ClickWebsiteChangeLink()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Change website" }).ClickAsync();

        return await VerifyPageAsync(() => new AS_ChangeWebsitePage(context));
    }
}


public class AS_ChangeOrganisationDetailsPage(ScenarioContext context) : EPAO_BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Change organisation details");

    public async Task<AS_OrganisationDetailsPage> ClickAccessButton()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Access" }).ClickAsync();

        return await VerifyPageAsync(() => new AS_OrganisationDetailsPage(context));
    }
}
