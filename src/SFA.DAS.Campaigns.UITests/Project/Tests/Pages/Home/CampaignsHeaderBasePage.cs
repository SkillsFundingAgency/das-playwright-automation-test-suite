using Azure;
using SFA.DAS.Campaigns.UITests.Project.Tests.Pages.Apprentices;
using SFA.DAS.Campaigns.UITests.Project.Tests.Pages.Employer;

namespace SFA.DAS.Campaigns.UITests.Project.Tests.Pages.Home;

public abstract class CampaignsHeaderBasePage(ScenarioContext context) : CampaignsVerifyLinks(context)
{
    protected ILocator Apprentice => page.GetByLabel("Main navigation").GetByRole(AriaRole.Link, new() { Name = "Apprentices" });
    protected ILocator Employer => page.GetByLabel("Main navigation").GetByRole(AriaRole.Link, new() { Name = "Employers" });
    protected ILocator SiteMap => page.GetByRole(AriaRole.Link, new() { Name = "Sitemap" });

    public async Task<ApprenticeHubPage> NavigateToApprenticeshipHubPage()
    {
        await Apprentice.ClickAsync();
        var hubPage = new ApprenticeHubPage(context);
        await hubPage.VerifyPage();
        return hubPage;
    }

    public async Task<EmployerHubPage> NavigateToEmployerHubPage()
    {
        await Employer.ClickAsync();
        var hubPage = new EmployerHubPage(context);
        await hubPage.VerifyPage();
        return hubPage;
    }

    public async Task<SiteMapPage> NavigateToSiteMapPage()
    {
        await SiteMap.ClickAsync();
        var siteMap = new SiteMapPage(context);
        await siteMap.VerifyPage();
        return siteMap;
    }
}