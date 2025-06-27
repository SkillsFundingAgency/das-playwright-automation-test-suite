namespace SFA.DAS.EPAO.UITests.Project.Tests.Pages.AssessmentService;

public class AS_CompletedAssessmentsPage(ScenarioContext context) : EPAO_BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Completed assessments");

    public async Task VerifyTableHeaders()
    {
        await Assertions.Expect(page.Locator("thead")).ToContainTextAsync("Apprentice");
        await Assertions.Expect(page.Locator("thead")).ToContainTextAsync("ULN");
        await Assertions.Expect(page.Locator("thead")).ToContainTextAsync("Employer");
        await Assertions.Expect(page.Locator("thead")).ToContainTextAsync("Training provider");
        await Assertions.Expect(page.Locator("thead")).ToContainTextAsync("Date requested");
    }
}
