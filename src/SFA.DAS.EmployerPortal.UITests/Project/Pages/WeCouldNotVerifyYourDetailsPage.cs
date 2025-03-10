namespace SFA.DAS.EmployerPortal.UITests.Project.Pages;

public class WeCouldNotVerifyYourDetailsPage(ScenarioContext context) : RegistrationBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("We could not verify your details");

        await Assertions.Expect(page.Locator("#main-content")).ToContainTextAsync("You have attempted to sign in to HMRC with the wrong details too many times.");
    }

    public async Task<UsingYourGovtGatewayDetailsPage> ClickAddViaGGLink()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Try adding your PAYE scheme" }).ClickAsync();

        return await VerifyPageAsync(() => new UsingYourGovtGatewayDetailsPage(context));
    }
}