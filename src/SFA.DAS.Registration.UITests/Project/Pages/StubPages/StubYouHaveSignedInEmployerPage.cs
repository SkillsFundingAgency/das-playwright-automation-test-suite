using Azure;
using NUnit.Framework;
using Polly;

using SFA.DAS.Registration.UITests.Project.Helpers;

namespace SFA.DAS.Registration.UITests.Project.Pages.StubPages;

public class StubYouHaveSignedInEmployerPage(ScenarioContext context, string username, string idOrUserRef, bool newUser) : StubYouHaveSignedInBasePage(context, username, idOrUserRef, newUser)
{
    public override async Task VerifyPage()
    {
        await base.VerifyPage();

        if (newUser)
        {
            idOrUserRef = await new UsersSqlDataHelper(objectContext, context.Get<DbConfig>()).GetUserId(username);

            objectContext.UpdateLoginIdOrUserRef(username, idOrUserRef);
        }
    }

    //public async Task<MyAccountTransferFundingPage> ContinueToMyAccountTransferFundingPage()
    //{
    //    await Continue();
    //    return new MyAccountTransferFundingPage(context);
    //}

    public async Task<YourAccountsPage> ContinueToYourAccountsPage()
    {
        await Continue();

        return await VerifyPageAsync(() => new YourAccountsPage(context));
    }

    public async Task<HomePage> ContinueToHomePage()
    {
        await Continue();

        return await VerifyPageAsync(() => new HomePage(context));
    }

    public async Task<AccountUnavailablePage> GoToAccountUnavailablePage()
    {
        await Continue();

        return await VerifyPageAsync(() => new AccountUnavailablePage(context));
    }

    public async Task<StubAddYourUserDetailsPage> ContinueToStubAddYourUserDetailsPage()
    {
        await Continue();

        return await VerifyPageAsync(() => new StubAddYourUserDetailsPage(context));
    }

    public async Task Continue() => await page.GetByRole(AriaRole.Link, new() { Name = "Continue" }).ClickAsync();
}


public class StubAddYourUserDetailsPage(ScenarioContext context) : StubAddYourUserDetailsBasePage(context)
{
    public async Task<ConfirmYourUserDetailsPage> EnterNameAndContinue(RegistrationDataHelper dataHelper)
    {
        await EnterNameAndContinue(dataHelper.FirstName, dataHelper.LastName);

        return await VerifyPageAsync(() => new ConfirmYourUserDetailsPage(context));
    }
}
public class ConfirmYourUserDetailsPage(ScenarioContext context) : RegistrationBasePage(context)
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

public class InvitationsPage(ScenarioContext context) : RegistrationBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Invitations");

    public async Task<HomePage> ClickAcceptInviteLink()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Accept invite" }).ClickAsync();

        return await VerifyPageAsync(() => new HomePage(context));
    }
}

