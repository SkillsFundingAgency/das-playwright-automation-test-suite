namespace SFA.DAS.EmployerPortal.UITests.Project.Pages.CreateAccount;

public class GgSignInPage(ScenarioContext context) : RegistrationBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.GetByRole(AriaRole.Heading)).ToContainTextAsync("Sign in");

    public async Task<SearchForYourOrganisationPage> SignInTo(int index)
    {
        await EnterGateWayCredentialsAndSignIn(index);

        return await VerifyPageAsync(() => new SearchForYourOrganisationPage(context));
    }

    public async Task<ConfirmPAYESchemePage> EnterPayeDetailsAndContinue(int index)
    {
        var gatewaydetails = await EnterGateWayCredentialsAndSignIn(index);

        return await VerifyPageAsync(() => new ConfirmPAYESchemePage(context, gatewaydetails.Paye));
    }

    public async Task SignInWithInvalidDetails()
    {
        await SignInTo(registrationDataHelper.InvalidGGId, registrationDataHelper.InvalidGGPassword);
    }

    public async Task VerifyErrorMessage(string error) => await Assertions.Expect(page.GetByRole(AriaRole.Alert)).ToContainTextAsync(error, new LocatorAssertionsToContainTextOptions { IgnoreCase = true });

    private async Task<GatewayCreds> EnterGateWayCredentialsAndSignIn(int index)
    {
        var gatewaydetails = objectContext.GetGatewayCreds(index);

        await SignInTo(gatewaydetails.GatewayId, gatewaydetails.GatewayPassword);

        return gatewaydetails;
    }

    private async Task SignInTo(string id, string password)
    {
        await page.GetByRole(AriaRole.Textbox, new() { Name = "User ID" }).FillAsync(id);

        await page.GetByRole(AriaRole.Textbox, new() { Name = "Password" }).FillAsync(password);

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();
    }
}
