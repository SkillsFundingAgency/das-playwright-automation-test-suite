namespace SFA.DAS.EPAO.UITests.Project.Tests.Pages.AssessmentService;

public class AS_RecordAGradePage(ScenarioContext context) : EPAO_BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Record a grade");


    private readonly EPAOApplySqlDataHelper _ePAOSqlDataHelper = context.Get<EPAOApplySqlDataHelper>();

    //#region Locators
    //private static By FamilyNameTextBox => By.Name("Surname");
    //private static By ULNTextBox => By.Name("Uln");
    //private static By FamilyNameMissingErrorText => By.LinkText("Enter the apprentice's family name");
    //private static By ULNMissingErrorText => By.LinkText("Enter the apprentice's ULN");
    //private static By InvalidUlnErrorText => By.LinkText("The apprentice's ULN should contain exactly 10 numbers");

    //#endregion

    public async Task<AS_AssesmentAlreadyRecorded> GoToAssesmentAlreadyRecordedPage()
    {
        await EnterApprenticeDetailsAndContinue(ePAOAdminDataHelper.FamilyName, ePAOAdminDataHelper.LearnerUln);

        return await VerifyPageAsync(() => new AS_AssesmentAlreadyRecorded(context));
    }

    public async Task<AS_ConfirmApprenticePage> SearchApprentice(bool deleteExistingCertificate, string learnerFamilyName = null, string learnerUln = null)
    {
        if (deleteExistingCertificate)
            await _ePAOSqlDataHelper.DeleteCertificate(learnerUln ?? ePAOAdminDataHelper.LearnerUln);

        await EnterApprenticeDetailsAndContinue(learnerFamilyName ?? ePAOAdminDataHelper.FamilyName, learnerUln ?? ePAOAdminDataHelper.LearnerUln);

        return await VerifyPageAsync(() => new AS_ConfirmApprenticePage(context));
    }

    public async Task EnterApprenticeDetailsAndContinue(string familyName, string uln)
    {
        await page.GetByRole(AriaRole.Textbox, new() { Name = "Family name" }).FillAsync(familyName);
        
        await page.GetByRole(AriaRole.Textbox, new() { Name = "Unique learner number (ULN)" }).FillAsync(uln);

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

    }

    //public void VerifyErrorMessage(string pageTitle) => VerifyElement(PageHeader, pageTitle);

    //public bool VerifyFamilyNameMissingErrorText() => pageInteractionHelper.IsElementDisplayed(FamilyNameMissingErrorText);

    //public bool VerifyULNMissingErrorText() => pageInteractionHelper.IsElementDisplayed(ULNMissingErrorText);

    //public bool VerifyInvalidUlnErrorText() => pageInteractionHelper.IsElementDisplayed(InvalidUlnErrorText);

    //public string GetPageTitle() => pageInteractionHelper.GetText(PageHeader);

    public async Task<AS_CannotFindApprenticePage> EnterApprenticeDetailsForExistingCertificateAndContinue()
    {
        await EnterApprenticeDetailsAndContinue(ePAOAdminDataHelper.FamilyName, ePAOAdminDataHelper.LearnerUlnForExistingCertificate);

        return await VerifyPageAsync(() => new AS_CannotFindApprenticePage(context));
    }
}

public class AS_CannotFindApprenticePage(ScenarioContext context) : EPAO_BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("We cannot find the apprentice details");
}