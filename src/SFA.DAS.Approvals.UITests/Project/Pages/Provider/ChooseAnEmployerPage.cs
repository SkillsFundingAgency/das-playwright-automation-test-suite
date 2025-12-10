namespace SFA.DAS.Approvals.UITests.Project.Pages.Provider;

internal class ChooseAnEmployerPage(ScenarioContext context) : ApprovalsBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Choose an employer");
    }

    internal async Task<ConfirmEmployerPage> ChooseAnEmployer(string agreementId)
    {
        await page.GetByRole(AriaRole.Row, new() { Name = agreementId }).GetByRole(AriaRole.Link).ClickAsync();

        return await VerifyPageAsync(() => new ConfirmEmployerPage(context));

    }


}
