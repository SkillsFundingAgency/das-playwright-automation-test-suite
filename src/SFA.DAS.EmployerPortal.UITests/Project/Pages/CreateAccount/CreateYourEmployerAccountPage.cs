namespace SFA.DAS.EmployerPortal.UITests.Project.Pages.CreateAccount;

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
