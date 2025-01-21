﻿using Gherkin;
using Microsoft.Playwright;
using SFA.DAS.Framework;
using SFA.DAS.FrameworkHelpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.CommonModels;

namespace SFA.DAS.Campaigns.UITests.Project.Tests.Pages
{

    public class CampaingnsHomePage(ScenarioContext context) : CampaingnsHeaderBasePage(context)
    {
        public override async Task VerifyPage() => await Assertions.Expect(page.GetByRole(AriaRole.Link, new() { Name = "Apprentices", Exact = true })).ToBeVisibleAsync();

        public async Task<CampaingnsHomePage> AcceptCookieAndAlert()
        {
            await page.GetByRole(AriaRole.Button, new() { Name = "Accept all cookies" }).ClickAsync();

            await page.Locator("#fiu-cb-close-accept").ClickAsync();

            return await VerifyPageAsync(() => new CampaingnsHomePage(context));
        }

        public async Task<ApprenticeHomePage> GoToApprenticePage()
        {
            await page.GetByRole(AriaRole.Link, new() { Name = "Learn more becoming an" }).ClickAsync();

            return await VerifyPageAsync(() => new ApprenticeHomePage(context));
        }
    }

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

    public class SiteMapPage(ScenarioContext context) : ApprenticeBasePage(context)
    {
        public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Sitemap");
    }

    public class CampaingnsVerifyLinks(ScenarioContext context) : CampaingnsBasePage(context)
    {
        public override async Task VerifyPage() => await Task.CompletedTask;

        private ILocator Links => page.GetByRole(AriaRole.Link);

        private ILocator VideoLinks => page.Locator(".fiu-video-player iframe");

        public async Task VerifyLinks() => await VerifyLinks(Links, AttributeHelper.Href, async (x) => await x.TextContentAsync());

        public async Task VerifyVideoLinks() => await VerifyLinks(VideoLinks, "src", async (x) => await x?.GetAttributeAsync("title"));

        public async Task VerifyLinks(ILocator locator, string attributeName, Func<ILocator, Task<string>> func)
        {
            var internalLinks = await locator.AllAsync();

            foreach (var item in internalLinks)
            {
                var attributeValue = await item.GetAttributeAsync(attributeName);

                var text = await func(item);

                var msg = $"'{text}' element's, '{attributeName}' attribute - attributeValue : '{attributeValue}'";

                if (string.IsNullOrEmpty(attributeValue) && !string.IsNullOrEmpty(text))
                    throw new Exception($"{msg} is broken");

                objectContext.SetDebugInformation(msg);
            }
        }
    }

    public abstract class HubBasePage(ScenarioContext context) : CampaingnsHeaderBasePage(context)
    {
        protected async Task<T> VerifyFiuCards<T>(Func<Task<T>> func)
        {
            List<Exception> exceptions = [];

            T result = default;

            var fiuCardsHeading = await GetFiuCardsHeadings();

            foreach (var fiuCardHeading in fiuCardsHeading)
            {
                try
                {
                    await page.GetByRole(AriaRole.Link, new() { Name = fiuCardHeading }).ClickAsync();

                    var nextPageheading = fiuCardHeading.Contains("Career starter apprenticeships") ? "Career starter apprenticeships" : fiuCardHeading;

                    var campaingnsDynamicFiuPage = new CampaingnsDynamicFiuPage(context, nextPageheading);

                    await campaingnsDynamicFiuPage.VerifyPageAsync();
                }
                catch (Exception ex)
                {
                    exceptions.Add(ex);
                }
                finally
                {
                    result = await func.Invoke();
                }
            }

            if (exceptions.Count > 0) throw new Exception(exceptions.ExceptionToString());

            return result;
        }

        private async Task<IReadOnlyList<string>> GetFiuCardsHeadings()
        {
            return await page.Locator("a.fiu-card__link").AllTextContentsAsync();
        }
    }

    public class CampaingnsDynamicFiuPage(ScenarioContext context, string pageTitle) : CampaingnsHeaderBasePage(context)
    {
        public readonly string PageTitle = pageTitle;

        public override async Task VerifyPage() => await Task.CompletedTask;

