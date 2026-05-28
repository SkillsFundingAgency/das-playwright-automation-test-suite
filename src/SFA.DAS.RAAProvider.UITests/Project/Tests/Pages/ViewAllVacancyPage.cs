
namespace SFA.DAS.RAAProvider.UITests.Project.Tests.Pages;

public class ViewAllVacancyPage(ScenarioContext context) : RaaBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("All vacancies");
    }

    public async Task<RecruitmentHomePage> ReturnToDashboard()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Return to recruitment" }).ClickAsync();
        return await VerifyPageAsync(() => new RecruitmentHomePage(context));
    }
}