namespace SFA.DAS.EPAO.UITests.Project.Tests.Pages.AssessmentService.OrganisationDetails;


public class AS_ChangePhoneNumberPage(ScenarioContext context) : EPAO_BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Change phone number");

    public async Task<AS_ConfirmPhoneNumberPage> EnterRandomPhoneNumberAndClickUpdate()
    {
        await page.GetByRole(AriaRole.Textbox, new() { Name = "Phone number" }).FillAsync(EPAODataHelper.GetRandomNumber(10));

        await page.GetByRole(AriaRole.Button, new() { Name = "Update number" }).ClickAsync();

        return await VerifyPageAsync(() => new AS_ConfirmPhoneNumberPage(context));
    }
}

public class AS_ConfirmPhoneNumberPage(ScenarioContext context) : EPAO_BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Confirm phone number");

    public async Task<AS_ContactPhoneNumberUpdatedPage> ClickConfirmButtonInConfirmPhoneNumberPage()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Confirm" }).ClickAsync();

        return await VerifyPageAsync(() => new AS_ContactPhoneNumberUpdatedPage(context));
    }
}

public class AS_ContactPhoneNumberUpdatedPage(ScenarioContext context) : AS_ChangeOrgDetailsBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("#main-content")).ToContainTextAsync("Contact phone number updated");
}