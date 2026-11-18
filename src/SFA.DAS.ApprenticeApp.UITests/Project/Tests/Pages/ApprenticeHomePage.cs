namespace SFA.DAS.ApprenticeApp.UITests.Project.Tests.Pages
{
    public class ApprenticeHomePage : AppBasePage
    {
        private const string WelcomeHeader = "h1.govuk-heading-xl";

        // Navigation Menu
        private readonly ILocator homeTab;
        private readonly ILocator ksbTab;
        private readonly ILocator notesTab;
        private readonly ILocator accountTab;

        // "How to do an apprenticeship" section links
        private readonly ILocator understandingKsbsLink;
        private readonly ILocator offTheJobTrainingLink;
        private readonly ILocator apprenticeshipAssessmentsLink;

        // "Support with your apprenticeship" section links
        private readonly ILocator savedArticlesLink;
        private readonly ILocator studentDiscountsLink;
        private readonly ILocator connectAndNetworkLink;
        private readonly ILocator yourRightsLink;
        private readonly ILocator rolesResponsibilitiesLink;
        private readonly ILocator learningDifficultyLink;
        private readonly ILocator careExperiencedLink;
        private readonly ILocator trainingProviderFeedbackLink;
        private readonly ILocator afterApprenticeshipLink;

        // "Need help?" section links
        private readonly ILocator contactUsLink;
        private readonly ILocator mentalHealthSupportLink;
        private readonly ILocator thinkingOfDroppingOutLink;
        private readonly ILocator checkPayLink;
        private readonly ILocator problemAtWorkLink;

        public ApprenticeHomePage(ScenarioContext context) : base(context)
        {
            // Navigation
            homeTab = page.Locator("a.govuk-service-navigation__link[href='/Support/Index']");
            ksbTab = page.Locator("a.govuk-service-navigation__link[href='/Ksb/Index']");
            notesTab = page.Locator("a.govuk-service-navigation__link[href='/Tasks/Index']");
            accountTab = page.Locator("a.govuk-service-navigation__link[href='/Account/YourAccount']");

            // How to do an apprenticeship
            understandingKsbsLink = page.Locator("a.app-stack__link[href*='understanding-knowledge-skills-and-behaviours']");
            offTheJobTrainingLink = page.Locator("a.app-stack__link[href*='off-the-job-otj-training']");
            apprenticeshipAssessmentsLink = page.Locator("a.app-stack__link[href*='apprenticeship-assessments']");

            // Support with your apprenticeship
            savedArticlesLink = page.Locator("a.app-stack__link[href='/Support/SavedArticles']");
            studentDiscountsLink = page.Locator("a.app-stack__link[href*='get-student-discounts']");
            connectAndNetworkLink = page.Locator("a.app-stack__link[href*='connect-and-network-with-other-apprentices']");
            yourRightsLink = page.Locator("a.app-stack__link[href*='your-rights-as-an-apprentice']");
            rolesResponsibilitiesLink = page.Locator("a.app-stack__link[href*='roles-and-responsibilities']");
            learningDifficultyLink = page.Locator("a.app-stack__link[href*='support-for-a-learning-difficulty-or-disability']");
            careExperiencedLink = page.Locator("a.app-stack__link[href*='support-for-care-experienced-apprentices']");
            trainingProviderFeedbackLink = page.Locator("a.app-stack__link[href*='training-provider-feedback']");
            afterApprenticeshipLink = page.Locator("a.app-stack__link[href*='after-your-apprenticeship']");

            // Need help?
            contactUsLink = page.Locator("a.app-stack__link[href*='contact-us']");
            mentalHealthSupportLink = page.Locator("a.app-stack__link[href*='mental-health-support']");
            thinkingOfDroppingOutLink = page.Locator("a.app-stack__link[href*='thinking-of-dropping-out']");
            checkPayLink = page.Locator("a.app-stack__link[href*='check-your-pay-and-get-help-with-money']");
            problemAtWorkLink = page.Locator("a.app-stack__link[href*='i-have-a-problem-at-work-or-training']");
        }

        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator(WelcomeHeader)).ToContainTextAsync("Welcome");
        }

        public async Task<string> GetWelcomeHeaderAsync()
            => await page.Locator(WelcomeHeader).InnerTextAsync();
        public async Task ClickYourRightsLinkAsync() => await yourRightsLink.ClickAsync();
        public async Task ClickApprenticeshipAssessmentsLinkAsync() => await apprenticeshipAssessmentsLink.ClickAsync();
        public async Task ClickUnderstandingKsbsAsync() => await understandingKsbsLink.ClickAsync();
        public async Task ClickContactUsAsync() => await contactUsLink.ClickAsync();
        public async Task ClickKsbsTabAsync() => await ksbTab.ClickAsync();

        public async Task VerifyAllSupportAndGuidanceLinksAsync()
        {
            var expectedHrefs = new[]
            {
        // How to do an apprenticeship
        "/Support/Category/understanding-knowledge-skills-and-behaviours-ksbs",
        "/Support/Category/off-the-job-otj-training",
        "/Support/Category/apprenticeship-assessments",

        // Support with your apprenticeship
        "/Support/SavedArticles",
        "/Support/Category/get-student-discounts",
        "/Support/Category/connect-and-network-with-other-apprentices",
        "/Support/Category/your-rights-as-an-apprentice",
        "/Support/Category/roles-and-responsibilities",
        "/Support/Category/support-for-a-learning-difficulty-or-disability",
        "/Support/Category/support-for-care-experienced-apprentices",
        "/Support/Category/training-provider-feedback",
        "/Support/Category/after-your-apprenticeship",

        // Need help?
        "/Support/Category/contact-us",
        "/Support/Category/mental-health-support",
        "/Support/Category/thinking-of-dropping-out",
        "/Support/Category/check-your-pay-and-get-help-with-money",
        "/Support/Category/i-have-a-problem-at-work-or-training"
    };

            foreach (var href in expectedHrefs)
            {
                var link = page.Locator($"a.app-stack__link[href='{href}']").First;

                await link.WaitForAsync(new() { State = WaitForSelectorState.Visible, Timeout = 10_000 });

                var isVisible = await link.IsVisibleAsync();

                Assert.That(isVisible, Is.True, $"Expected support link '{href}' to be visible on the Home tab.");
            }
        }
    }
}