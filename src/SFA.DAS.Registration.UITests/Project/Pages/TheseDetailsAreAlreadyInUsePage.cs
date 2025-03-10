using SFA.DAS.EmployerPortal.UITests.Project.Pages.CreateAccount;

namespace SFA.DAS.EmployerPortal.UITests.Project.Pages;

public class TheseDetailsAreAlreadyInUsePage(ScenarioContext context) : RegistrationBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("These details are already in use");
    }

    public async Task<AddPayeSchemeUsingGGDetailsPage> CickUseDifferentDetailsButtonInTheseDetailsAreAlreadyInUsePage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Use different details" }).ClickAsync();

        return await VerifyPageAsync(() => new AddPayeSchemeUsingGGDetailsPage(context));
    }

    public async Task<EnterYourPAYESchemeDetailsPage> CickBackLinkInTheseDetailsAreAlreadyInUsePage()
    {
        await ClickBackLink();

        return await VerifyPageAsync(() => new EnterYourPAYESchemeDetailsPage(context));
    }
}
