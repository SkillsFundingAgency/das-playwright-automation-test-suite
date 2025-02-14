using Azure;
using NUnit.Framework;
using Polly;
using SFA.DAS.Framework;
using SFA.DAS.Login.Service.Project;
using SFA.DAS.MongoDb.DataGenerator;
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

        return await VerifyPageAsync(() => new YouVeSuccessfullyAddedUserDetailsPage(context));
    }

    public async Task<StubAddYourUserDetailsPage> ClickChange()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Change   first name" }).ClickAsync();

        return await VerifyPageAsync(() => new StubAddYourUserDetailsPage(context));
    }
}

public class YouVeSuccessfullyAddedUserDetailsPage(ScenarioContext context, bool updated = false) : RegistrationBasePage(context)
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

    //#region Locators
    //private static By AcceptInviteLink => By.Id("invitationId");

    //#endregion

    public async Task<HomePage> ClickAcceptInviteLink()
    {
        //formCompletionHelper.Click(AcceptInviteLink);

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

    //public async Task VerifyStepCannotBeStartedYet(string listItemText)
    //{
    //    By stepSelector = By.XPath($"//span[contains(text(), '{listItemText}')]");

    //    var element = pageInteractionHelper.FindElement(stepSelector);
    //    string tagName = element.TagName.ToLower();

    //    Assert.AreNotEqual("a", tagName, "The text has an anchor tag");
    //}

    //public bool CheckIsPageCurrent()
    //{
    //    return pageInteractionHelper.Verify(() =>
    //    {
    //        var result = IsPageCurrent;

    //        return !result.Item1 ? throw new Exception(
    //            MessageHelper.GetExceptionMessage("IsPageCurrent", PageTitle, result.Item2))
    //        : true;

    //    }, null);
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

    //public async Task<TheseDetailsAreAlreadyInUsePage> ClickBackButton()
    //{
    //    await page.GetByRole(AriaRole.Link, new() { Name = "Back", Exact = true }).ClickAsync();
    //    return new TheseDetailsAreAlreadyInUsePage(context);
    //}

}


public class GgSignInPage(ScenarioContext context) : RegistrationBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.GetByRole(AriaRole.Heading)).ToContainTextAsync("Sign in");

    public async Task<SearchForYourOrganisationPage> SignInTo(int index)
    {
        await EnterGateWayCredentialsAndSignIn(index);

        return await VerifyPageAsync(() => new SearchForYourOrganisationPage(context));
    }

    //public async Task<ConfirmPAYESchemePage> EnterPayeDetailsAndContinue(int index)
    //{
    //    var gatewaydetails = await EnterGateWayCredentialsAndSignIn(index);

    //    return new ConfirmPAYESchemePage(context, gatewaydetails.Paye);
    //}

    public async Task SignInWithInvalidDetails()
    {
        await SignInTo(registrationDataHelper.InvalidGGId, registrationDataHelper.InvalidGGPassword);
    }

    //public string GetErrorMessage() => pageInteractionHelper.GetText(ErrorMessageText);

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
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("You have confirmed your Account name");

        await Assertions.Expect(page.GetByRole(AriaRole.Alert)).ToContainTextAsync("Account name confirmed");
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

    //public async Task<EnterYourTrainingProviderNameReferenceNumberUKPRNPage> AddTrainingProviderNow()
    //{
    //    await page.GetByRole(AriaRole.Radio, new() { Name = "Yes, I'll add a training" }).CheckAsync();

    //    Continue();

    //    return await VerifyPageAsync(() => new HomePage(context));
    //    return new Relationships.EnterYourTrainingProviderNameReferenceNumberUKPRNPage(context);
    //}

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

public class EmployerAccountCreatedPage(ScenarioContext context) : EmpAccountCreationBase(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Employer account created");

    public async Task<HomePage> SelectGoToYourEmployerAccountHomepage()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Go to your employer account" }).ClickAsync();

        return await VerifyPageAsync(() => new HomePage(context));
    }
}