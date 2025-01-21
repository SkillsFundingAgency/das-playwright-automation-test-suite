using SFA.DAS.Campaigns.UITests.Project.Tests.Pages.Apprentices;

namespace SFA.DAS.Campaigns.UITests.Project.Tests.Pages;

public abstract class CampaingnsHeaderBasePage(ScenarioContext context) : CampaingnsVerifyLinks(context)
{
    protected ILocator Apprentice => page.GetByLabel("Main navigation").GetByRole(AriaRole.Link, new() { Name = "Apprentices" });

    protected ILocator Employer => page.GetByRole(AriaRole.Link, new() { Name = "Employers" });

    protected ILocator Influencers => page.GetByRole(AriaRole.Link, new() { Name = "Influencers" });

    protected ILocator SiteMap => page.GetByRole(AriaRole.Link, new() { Name = "Sitemap" });

    public async Task<ApprenticeHubPage> NavigateToApprenticeshipHubPage()
    {
        await Apprentice.ClickAsync();

        return await VerifyPageAsync(() => new ApprenticeHubPage(context));
    }

    //public EmployerHubPage NavigateToEmployerHubPage()
    //{
    //    await Employer.ClickAsync();
    //    return new(context);
    //}

    //public InfluencersHubPage NavigateToInfluencersHubPage()
    //{
    //    await Influencers.ClickAsync();
    //    return new(context);
    //}

    public async Task<SiteMapPage> NavigateToSiteMapPage()
    {
        await SiteMap.ClickAsync();

        return await VerifyPageAsync(() => new SiteMapPage(context));
    }
}
