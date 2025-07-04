namespace SFA.DAS.EPAO.UITests.Project.Tests.Pages.AssessmentService.OrganisationDetails;

public class AS_ChangeEmailPage(ScenarioContext context) : EPAO_BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Change email address");

    public async Task<AS_ConfirmEmailAddressPage> EnterRandomEmailAndClickChange()
    {
        await page.GetByRole(AriaRole.Textbox, new() { Name = "Contact email" }).FillAsync(ePAOAssesmentServiceDataHelper.RandomEmail);

        await page.GetByRole(AriaRole.Button, new() { Name = "Change email address" }).ClickAsync();

        return await VerifyPageAsync(() => new AS_ConfirmEmailAddressPage(context));
    }
}

public class AS_ConfirmEmailAddressPage(ScenarioContext context) : EPAO_BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Confirm email address");

    public async Task<AS_EmailAddressUpdatedPage> ClickConfirmButtonInConfirmEmailAddressPage()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Confirm email address" }).ClickAsync();

        return await VerifyPageAsync(() => new AS_EmailAddressUpdatedPage(context));
    }
}

public class AS_EmailAddressUpdatedPage(ScenarioContext context) : AS_ChangeOrgDetailsBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("#main-content")).ToContainTextAsync("Email address updated");
}