public class CreateYourEmployerAccountPage(ScenarioContext context) : RegistrationBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Create your employer account");

    #region Constants
    public static string UserDetailsItemText => "Add your user detail";
    public static string OrganisationAndPAYEItemText => "Add a PAYE scheme";
    public static string AccountNameItemText => "Set your account name";
    public static string EmployerAgreementItemText => "Your employer agreement";
    public static string TrainingProviderItemText => "Add a training provider and set their permissions";

    #endregion

    public async Task<ChangeYourUserDetailsPage> GoToAddYouUserDetailsLink()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Add your user details" }).ClickAsync();

        return await VerifyPageAsync(() => new ChangeYourUserDetailsPage(context));
    }

    public async Task<HowMuchIsYourOrgAnnualPayBillPage> GoToAddPayeLink()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Add a PAYE scheme" }).ClickAsync();

        return await VerifyPageAsync(() => new HowMuchIsYourOrgAnnualPayBillPage(context));
    }

    public async Task<CannotAddPayeSchemePage> GoToAddPayeLinkWhenAlreadyAdded()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Add a PAYE scheme" }).ClickAsync();

        return await VerifyPageAsync(() => new CannotAddPayeSchemePage(context));
    }

    public async Task<SetYourEmployerAccountNamePage> GoToSetYourAccountNameLink()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Set your account name" }).ClickAsync();

        return await VerifyPageAsync(() => new SetYourEmployerAccountNamePage(context));
    }

    public async Task<AboutYourAgreementPage> GoToYourEmployerAgreementLink()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Your employer agreement" }).ClickAsync();

        return await VerifyPageAsync(() => new AboutYourAgreementPage(context));
    }

    public async Task<AddATrainingProviderPage> GoToTrainingProviderLink()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Add a training provider and" }).ClickAsync();

        return await VerifyPageAsync(() => new AddATrainingProviderPage(context));
    }

    public async Task VerifySetYourAccountNameStepCannotBeStartedYet()
    {
        await Assertions.Expect(page.Locator("#status-set-account-name")).ToContainTextAsync("Cannot start yet");
    }

    public async Task VerifyYourEmployerAgreementStepCannotBeStartedYet()
    {
        await Assertions.Expect(page.Locator("#status-sign-agreement")).ToContainTextAsync("Cannot start yet");
    }

    public async Task VerifyAddTrainingProviderStepCannotBeStartedYet()
    {
        await Assertions.Expect(page.Locator("#status-add-training-provider")).ToContainTextAsync("Cannot start yet");
    }

    //public async Task VerifyStepCannotBeStartedYet(string listItemText)
    //{
    //    By stepSelector = By.XPath($"//span[contains(text(), '{listItemText}')]");

    //    var element = pageInteractionHelper.FindElement(stepSelector);
    //    string tagName = element.TagName.ToLower();

    //    Assert.AreNotEqual("a", tagName, "The text has an anchor tag");
    //}
}

public class CannotAddPayeSchemePage(ScenarioContext context) : RegistrationBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Add PAYE Scheme");

    public async Task<CreateYourEmployerAccountPage> GoBackToCreateYourEmployerAccountPage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Close" }).ClickAsync();

        return await VerifyPageAsync(() => new CreateYourEmployerAccountPage(context));
    }
}

public class ChangeYourUserDetailsPage(ScenarioContext context) : RegistrationBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Change your user details");

    public async Task<ConfirmYourUserDetailsPage> EnterName()
    {
        await page.GetByRole(AriaRole.Textbox, new() { Name = "First name" }).FillAsync(registrationDataHelper.FirstName);

        await page.GetByRole(AriaRole.Textbox, new() { Name = "Last name" }).FillAsync(" " + registrationDataHelper.LastName);

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new ConfirmYourUserDetailsPage(context));
    }
}


public class HowMuchIsYourOrgAnnualPayBillPage(ScenarioContext context) : RegistrationBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("How much is your organisation's annual pay bill?");

    public async Task<AddAPAYESchemePage> SelectOptionLessThan3Million()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "Less than £3 million" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new AddAPAYESchemePage(context));
    }

    public async Task<AddPayeSchemeUsingGGDetailsPage> SelectOptionCloseTo3Million()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "Close to £3 million" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new AddPayeSchemeUsingGGDetailsPage(context));
    }

    public async Task<CreateYourEmployerAccountPage> GoBackToCreateYourEmployerAccountPage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Back", Exact = true }).ClickAsync();

        return await VerifyPageAsync(() => new CreateYourEmployerAccountPage(context));
    }
}


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

        return await VerifyPageAsync(() => new ConfirmPAYESchemePage(context,gatewaydetails.Paye));
    }

    public async Task SignInWithInvalidDetails()
    {
        await SignInTo(registrationDataHelper.InvalidGGId, registrationDataHelper.InvalidGGPassword);
    }

    public async Task VerifyErrorMessage(string error) => await Assertions.Expect(page.GetByRole(AriaRole.Alert)).ToContainTextAsync(error, new LocatorAssertionsToContainTextOptions { IgnoreCase = true});

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

