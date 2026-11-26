namespace SFA.DAS.RAAEmployer.UITests.Project.Tests.Pages;

public class CreateAnAdvertHomePage(ScenarioContext context) : RaaBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Create an advert for Find an apprenticeship");
    }

    public async Task<CreateAnApprenticeshipAdvertOrVacancyPage> GoToCreateAnApprenticeshipAdvertPage()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Start now" }).ClickAsync();

        return await VerifyPageAsync(() => new CreateAnApprenticeshipAdvertOrVacancyPage(context));
    }
}
