namespace SFA.DAS.EmployerPortal.UITests.Project.Pages.CreateAccount;

public class AddPayeSchemeUsingGGDetailsPage(ScenarioContext context) : RegistrationBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Add a PAYE scheme using your Government Gateway details");


    public async Task<GgSignInPage> AgreeAndContinue()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new GgSignInPage(context));
    }

    public async Task<TheseDetailsAreAlreadyInUsePage> ClickBackButton()
    {
        await ClickBackLink();

        return await VerifyPageAsync(() => new TheseDetailsAreAlreadyInUsePage(context));
    }

}
