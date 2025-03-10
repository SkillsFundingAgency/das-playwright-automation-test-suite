namespace SFA.DAS.EmployerPortal.UITests.Project.Pages;

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
