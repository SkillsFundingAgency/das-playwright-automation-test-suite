using Azure;
using SFA.DAS.Framework;
using SFA.DAS.MongoDb.DataGenerator;
using SFA.DAS.Registration.UITests.Project.Pages.InterimPages;
using SFA.DAS.Registration.UITests.Project.Pages.StubPages;

namespace SFA.DAS.Registration.UITests.Project.Pages;

public class UsingYourGovtGatewayDetailsPage(ScenarioContext context) : RegistrationBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Add a PAYE scheme using your Government Gateway details");
    }

    public async Task<GgSignInPage> ContinueToGGSignIn()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Continue" }).ClickAsync();

        return new GgSignInPage(context);
    }
}

public class AddAPAYESchemePage(ScenarioContext context) : RegistrationBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("#main-content")).ToContainTextAsync("Add a PAYE Scheme");
    }

    public async Task<UsingYourGovtGatewayDetailsPage> AddPaye()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "Use Government Gateway log in" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new UsingYourGovtGatewayDetailsPage(context));
    }

    public async Task<EnterYourPAYESchemeDetailsPage> AddAORN()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "Use Accounts Office reference" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new EnterYourPAYESchemeDetailsPage(context));
    }
}

public class EnterYourPAYESchemeDetailsPage(ScenarioContext context) : RegistrationBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Add PAYE details");
    }

    #region Constants
    public static string BlankAornFieldErrorMessage => "Enter your Accounts Office reference in the correct format";
    public static string AornInvalidFormatErrorMessage => "Enter your Accounts Office reference in the correct format";
    public static string BlankPayeFieldErrorMessage => "Enter your PAYE reference in the correct format";
    public static string PayeInvalidFormatErrorMessage => "Enter your PAYE reference in the correct format";
    public static string InvalidAornAndPayeErrorMessage1stAttempt => "You have 2 attempts remaining to enter a valid PAYE and accounts office reference";
    public static string InvalidAornAndPayeErrorMessage2ndAttempt => "You have 1 attempt remaining to enter a valid PAYE and accounts office reference";

    #endregion

    public async Task<CheckYourDetailsPage> EnterAornAndPayeDetailsForSingleOrgScenarioAndContinue()
    {
        await EnterAornAndPayeAndContinue();

        return await VerifyPageAsync(() => new CheckYourDetailsPage(context));
    }

    public async Task<TheseDetailsAreAlreadyInUsePage> ReEnterTheSameAornDetailsAndContinue()
    {
        await EnterAornAndPayeAndContinue();

        return await VerifyPageAsync(() => new TheseDetailsAreAlreadyInUsePage(context));
    }

    public async Task<ChooseAnOrganisationPage> EnterAornAndPayeDetailsForMultiOrgScenarioAndContinue()
    {
        await EnterAornAndPayeAndContinue();

        return await VerifyPageAsync(() => new ChooseAnOrganisationPage(context));
    }

    public async Task Continue()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();
    }

    private async Task EnterAornAndPayeAndContinue() => await EnterAornAndPayeAndContinue(registrationDataHelper.AornNumber, objectContext.GetGatewayPaye(0));

    public async Task EnterAornAndPayeAndContinue(string aornNumber, string Paye)
    {
        await page.GetByRole(AriaRole.Textbox, new() { Name = "Accounts office reference number" }).FillAsync(aornNumber);

        await page.GetByRole(AriaRole.Textbox, new() { Name = "Employer PAYE scheme reference" }).FillAsync(Paye);

        await Continue();
    }

    public async Task VerifyErrorMessageAboveAornTextBox(string message) => await Assertions.Expect(page.Locator("#error-message-aorn")).ToContainTextAsync(message);

    public async Task VerifyErrorMessageAbovePayeTextBox(string message) => await Assertions.Expect(page.Locator("#error-message-payeRef")).ToContainTextAsync(message);

    public async Task VerifyInvalidAornAndPayeErrorMessage(string message) => await Assertions.Expect(page.GetByLabel("There is a problem")).ToContainTextAsync(message);
}


public class ChooseAnOrganisationPage(ScenarioContext context) : RegistrationBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Choose your organisation");
    }
    
    public async Task<CheckYourDetailsPage> SelectFirstOrganisationAndContinue()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = objectContext.GetOrganisationName() }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new CheckYourDetailsPage(context));
    }
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
        await page.GetByRole(AriaRole.Link, new() { Name = $"Open  {objectContext.GetOrganisationName()}" }).ClickAsync();

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

        await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();

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

public class AboutYourAgreementPage(ScenarioContext context) : InterimEmployerBasePage(context, false)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("About your agreement");
    }

    public async Task<SignAgreementPage> ClickContinueToYourAgreementButtonInAboutYourAgreementPage()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new SignAgreementPage(context));
    }

    public async Task<SignAgreementPage> ClickContinueToYourAgreementButtonToDoYouAcceptTheEmployerAgreementPage()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new SignAgreementPage(context));

    }

    public async Task<CreateYourEmployerAccountPage> GoBackToCreateYourEmployerAccountPage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Back", Exact = true }).ClickAsync();

        return await VerifyPageAsync(() => new CreateYourEmployerAccountPage(context));
    }
}

public class SignAgreementPage(ScenarioContext context) : RegistrationBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("#main-content")).ToContainTextAsync($"Do you accept the employer agreement");
    }

    public async Task<AccessDeniedPage> ClickYesAndContinueDoYouAcceptTheEmployerAgreementOnBehalfOfPage()
    {
        await page.GetByText("Yes, I accept the agreement").ClickAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new AccessDeniedPage(context));
    }

    public async Task<YouHaveAcceptedYourEmployerAgreementPage> SignAgreement()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "Yes, I accept the agreement" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new YouHaveAcceptedYourEmployerAgreementPage(context));
    }

    public async Task<YouHaveAcceptedTheEmployerAgreementPage> SignAdditionalAgreement()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "Yes, I accept the agreement" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new YouHaveAcceptedTheEmployerAgreementPage(context));
    }

    public async Task<CreateYourEmployerAccountPage> DoNotSignAgreement()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "Not yet" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new CreateYourEmployerAccountPage(context));
    }
}

public class YouHaveAcceptedTheEmployerAgreementPage(ScenarioContext context) : RegistrationBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("#main-content")).ToContainTextAsync("accepted the employer agreement");

        await Assertions.Expect(page.GetByRole(AriaRole.Link, new() { Name = "Download your accepted agreement" })).ToBeVisibleAsync();
    }

    public async Task<HomePage> ClickOnViewYourAccount()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "View your account" }).ClickAsync();

        return await VerifyPageAsync(() => new HomePage(context));
    }

    public async Task<YourOrganisationsAndAgreementsPage> ClickOnReviewAndAcceptYourOtherAgreementsLink()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "review and accept your other agreements" }).ClickAsync();

        return await VerifyPageAsync(() => new YourOrganisationsAndAgreementsPage(context));
    }
}



public class YouHaveAcceptedYourEmployerAgreementPage(ScenarioContext context) : RegistrationBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("accepted your employer agreement");

        await Assertions.Expect(page.GetByRole(AriaRole.Alert)).ToContainTextAsync("Employer agreement accepted");
    }

    public async Task<CreateYourEmployerAccountPage> SelectContinueToCreateYourEmployerAccount()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new CreateYourEmployerAccountPage(context));
    }

    public async Task<HomePage> ClickOnViewYourAccountButton()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new HomePage(context));
    }
}

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