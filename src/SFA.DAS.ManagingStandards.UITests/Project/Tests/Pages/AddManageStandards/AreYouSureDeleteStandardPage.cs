namespace SFA.DAS.ManagingStandards.UITests.Project.Tests.Pages.AddManageStandards;

public class AreYouSureDeleteStandardPage(ScenarioContext context) : ManagingStandardsBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Are you sure you want to delete this standard?");
    }

    public async Task<ManageTheStandardsYouDeliverPage> DeleteStandard()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Delete standard" }).ClickAsync();

        return await VerifyPageAsync(() => new ManageTheStandardsYouDeliverPage(context));
    }
}
