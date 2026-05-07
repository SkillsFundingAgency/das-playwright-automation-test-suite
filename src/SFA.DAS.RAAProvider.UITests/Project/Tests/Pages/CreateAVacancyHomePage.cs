
namespace SFA.DAS.RAAProvider.UITests.Project.Tests.Pages;

public class CreateAVacancyHomePage(ScenarioContext context) : RaaBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Create a vacancy for Find an apprenticeship");
    }
    public async Task<CreateAnApprenticeshipAdvertOrVacancyPage> GoToCreateAnApprenticeshipVacancyPage()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Create vacancy" }).ClickAsync();
        return await VerifyPageAsync(() => new CreateAnApprenticeshipAdvertOrVacancyPage(context));
    }
}
