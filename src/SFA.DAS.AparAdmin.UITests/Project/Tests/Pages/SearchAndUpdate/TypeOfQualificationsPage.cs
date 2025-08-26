using SFA.DAS.AparAdmin.UITests.Project.Tests.Pages.SearchAndUpdate;

public class TypeOfQualificationsPage(ScenarioContext context) : BasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1"))
            .ToContainTextAsync("Choose what type of qualifications they offer");
    }

    public async Task<SuccessPage> YesForBootcamps()
    {
        await page.GetByLabel("Yes").CheckAsync();
        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();
        return await VerifyPageAsync(() => new SuccessPage(context));
    }

    public async Task<ProviderDetailsPage> CacnelTheTypeOFQualifications()
    {
        await page.GetByLabel("No").CheckAsync();
        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();
        return await VerifyPageAsync(() => new ProviderDetailsPage(context));
    }
}
