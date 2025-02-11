using SFA.DAS.Framework;

namespace SFA.DAS.Registration.UITests.Project.Pages;

public class AddAPAYESchemePage(ScenarioContext context) : RegistrationBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("#main-content")).ToContainTextAsync("Add a PAYE Scheme");
    }

    //public async Task<UsingYourGovtGatewayDetailsPage> AddPaye()
    //{
    //    await page.GetByRole(AriaRole.Radio, new() { Name = "Use Government Gateway log in" }).CheckAsync();

    //    await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

    //    return await VerifyPageAsync(() => new UsingYourGovtGatewayDetailsPage(context));
    //}

    //public async Task<EnterYourPAYESchemeDetailsPage> AddAORN()
    //{
    //    await page.GetByRole(AriaRole.Radio, new() { Name = "Use Accounts Office reference" }).CheckAsync();

    //    await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

    //    return await VerifyPageAsync(() => new EnterYourPAYESchemeDetailsPage(context));
    //}
}

public class CheckYourAccountPage(ScenarioContext context) : CheckPage(context)
{
    protected override string PageTitle => YourAccountsPage.PageTitle;

    protected override ILocator PageLocator => new YourAccountsPage(context).PageIdentifier;
}

public class YourAccountsPage(ScenarioContext context) : RegistrationBasePage(context)
{
    public static string PageTitle => "Your accounts";

    public ILocator PageIdentifier => page.Locator("#main-content");

    public override async Task VerifyPage() => await Assertions.Expect(PageIdentifier).ToContainTextAsync(PageTitle);

    public async Task<AddAPAYESchemePage> AddNewAccount()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Add new account" }).ClickAsync();

        return await VerifyPageAsync(() => new AddAPAYESchemePage(context));

    }

    public async Task<HomePage> ClickAccountLink(string orgName)
    {
        await page.GetByRole(AriaRole.Row, new() { Name = orgName }).GetByRole(AriaRole.Link).ClickAsync();

        objectContext.SetOrganisationName(orgName);

        return await VerifyPageAsync(() => new HomePage(context));
    }

    public async Task<HomePage> OpenAccount()
    {
        await page.GetByRole(AriaRole.Row).First.GetByRole(AriaRole.Link).ClickAsync();

        return await VerifyPageAsync(() => new HomePage(context));
    }
}



public class YouveLoggedOutPage(ScenarioContext context) : RegistrationBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("You have been signed out");

        await Assertions.Expect(page.GetByRole(AriaRole.Link, new() { Name = "sign in" })).ToBeVisibleAsync();
    }

    public async Task<SignInToYourApprenticeshipServiceAccountPage> CickSignInInYouveLoggedOutPage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "sign in" }).ClickAsync();

        return await VerifyPageAsync(() => new SignInToYourApprenticeshipServiceAccountPage(context));
    }

    public class SignInToYourApprenticeshipServiceAccountPage(ScenarioContext context) : RegistrationBasePage(context)
    {
        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Sign in to your apprenticeship service account");
        }

        public async Task<CreateAnAccountToManageApprenticeshipsPage> GoManageApprenticeLandingPage()
        {
            var url = UrlConfig.EmployerApprenticeshipService_BaseUrl;

            objectContext.SetDebugInformation(url);

            await driver.Page.GotoAsync(url);

            return await VerifyPageAsync(() => new CreateAnAccountToManageApprenticeshipsPage(context));
        }
    }

    public class RenameAccountPage(ScenarioContext context) : RegistrationBasePage(context)
    {
        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Rename account");
        }

        public async Task<HomePage> EnterNewNameAndContinue(string newOrgName)
        {
            await page.GetByRole(AriaRole.Textbox, new() { Name = "New account name" }).FillAsync(newOrgName);

            await page.GetByRole(AriaRole.Textbox, new() { Name = "New account name" }).ClickAsync();

            objectContext.SetOrganisationName(newOrgName);

            return await VerifyPageAsync(() => new HomePage(context));
        }
    }

    public class NotificationSettingsPage(ScenarioContext context) : RegistrationBasePage(context)
    {
        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Notification settings");
        }
    }
}
