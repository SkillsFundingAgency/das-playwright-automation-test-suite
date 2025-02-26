using SFA.DAS.Registration.UITests.Project.Pages.CreateAccount;

namespace SFA.DAS.Registration.UITests.Project.Pages;

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
