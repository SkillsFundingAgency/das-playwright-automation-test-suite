namespace SFA.DAS.RAA.Service.Project.Pages;

public class CreateANewReportPage(ScenarioContext context) : RaaBasePage(context)
{
    public override async Task VerifyPage()
    {
        string PageTitle = $"Create a report";

        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync(PageTitle);
    }

    public async Task<ReportsDashboardPage> SelectTimePeriodAndContinue()
    {
        await SelectRadioOptionByForAttribute("daterange-7");

        var generateButton = page.GetByRole(AriaRole.Button, new() { Name = "Generate report" });
        await generateButton.ClickAsync();

        var backToDashboard = page.GetByRole(AriaRole.Link, new() { Name = "Back to dashboard" });
        await backToDashboard.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 30000 });
        await backToDashboard.ClickAsync();

        return await VerifyPageAsync(() => new ReportsDashboardPage(context));
    }
}