public class SetYourEmployerAccountNamePage(ScenarioContext context) : RegistrationBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Set your account name");

    public async Task<YourAccountNameHasBeenChangedPage> SelectoptionToSkipNameChange()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "No, I don't need to change my" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new YourAccountNameHasBeenChangedPage(context));
    }

    public async Task<ConfirmYourNewAccountNamePage> SelectoptionToChangeAccountName(string newAccountName)
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "Yes, I want to change my" }).CheckAsync();

        await page.GetByRole(AriaRole.Textbox, new() { Name = "Enter new account name." }).FillAsync(newAccountName);

        objectContext.SetOrganisationName(newAccountName);

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new ConfirmYourNewAccountNamePage(context));
    }

    public async Task<CreateYourEmployerAccountPage> GoBackToCreateYourEmployerAccountPage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Back", Exact = true }).ClickAsync();

        return await VerifyPageAsync(() => new CreateYourEmployerAccountPage(context));
    }
}

public class YourAccountNameHasBeenChangedPage(ScenarioContext context) : RegistrationBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.GetByRole(AriaRole.Alert)).ToContainTextAsync("Account name confirmed");

        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("You have confirmed your Account name");
    }

    public async Task<CreateYourEmployerAccountPage> ContinueToAcknowledge()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new CreateYourEmployerAccountPage(context));
    }
}

public class ConfirmYourNewAccountNamePage(ScenarioContext context) : RegistrationBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Confirm your new account name");

    public async Task<YouAccountNameHasBeenChangeToPage> ContinueToAcknowledge(string newAccountName)
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new YouAccountNameHasBeenChangeToPage(context, newAccountName));
    }
}

public class YouAccountNameHasBeenChangeToPage(ScenarioContext context, string newAccountName) : RegistrationBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync($"Your account name has been changed to {newAccountName}");

    public async Task<CreateYourEmployerAccountPage> ContinueToCreateYourEmployerAccountPage()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new CreateYourEmployerAccountPage(context));
    }
}

public class AddATrainingProviderPage(ScenarioContext context) : RegistrationBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Add a training provider");

    public async Task<EnterYourTrainingProviderNameReferenceNumberUKPRNPage> AddTrainingProviderNow()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "Yes, I'll add a training" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new EnterYourTrainingProviderNameReferenceNumberUKPRNPage(context));
    }

    public async Task<EmployerAccountCreatedPage> AddTrainingProviderLater()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "No, I want to finish setting" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new EmployerAccountCreatedPage(context));
    }

    public async Task<CreateYourEmployerAccountPage> GoBackToCreateYourEmployerAccountPage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Back", Exact = true }).ClickAsync();

        return await VerifyPageAsync(() => new CreateYourEmployerAccountPage(context));
    }
}

public class EnterYourTrainingProviderNameReferenceNumberUKPRNPage(ScenarioContext context) : RegistrationBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("name or reference number (UKPRN)");
    }

    public async Task<AddPermissionsForTrainingProviderPage> SearchForATrainingProvider(ProviderConfig providerConfig)
    {
        await EnterATrainingProvider(providerConfig);

        return await VerifyPageAsync(() => new AddPermissionsForTrainingProviderPage(context, providerConfig));
    }

    //public async Task<AlreadyLinkedToTrainingProviderPage> SearchForAnExistingTrainingProvider(ProviderConfig providerConfig)
    //{
    //    await EnterATrainingProvider(providerConfig);

    //    return await VerifyPageAsync(() => new AlreadyLinkedToTrainingProviderPage(context));
    //}

    private async Task EnterATrainingProvider(ProviderConfig providerConfig)
    {
        await page.Locator("#SearchTerm").FillAsync(providerConfig.Ukprn);

        await page.GetByRole(AriaRole.Option, new() { Name = providerConfig.Ukprn }).ClickAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        //new TrainingProviderAutoCompleteHelper(context).SelectFromAutoCompleteList(providerConfig.Ukprn);

    }
}


public class AddPermissionsForTrainingProviderPage(ScenarioContext context, ProviderConfig providerConfig) : PermissionBasePageForTrainingProviderPage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync($"Add {providerConfig.Name.ToUpperInvariant()} and set permissions");
    }

    public async Task VerifyDoNotAllowPermissions()
    {
        await SetAddApprentice(AddApprenticePermissions.NoToAddApprenticeRecords);

        await SetRecruitApprentice(RecruitApprenticePermissions.NoToRecruitApprentices);

        await Assertions.Expect(page.GetByRole(AriaRole.Alert)).ToContainTextAsync("Error: You must select yes for at least one permission for add apprentice records or recruit apprentices");
    }
}

