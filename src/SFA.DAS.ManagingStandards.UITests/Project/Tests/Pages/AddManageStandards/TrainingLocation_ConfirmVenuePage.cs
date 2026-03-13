namespace SFA.DAS.ManagingStandards.UITests.Project.Tests.Pages.AddManageStandards;

public class TrainingLocation_ConfirmVenuePage(ScenarioContext context) : ManagingStandardsBasePage(context)
{

    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Training venues");
    }

    public async Task<ManageAStandard_TeacherPage> ConfirmVenueDetailsAndDeliveryMethod_AtOneOFYourTrainingLocation()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new ManageAStandard_TeacherPage(context));
    }

    public async Task<AnyWhereInEnglandPage> ConfirmVenueDetailsAndDeliveryMethod_AtBoth()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new AnyWhereInEnglandPage(context));
    }

}
