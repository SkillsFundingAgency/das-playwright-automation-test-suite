
namespace SFA.DAS.RAAProvider.UITests.Project.Tests.Pages;

public class CreateAVacancyPage(ScenarioContext context) : RaaBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Create a vacancy");
    }

    public async Task<SelectEmployersPage> StartNow()
    {
        await page.Locator("[data-automation='create-vacancy']").ClickAsync();
        return await VerifyPageAsync(() => new SelectEmployersPage(context));
    }

}
