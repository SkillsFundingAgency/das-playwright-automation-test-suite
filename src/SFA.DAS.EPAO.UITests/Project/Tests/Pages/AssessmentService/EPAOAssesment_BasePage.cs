namespace SFA.DAS.EPAO.UITests.Project.Tests.Pages.AssessmentService;

public abstract class EPAOAssesment_BasePage(ScenarioContext context) : EPAO_BasePage(context)
{
    public async Task<AS_CheckAndSubmitAssessmentPage> ClickBackLink()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Back", Exact = true }).ClickAsync();

        return await VerifyPageAsync(() => new AS_CheckAndSubmitAssessmentPage(context));
    }
}
