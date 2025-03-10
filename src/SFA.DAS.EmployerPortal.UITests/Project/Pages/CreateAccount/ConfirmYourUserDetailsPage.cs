using SFA.DAS.EmployerPortal.UITests.Project.Pages.StubPages;

namespace SFA.DAS.EmployerPortal.UITests.Project.Pages.CreateAccount;

public class ConfirmYourUserDetailsPage(ScenarioContext context) : EmployerPortalBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Confirm your user details");

    public async Task<YouVeSuccessfullyAddedUserDetailsPage> ConfirmNameAndContinue(bool updated = false)
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new YouVeSuccessfullyAddedUserDetailsPage(context, updated));
    }

    public async Task<StubAddYourUserDetailsPage> ClickChange()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Change   first name" }).ClickAsync();

        return await VerifyPageAsync(() => new StubAddYourUserDetailsPage(context));
    }
}
