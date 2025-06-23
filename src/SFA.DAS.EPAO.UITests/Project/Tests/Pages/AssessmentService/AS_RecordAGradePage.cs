namespace SFA.DAS.EPAO.UITests.Project.Tests.Pages.AssessmentService;

public class AS_RecordAGradePage(ScenarioContext context) : EPAO_BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Record a grade");

    private readonly EPAOApplySqlDataHelper _ePAOSqlDataHelper = context.Get<EPAOApplySqlDataHelper>();

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

    public async Task VerifyFamilyNameMissingErrorText() => await Assertions.Expect(page.GetByLabel("There is a problem").GetByRole(AriaRole.List)).ToContainTextAsync("Enter the apprentice's family name");

    public async Task VerifyULNMissingErrorText() => await Assertions.Expect(page.GetByLabel("There is a problem").GetByRole(AriaRole.List)).ToContainTextAsync("Enter the apprentice's ULN");

    public async Task VerifyInvalidUlnErrorText() => await Assertions.Expect(page.GetByLabel("There is a problem").GetByRole(AriaRole.Link)).ToContainTextAsync("The apprentice's ULN should contain exactly 10 numbers");

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