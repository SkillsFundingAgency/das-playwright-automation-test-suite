namespace SFA.DAS.RAA.Service.Project.Pages;

public class ReportsDashboardPage(ScenarioContext context) : RaaBasePage(context)
{
    public override async Task VerifyPage()
    {
        string PageTitle = "Reports";
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync(PageTitle);
    }

    public async Task<CreateANewReportPage> ClickCreateAReportLink()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Create a report" }).ClickAsync();
        return await VerifyPageAsync(() => new CreateANewReportPage(context));
    }

    public async Task<ReportsDashboardPage> VerifyCSVDownloadLink()
    {
        const int maxAttempts = 5;
        if (maxAttempts <= 0) throw new ArgumentOutOfRangeException(nameof(maxAttempts));

        var attempts = 0;

        var processingIndicator = page.GetByText("Processing");

        var refreshLink = page.GetByRole(AriaRole.Link, new() { Name = "Refresh" });

        while (await processingIndicator.CountAsync() > 0 && await processingIndicator.IsVisibleAsync() && attempts < maxAttempts)
        {
            attempts++;
            await refreshLink.ClickAsync();
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        }

        var tableDownloadLinks = page.Locator("table a[href]");
        var links = await tableDownloadLinks.AllAsync();

        if (links == null || links.Count == 0)
            throw new Exception("No download links found in the reports table.");

        var firstLink = links.First();
        var href = await firstLink.GetAttributeAsync("href") ?? string.Empty;

        if (!href.Contains("download-csv", StringComparison.OrdinalIgnoreCase))
            throw new Exception($"Expected 'download-csv' in href but was '{href}'");

        return this;
    }
}
