using Azure;
using System.Linq;

namespace SFA.DAS.Campaigns.UITests.Project.Tests.Pages.Apprentices;

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

public abstract class EmployerBasePage(ScenarioContext context) : HubBasePage(context)
{
}

public class EmployerHubPage(ScenarioContext context) : HubBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Hire an apprentice");

    //protected static By SearchForAnApprenticeship => By.CssSelector("#fiu-panel-link-fat");

    //protected static By EmployerSignUpButton => By.CssSelector("a[href='/employers/sign-up']");

    public async Task VerifySubHeadings() => await VerifyFiuCards(() => NavigateToEmployerHubPage());

    public async Task<UnderstandingApprenticeshipBenefitsFundingPage> NavigateToUnderstandingApprenticeshipBenefitsAndFunding()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Understanding apprenticeship" }).ClickAsync();

        return new UnderstandingApprenticeshipBenefitsFundingPage(context);
    }

    //public SearchForAnApprenticeshipPage NavigateToFindAnApprenticeshipPage()
    //{
    //    formCompletionHelper.ClickElement(SearchForAnApprenticeship);
    //    return new SearchForAnApprenticeshipPage(context);
    //}

    public async Task<SignUpPage> NavigateToSignUpPage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Sign up to emails" }).ClickAsync();

        return await VerifyPageAsync(() => new SignUpPage(context));
    }
}

public class SignUpPage(ScenarioContext context) : CampaingnsVerifyLinks(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Get emails about taking on your first apprentice");

    public async Task YourDetails()
    {
        await page.GetByLabel("First name").FillAsync(campaignsDataHelper.Firstname);
        await page.GetByLabel("Last name").FillAsync(campaignsDataHelper.Lastname);
        await page.GetByLabel("Email address").FillAsync(campaignsDataHelper.Email);
    }

    public async Task SelectCompanySizeOption1() => await page.GetByLabel("Less than 10 employees").CheckAsync();
    public async Task SelectCompanySizeOption2() => await page.Locator("#between10and49employees").CheckAsync();
    public async Task SelectCompanySizeOption3() => await page.Locator("#between50and249employees").CheckAsync();
    public async Task SelectCompanySizeOption4() => await page.Locator("#over250employees").CheckAsync();

    public async Task<ThanksForSubscribingPage> RegisterInterest()
    {
        var industryAllOptions = await page.GetByLabel("Industry").Locator("option").AllTextContentsAsync();

        var industryoption = GetRandomOption(industryAllOptions.ToList().Where(x => x != "Choose your industry").ToList());

        await page.GetByLabel("Industry").SelectOptionAsync([industryoption]);

        var regionAllOptions = await page.GetByLabel("Region").Locator("option").AllTextContentsAsync();

        var regionoption = GetRandomOption(regionAllOptions.ToList().Where(x => x != "Choose your location").ToList());

        await page.GetByLabel("Region").SelectOptionAsync([regionoption]);

        await page.GetByLabel("I'am happy to be take part in").CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Sign up" }).ClickAsync();

        return await VerifyPageAsync(() => new ThanksForSubscribingPage(context));
    }

    private static string GetRandomOption(List<string> options) => RandomDataGenerator.GetRandomElementFromListOfElements(options);
}

public class ThanksForSubscribingPage(ScenarioContext context) : CampaingnsVerifyLinks(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Thank you for signing up");
}

public class UnderstandingApprenticeshipBenefitsFundingPage(ScenarioContext context) : EmployerBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Understanding apprenticeship benefits and funding");

    public async Task SelectUnder3Million() => await CalculateFunding(false);

    public async Task SelectOver3Million() => await CalculateFunding(true);

    private async Task CalculateFunding(bool IsOver3Million)
    {
        await driver.Page.GetByLabel(IsOver3Million ? "Over £3 million" : "Under £3 million").CheckAsync();

        await driver.Page.GetByLabel("What training course do you").FillAsync("soft");

        await driver.Page.GetByRole(AriaRole.Option, new() { Name = "Software developer (Level 4)" }).ClickAsync();

        await driver.Page.GetByLabel("How many roles do you have").FillAsync("2");

        await driver.Page.GetByRole(AriaRole.Button, new() { Name = "Calculate funding" }).ClickAsync();

        await Assertions.Expect(driver.Page.Locator("#funding")).ToContainTextAsync("Your estimated funding");
    }
}

public class InfluencersHubPage(ScenarioContext context) : InfluencersBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Inspire and Influence");

    public async Task VerifySubHeadings() => await VerifyFiuCards(() => NavigateToInfluencersHubPage());

    public async Task<BrowseApprenticeshipPage> NavigateToBrowseApprenticeshipPage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Browse apprenticeships" }).ClickAsync();

        return await VerifyPageAsync(() => new BrowseApprenticeshipPage(context));
    }
}

public abstract class InfluencersBasePage(ScenarioContext context) : HubBasePage(context)
{
    public async Task<InfluencersHowTheyWorkPage> NavigateToHowDoTheyWorkPage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "How they work" }).ClickAsync();

        return await VerifyPageAsync(() => new InfluencersHowTheyWorkPage(context));
    }

    public async Task<InfluencersRequestSupportPage> NavigateToRequestSupportPage()
    {

        await page.GetByRole(AriaRole.Link, new() { Name = "Request support" }).ClickAsync();

        return await VerifyPageAsync(() => new InfluencersRequestSupportPage(context));
    }

    public async Task<InfluencersResourceHubPage> NavigateToResourceHubPage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Resource hub" }).ClickAsync();

        return await VerifyPageAsync(() => new InfluencersResourceHubPage(context));
        
    }

    public async Task<InfluencersApprenticeAmbassadorNetworkPage> NavigateToApprenticeAmbassadorNetworkPage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Apprenticeship ambassador" }).ClickAsync();

        return await VerifyPageAsync(() => new InfluencersApprenticeAmbassadorNetworkPage(context));
        
    }
}

public class InfluencersHowTheyWorkPage(ScenarioContext context) : InfluencersBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("How they work");

    public async Task<InfluencersHowTheyWorkPage> VerifyInfluencersHowTheyWorkPageSubHeadings() => await VerifyFiuCards(() => NavigateToHowDoTheyWorkPage());
}

public class InfluencersRequestSupportPage(ScenarioContext context) : InfluencersBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Request support");

    public async Task<InfluencersRequestSupportPage>  VerifySubHeadings() => await VerifyFiuCards(() => NavigateToRequestSupportPage());
}

public class InfluencersResourceHubPage(ScenarioContext context) : InfluencersBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Resource hub");

    public async Task<InfluencersResourceHubPage> VerifySubHeadings() => await VerifyFiuCards(() => NavigateToResourceHubPage());
}

public class InfluencersApprenticeAmbassadorNetworkPage(ScenarioContext context) : InfluencersBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Apprenticeship ambassador network");

    public async Task<InfluencersApprenticeAmbassadorNetworkPage> VerifySubHeadings() => await VerifyFiuCards(() => NavigateToApprenticeAmbassadorNetworkPage());
}
