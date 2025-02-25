namespace SFA.DAS.Registration.UITests.Project.Pages.CreateAccount;

public class YouVeSuccessfullyAddedUserDetailsPage(ScenarioContext context, bool updated) : RegistrationBasePage(context)
{
    private readonly string PageTitle = updated ? "You have successfully changed user details" : "You have successfully added user details";

    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("#main-content")).ToContainTextAsync(PageTitle);

    public async Task<CreateYourEmployerAccountPage> ClickContinueButtonToAcknowledge()
    {
        await Continue();

        return await VerifyPageAsync(() => new CreateYourEmployerAccountPage(context));
    }

    public async Task<InvitationsPage> ClickContinueToInvitationsPage()
    {
        await Continue();

        return new InvitationsPage(context);
    }

    public async Task Continue() => await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();
}
