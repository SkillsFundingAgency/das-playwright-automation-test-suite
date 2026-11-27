namespace SFA.DAS.ApprenticeApp.UITests.Project.Tests.Pages
{
    public class ApprenticeHomePage(ScenarioContext context) : AppBasePage(context)
    {
        private ILocator WelcomeHeader => page.Locator("h1.govuk-heading-xl");

        // Navigation
        private ILocator HomeTab => page.GetByRole(AriaRole.Link, new() { Name = "Home" });
        private ILocator KsbTab => page.GetByRole(AriaRole.Link, new() { Name = "KSBs" });
        private ILocator NotesTab => page.GetByRole(AriaRole.Link, new() { Name = "Notes and evidence" });
        private ILocator AccountTab => page.GetByRole(AriaRole.Link, new() { Name = "Account" });

        // How to do an apprenticeship
        private ILocator UnderstandingKsbsLink => page.Locator("a.app-stack__link[href*='understanding-knowledge-skills-and-behaviours']");
        private ILocator OffTheJobTrainingLink => page.Locator("a.app-stack__link[href*='off-the-job-otj-training']");
        private ILocator ApprenticeshipAssessmentsLink => page.Locator("a.app-stack__link[href*='apprenticeship-assessments']");

        // Support with your apprenticeship
        private ILocator SavedArticlesLink => page.Locator("a.app-stack__link[href='/Support/SavedArticles']");
        private ILocator StudentDiscountsLink => page.Locator("a.app-stack__link[href*='get-student-discounts']");
        private ILocator ConnectAndNetworkLink => page.Locator("a.app-stack__link[href*='connect-and-network-with-other-apprentices']");
        private ILocator YourRightsLink => page.Locator("a.app-stack__link[href*='your-rights-as-an-apprentice']");
        private ILocator RolesResponsibilitiesLink => page.Locator("a.app-stack__link[href*='roles-and-responsibilities']");
        private ILocator LearningDifficultyLink => page.Locator("a.app-stack__link[href*='support-for-a-learning-difficulty-or-disability']");
        private ILocator CareExperiencedLink => page.Locator("a.app-stack__link[href*='support-for-care-experienced-apprentices']");
        private ILocator TrainingProviderFeedbackLink => page.Locator("a.app-stack__link[href*='training-provider-feedback']");
        private ILocator AfterApprenticeshipLink => page.Locator("a.app-stack__link[href*='after-your-apprenticeship']");

        // Need help?
        private ILocator ContactUsLink => page.Locator("a.app-stack__link[href*='contact-us']");
        private ILocator MentalHealthSupportLink => page.Locator("a.app-stack__link[href*='mental-health-support']");
        private ILocator ThinkingOfDroppingOutLink => page.Locator("a.app-stack__link[href*='thinking-of-dropping-out']");
        private ILocator CheckPayLink => page.Locator("a.app-stack__link[href*='check-your-pay-and-get-help-with-money']");
        private ILocator ProblemAtWorkLink => page.Locator("a.app-stack__link[href*='i-have-a-problem-at-work-or-training']");

        public override async Task VerifyPage()
        {
            await Assertions.Expect(WelcomeHeader).ToContainTextAsync("Welcome");
        }

        public async Task<string> GetWelcomeHeaderAsync()
            => await WelcomeHeader.InnerTextAsync();

        public async Task ClickYourRightsLinkAsync() => await YourRightsLink.ClickAsync();
        public async Task ClickApprenticeshipAssessmentsLinkAsync() => await ApprenticeshipAssessmentsLink.ClickAsync();
        public async Task ClickUnderstandingKsbsAsync() => await UnderstandingKsbsLink.ClickAsync();
        public async Task ClickContactUsAsync() => await ContactUsLink.ClickAsync();
        public async Task ClickKsbsTabAsync() => await KsbTab.ClickAsync();

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

                await Assertions.Expect(link).ToBeVisibleAsync();
            }
        }
    }
}
