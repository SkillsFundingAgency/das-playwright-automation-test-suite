namespace SFA.DAS.EmployerPortal.UITests.Project.Pages.CreateAccount;

public class HowMuchIsYourOrgAnnualPayBillPage(ScenarioContext context) : EmployerPortalBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("How much is your organisation's annual pay bill?");

    public async Task<AddAPAYESchemePage> SelectOptionLessThan3Million()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "Less than £3 million" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new AddAPAYESchemePage(context));
    }

    public async Task<AddPayeSchemeUsingGGDetailsPage> SelectOptionCloseTo3Million()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "Close to £3 million" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new AddPayeSchemeUsingGGDetailsPage(context));
    }

    public async Task<CreateYourEmployerAccountPage> GoBackToCreateYourEmployerAccountPage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Back", Exact = true }).ClickAsync();

        return await VerifyPageAsync(() => new CreateYourEmployerAccountPage(context));
    }
}
