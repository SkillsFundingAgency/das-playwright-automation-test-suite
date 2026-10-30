namespace SFA.DAS.TransferMatching.UITests.Project.Tests.Pages;

public class Searchfundingopportunitiespage(ScenarioContext context) : TransferMatchingBasePage(context)
{
    protected override string PageTitle => "Search funding opportunities";

    public async Task<Searchfundingopportunitiespage> SelectAndApplyFilters()
    {
        await page.Locator("xpath=/html/body/div[3]/main/div[2]/form/div[1]/details/summary").ClickAsync();
        foreach (var id in new[] { "Sectors", "Sectors-3", "Sectors-5", "Sectors-14", "Sectors-16" })
            await page.Locator($"[id='{id}']").CheckAsync();
        await page.Locator("#opportunity-filter-sector").ClickAsync();
        return await VerifyPageAsync(() => this);
    }
}
