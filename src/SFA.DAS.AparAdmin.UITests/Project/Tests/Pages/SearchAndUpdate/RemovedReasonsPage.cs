using Azure;
using SFA.DAS.AparAdmin.UITests.Project.Tests.Pages.SearchAndUpdate;

public class RemovedReasonsPage(ScenarioContext context) : BasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1"))
            .ToContainTextAsync("Why do you want to remove this provider?");
    }

    public async Task <SuccessPage> ConfirmRemovalReasonAndContinue(string value)
    {
        await page.ClickAsync($"input[name='RemovalReasonId'][value='{value}']");
        await page.ClickAsync("button#continue");
        return await VerifyPageAsync(() => new SuccessPage(context));
    }

    public async Task<SuccessPage> YesChangeTypeofOrganisation()
    {
        await page.GetByLabel("Yes").CheckAsync();
        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();
        return await VerifyPageAsync(() => new SuccessPage(context));
    }

    public async Task<ProviderDetailsPage> YesDoNotChangeTypeofOrganisation()
    {
        await page.GetByLabel("No").CheckAsync();
        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();
        return await VerifyPageAsync(() => new ProviderDetailsPage(context));
    }
}
