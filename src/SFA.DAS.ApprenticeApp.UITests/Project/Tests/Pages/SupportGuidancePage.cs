
namespace SFA.DAS.ApprenticeApp.UITests.Project.Tests.Pages
{
    public class SupportGuidancePage : AppBasePage
    {
        private const string SupportGuidanceHeader = "h1.govuk-heading-xl";

        private readonly ILocator savedArticlesLink;
        private readonly ILocator yourRightsLink;
        private readonly ILocator whereToGetSupportLink;
        private readonly ILocator learningDifficultyLink;
        private readonly ILocator careExperiencedLink;
        private readonly ILocator apprenticeshipAssessmentsLink;
        private readonly ILocator offTheJobTrainingLink;
        private readonly ILocator connectAndNetworkLink;
        private readonly ILocator trainingProviderFeedbackLink;
        private readonly ILocator rolesResponsibilitiesLink;
        private readonly ILocator studentDiscountsLink;
        private readonly ILocator afterApprenticeshipLink;

        public SupportGuidancePage(ScenarioContext context) : base(context)
        {
            savedArticlesLink = page.Locator("a.app-stack__link[href='/Support/SavedArticles']");
            yourRightsLink = page.Locator("a.app-stack__link[href='/Support/Category/your-rights-as-an-apprentice']");
            whereToGetSupportLink = page.Locator("a.app-stack__link[href='/Support/Category/where-to-get-support']");
            learningDifficultyLink = page.Locator("a.app-stack__link[href='/Support/Category/support-for-a-learning-difficulty-or-disability']");
            careExperiencedLink = page.Locator("a.app-stack__link[href='/Support/Category/support-for-care-experienced-apprentices']");
            apprenticeshipAssessmentsLink = page.Locator("a.app-stack__link[href='/Support/Category/apprenticeship-assessments']");
            offTheJobTrainingLink = page.Locator("a.app-stack__link[href='/Support/Category/off-the-job-otj-training']");
            connectAndNetworkLink = page.Locator("a.app-stack__link[href='/Support/Category/connect-and-network-with-other-apprentices']");
            trainingProviderFeedbackLink = page.Locator("a.app-stack__link[href='/Support/Category/training-provider-feedback']");
            rolesResponsibilitiesLink = page.Locator("a.app-stack__link[href='/Support/Category/roles-and-responsibilities']");
            studentDiscountsLink = page.Locator("a.app-stack__link[href='/Support/Category/get-student-discounts']");
            afterApprenticeshipLink = page.Locator("a.app-stack__link[href='/Support/Category/after-your-apprenticeship']");
        }

        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator(SupportGuidanceHeader)).ToBeVisibleAsync();
        }

        public async Task<string> SupportGuidancePageTitleAsync()
            => await page.Locator(SupportGuidanceHeader).InnerTextAsync();

        // Interaction helper methods
        public async Task ClickYourRightsLinkAsync() => await yourRightsLink.ClickAsync();
        public async Task ClickApprenticeshipAssessmentsLinkAsync() => await apprenticeshipAssessmentsLink.ClickAsync();
    }
}