public enum AddApprenticePermissions
{
    //[ToString("Yes, employer will review records")]
    YesAddApprenticeRecords,
    //[ToString("No")]
    NoToAddApprenticeRecords
}

public enum RecruitApprenticePermissions
{
    //[ToString("Yes")]
    YesRecruitApprentices,
    //[ToString("Yes, employer will review adverts")]
    YesRecruitApprenticesButEmployerWillReview,
    //[ToString("No")]
    NoToRecruitApprentices
}


public abstract class PermissionBasePageForTrainingProviderPage(ScenarioContext context) : RegistrationBasePage(context)
{

    //public async Task<ManageTrainingProvidersPage> AddOrSetPermissions((AddApprenticePermissions cohortpermission, RecruitApprenticePermissions recruitpermission) permisssion)
    //{
    //    await SetAddApprentice(permisssion.cohortpermission);

    //    await SetRecruitApprentice(permisssion.recruitpermission);

    //    return await VerifyPageAsync(() => new ManageTrainingProvidersPage(context));
    //}

    public async Task<EmployerAccountCreatedPage> AddOrSetPermissionsAndCreateAccount((AddApprenticePermissions cohortpermission, RecruitApprenticePermissions recruitpermission) permisssion)
    {
        await SetAddApprentice(permisssion.cohortpermission);

        await SetRecruitApprentice(permisssion.recruitpermission);

        return await VerifyPageAsync(() => new EmployerAccountCreatedPage(context));
    }

    protected async Task SetAddApprentice(AddApprenticePermissions permission)
    {
        if (permission == AddApprenticePermissions.YesAddApprenticeRecords)
        {
            await page.GetByRole(AriaRole.Radio, new() { Name = "Yes, but I want final review" }).CheckAsync();
        }

        if (permission == AddApprenticePermissions.NoToAddApprenticeRecords)
        {
            await page.GetByRole(AriaRole.Group, new() { Name = "Add apprentice records" }).GetByLabel("No").CheckAsync();
        }
    }

    protected async Task SetRecruitApprentice(RecruitApprenticePermissions permission)
    {
        if (permission == RecruitApprenticePermissions.YesRecruitApprentices)
        {
            await page.GetByRole(AriaRole.Radio, new() { Name = "Yes", Exact = true }).CheckAsync();
        }

        if (permission == RecruitApprenticePermissions.YesRecruitApprenticesButEmployerWillReview)
        {
            await page.GetByRole(AriaRole.Radio, new() { Name = "Yes, but I want to review" }).CheckAsync();
        }

        if (permission == RecruitApprenticePermissions.NoToRecruitApprentices)
        {
            await page.GetByRole(AriaRole.Group, new() { Name = "Recruit apprentices" }).GetByLabel("No").CheckAsync();
        }

        await page.GetByRole(AriaRole.Button, new() { Name = "Confirm" }).ClickAsync();
    }
}

public class EmployerAccountCreatedPage(ScenarioContext context) : EmpAccountCreationBase(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Employer account created");

        await SetEasNewUser();
    }

    public async Task<HomePage> SelectGoToYourEmployerAccountHomepage()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Go to your employer account" }).ClickAsync();

        return await VerifyPageAsync(() => new HomePage(context));
    }
}
public class ConfirmPAYESchemePage(ScenarioContext context, string paye) : RegistrationBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Confirm PAYE scheme");

    public async Task<PAYESchemeAddedPage> ClickContinueInConfirmPAYESchemePage()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Yes, continue" }).ClickAsync();

        return await VerifyPageAsync(() => new PAYESchemeAddedPage(context, paye));
    }
}

public class PAYESchemeAddedPage(ScenarioContext context, string paye) : RegistrationBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("#main-content")).ToContainTextAsync($"{paye} has been added");

    public async Task<UsingYourGovtGatewayDetailsPage> SelectAddAnotherPAYEScheme()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "Add another PAYE scheme" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new UsingYourGovtGatewayDetailsPage(context));
    }

    public async Task<HomePage> SelectContinueAccountSetupInPAYESchemeAddedPage()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "Return to homepage" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new HomePage(context));
    }
}