        public async Task VerifyPageAsync()
        {
            await Assertions.Expect(page.Locator("h1")).ToContainTextAsync(PageTitle);

            await VerifyLinks();

            await VerifyVideoLinks();
        }
    }

    public class ApprenticeAreTheyRightForYouPage(ScenarioContext context) : ApprenticeBasePage(context)
    {
        public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Are they right for you?");

        public async Task<ApprenticeAreTheyRightForYouPage> VerifyApprenticeAreTheyRightForYouPageSubHeadings() => await VerifyFiuCards(() => NavigateToAreApprenticeShipRightForMe());
    }

    public class ApprenticeHowDoTheyWorkPage(ScenarioContext context) : ApprenticeBasePage(context)
    {
        public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("How do they work?");

        public async Task<ApprenticeHowDoTheyWorkPage> VerifyHowDoTheyWorkPageSubHeadings() => await VerifyFiuCards(() => NavigateToHowDoTheyWorkPage());
    }

    public class GettingStartedPage(ScenarioContext context) : ApprenticeBasePage(context)
    {
        public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Get started");
    }

    public class BrowseApprenticeshipPage(ScenarioContext context) : ApprenticeBasePage(context)
    {
        public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Browse apprenticeships before you apply");

        public async Task<ResultsPage> SearchForAnApprenticeship()
        {
            await page.GetByLabel("Select an interest").SelectOptionAsync(["Digital"]);

            await page.GetByLabel("Enter your postcode").FillAsync("CV1 2WT");

            await page.GetByRole(AriaRole.Button, new() { Name = "Search" }).ClickAsync();

            return await VerifyPageAsync(() => new ResultsPage(context));
        }
    }

    public class ResultsPage(ScenarioContext context) : ApprenticeBasePage(context)
    {
        public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Results");
    }


    public abstract class ApprenticeBasePage(ScenarioContext context) : HubBasePage(context)
    {

        private ILocator ApprenticeTab => page.GetByLabel("Apprentices");

        public async Task<ApprenticeAreTheyRightForYouPage> NavigateToAreApprenticeShipRightForMe()
        {
            await ApprenticeTab.GetByRole(AriaRole.Link, new() { Name = "Are they right for you?" }).ClickAsync();

            return await VerifyPageAsync(() => new ApprenticeAreTheyRightForYouPage(context));
        }

        public async Task<ApprenticeHowDoTheyWorkPage> NavigateToHowDoTheyWorkPage()
        {
            await ApprenticeTab.GetByRole(AriaRole.Link, new() { Name = "How do they work?" }).ClickAsync();

            return await VerifyPageAsync(() => new ApprenticeHowDoTheyWorkPage(context));
        }

        public async Task<GettingStartedPage> NavigateToGettingStarted()
        {
            await ApprenticeTab.GetByRole(AriaRole.Link, new() { Name = "Get started" }).ClickAsync();

            return await VerifyPageAsync(() => new GettingStartedPage(context));
        }

        public async Task<BrowseApprenticeshipPage> NavigateToBrowseApprenticeshipPage()
        {
            await ApprenticeTab.GetByRole(AriaRole.Link, new() { Name = "Browse apprenticeships" }).ClickAsync();

            return await VerifyPageAsync(() => new BrowseApprenticeshipPage(context));
        }
    }

    public class ApprenticeHubPage(ScenarioContext context) : ApprenticeBasePage(context)
    {
        public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Become an apprentice");

        protected ILocator SetUpService => page.GetByRole(AriaRole.Link, new() { Name = "Create an account", Exact = true });

        public async Task VerifySubHeadings() => await VerifyFiuCards(() => NavigateToApprenticeshipHubPage());

        public async Task<SetUpServicePage> NavigateToSetUpServiceAccountPage()
        {
            await SetUpService.ClickAsync();

            return await VerifyPageAsync(() => new SetUpServicePage(context));
        }

        public async Task<CampaingnsDynamicFiuPage> NavigateToApprenticeStories()
        {
            await page.GetByRole(AriaRole.Link, new() { Name = "Apprentice stories" }).ClickAsync();

            return new CampaingnsDynamicFiuPage(context, "Apprentice stories");
        }
    }


    public class SetUpServicePage(ScenarioContext context) : ApprenticeBasePage(context)
    {
        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Create an account to search and apply for apprenticeships");
        }
    }
}
