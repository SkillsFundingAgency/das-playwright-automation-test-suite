using System.Threading.Tasks;
using Microsoft.Playwright;
using Reqnroll;

namespace SFA.DAS.ApprenticeApp.UITests.Project.Tests.Pages
{
    public class SupportGuidancePage : AppBasePage
    {
        private const string SupportGuidanceHeader = "h1.govuk-heading-xl";

        private readonly ILocator _savedArticlesLink;
        private readonly ILocator _yourRightsLink;
        private readonly ILocator _whereToGetSupportLink;
        private readonly ILocator _learningDifficultyLink;
        private readonly ILocator _careExperiencedLink;
        private readonly ILocator _apprenticeshipAssessmentsLink;
        private readonly ILocator _offTheJobTrainingLink;
        private readonly ILocator _connectAndNetworkLink;
        private readonly ILocator _trainingProviderFeedbackLink;
        private readonly ILocator _rolesResponsibilitiesLink;
        private readonly ILocator _studentDiscountsLink;
        private readonly ILocator _afterApprenticeshipLink;

        public SupportGuidancePage(ScenarioContext context) : base(context)
        {
            _savedArticlesLink = page.Locator("a.app-stack__link[href='/Support/SavedArticles']");
            _yourRightsLink = page.Locator("a.app-stack__link[href='/Support/Category/your-rights-as-an-apprentice']");
            _whereToGetSupportLink = page.Locator("a.app-stack__link[href='/Support/Category/where-to-get-support']");
            _learningDifficultyLink = page.Locator("a.app-stack__link[href='/Support/Category/support-for-a-learning-difficulty-or-disability']");
            _careExperiencedLink = page.Locator("a.app-stack__link[href='/Support/Category/support-for-care-experienced-apprentices']");
            _apprenticeshipAssessmentsLink = page.Locator("a.app-stack__link[href='/Support/Category/apprenticeship-assessments']");
            _offTheJobTrainingLink = page.Locator("a.app-stack__link[href='/Support/Category/off-the-job-otj-training']");
            _connectAndNetworkLink = page.Locator("a.app-stack__link[href='/Support/Category/connect-and-network-with-other-apprentices']");
            _trainingProviderFeedbackLink = page.Locator("a.app-stack__link[href='/Support/Category/training-provider-feedback']");
            _rolesResponsibilitiesLink = page.Locator("a.app-stack__link[href='/Support/Category/roles-and-responsibilities']");
            _studentDiscountsLink = page.Locator("a.app-stack__link[href='/Support/Category/get-student-discounts']");
            _afterApprenticeshipLink = page.Locator("a.app-stack__link[href='/Support/Category/after-your-apprenticeship']");
        }

        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator(SupportGuidanceHeader)).ToBeVisibleAsync();
        }

        public async Task<string> SupportGuidancePageTitleAsync()
            => await page.Locator(SupportGuidanceHeader).InnerTextAsync();

        // Interaction helper methods
        public async Task ClickYourRightsLinkAsync() => await _yourRightsLink.ClickAsync();
        public async Task ClickApprenticeshipAssessmentsLinkAsync() => await _apprenticeshipAssessmentsLink.ClickAsync();
    }
}