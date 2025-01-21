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

public abstract class EmployerBasePage : HubBasePage
{
    //protected override By PageHeader => SubPageHeader;

    //protected static By AreTheyRightForYou => By.CssSelector("a[href='/employers/are-they-right-for-you-employers']");

    //protected static By HowDoTheyWork => By.CssSelector("a[href= '/employers/how-do-they-work-for-employers']");

    //protected static By SettingItUp => By.CssSelector("a[href='/employers/setting-it-up']");

    protected EmployerBasePage(ScenarioContext context) : base(context) { }

    //public EmployerAreTheyRightForYouPage NavigateToAreTheyRightForYouPage()
    //{
    //    formCompletionHelper.ClickElement(AreTheyRightForYou);
    //    return new EmployerAreTheyRightForYouPage(context);
    //}

    //public EmployerHowDoTheyWorkPage ClickHowDoTheyWorkLink()
    //{
    //    formCompletionHelper.ClickElement(HowDoTheyWork);
    //    return new EmployerHowDoTheyWorkPage(context);
    //}

    //public SettingItUpPage ClickSettingUpLink()
    //{
    //    formCompletionHelper.ClickElement(SettingItUp);
    //    return new SettingItUpPage(context);
    //}
}

public class EmployerHubPage(ScenarioContext context) : EmployerBasePage(context)
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

    //public SignUpPage NavigateToSignUpPage()
    //{
    //    formCompletionHelper.ClickElement(EmployerSignUpButton);
    //    return new SignUpPage(context);
    //}
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
