namespace SFA.DAS.EmployerPortal.UITests.Project.Pages.CreateAccount;

public class AddATrainingProviderPage(ScenarioContext context) : EmployerPortalBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Add a training provider");

    public async Task<EnterYourTrainingProviderNameReferenceNumberUKPRNPage> AddTrainingProviderNow()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "Yes, I'll add a training" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new EnterYourTrainingProviderNameReferenceNumberUKPRNPage(context));
    }

    public async Task<EmployerAccountCreatedPage> AddTrainingProviderLater()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "No, I want to finish setting" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new EmployerAccountCreatedPage(context));
    }

    public async Task<CreateYourEmployerAccountPage> GoBackToCreateYourEmployerAccountPage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Back", Exact = true }).ClickAsync();

        return await VerifyPageAsync(() => new CreateYourEmployerAccountPage(context));
    }
}
