namespace SFA.DAS.Campaigns.UITests.Project.Tests.Pages.Influencers;

public abstract class InfluencersBasePage(ScenarioContext context) : HubBasePage(context)
{
    private ILocator InfluencersTab => page.GetByLabel("Influencers");

    public async Task<InfluencersHowTheyWorkPage> NavigateToHowDoTheyWorkPage()
    {
        await InfluencersTab.GetByRole(AriaRole.Link, new() { Name = "How they work" }).ClickAsync();

        return await VerifyPageAsync(() => new InfluencersHowTheyWorkPage(context));
    }

    public async Task<InfluencersRequestSupportPage> NavigateToRequestSupportPage()
    {

        await InfluencersTab.GetByRole(AriaRole.Link, new() { Name = "Request support" }).ClickAsync();

        return await VerifyPageAsync(() => new InfluencersRequestSupportPage(context));
    }

    public async Task<InfluencersResourceHubPage> NavigateToResourceHubPage()
    {
        await InfluencersTab.GetByRole(AriaRole.Link, new() { Name = "Resource hub" }).ClickAsync();

        return await VerifyPageAsync(() => new InfluencersResourceHubPage(context));

    }

    public async Task<InfluencersApprenticeAmbassadorNetworkPage> NavigateToApprenticeAmbassadorNetworkPage()
    {
        await InfluencersTab.GetByRole(AriaRole.Link, new() { Name = "Apprenticeship ambassador" }).ClickAsync();

        return await VerifyPageAsync(() => new InfluencersApprenticeAmbassadorNetworkPage(context));

    }
}