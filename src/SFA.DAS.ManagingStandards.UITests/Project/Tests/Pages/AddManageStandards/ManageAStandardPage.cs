namespace SFA.DAS.ManagingStandards.UITests.Project.Tests.Pages.AddManageStandards;

public class ManageAStandardPage(ScenarioContext context, string standardName) : ManagingStandardsBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync(standardName);
    }

    public async Task<AreYouSureDeleteStandardPage> ClickDeleteAStandard()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Delete standard" }).ClickAsync();

        return await VerifyPageAsync(() => new AreYouSureDeleteStandardPage(context));
    }
}
