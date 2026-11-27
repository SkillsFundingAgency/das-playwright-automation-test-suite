namespace SFA.DAS.Approvals.UITests.Project.Pages.Employer;

internal class SelectTransferConnectionPage(ScenarioContext context) : ApprovalsBasePage(context)
{
    public override async Task VerifyPage() =>
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Select a connection");

    internal async Task<ChooseYourMainTrainingProviderPage> SelectSender(string sender)
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = sender, Exact = true }).CheckAsync();
        await page.Locator("#submit-transfer-connection").ClickAsync();
        return await VerifyPageAsync(() => new ChooseYourMainTrainingProviderPage(context));
    }
}
