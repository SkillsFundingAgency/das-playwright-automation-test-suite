namespace SFA.DAS.TransferMatching.UITests.Project.Tests.Pages;

public class FindABusinessPage(ScenarioContext context) : TransferMatchingBasePage(context)
{
    protected override string PageTitle => "Find a business to fund training";
    public ILocator StartNowSelector => page.GetByRole(AriaRole.Link, new() { Name = "Start now", Exact = true });

    public async Task<Searchfundingopportunitiespage> GoToOpportunitiesPage()
    {
        await StartNowSelector.ClickAsync();
        return await VerifyPageAsync(() => new Searchfundingopportunitiespage(context));
    }
}
