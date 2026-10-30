namespace SFA.DAS.TransferMatching.UITests.Project.Tests.Pages;

public class MyApplicationsPage(ScenarioContext context) : TransferMatchingBasePage(context)
{
    protected override string PageTitle => "My applications";

    public async Task<ApplicationsDetailsPage> OpenPledgeApplication(string expectedStatus)
    {
        var pledge = page.GetByRole(AriaRole.Link, new() { Name = $"({GetPledgeId()})" });
        await FindOnPaginatedList(pledge);
        await pledge.ClickAsync();
        return await VerifyPageAsync(() => new ApplicationsDetailsPage(context, expectedStatus));
    }
}
