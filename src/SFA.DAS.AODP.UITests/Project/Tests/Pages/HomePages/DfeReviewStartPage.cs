
using SFA.DAS.AODP.UITests.Project.Tests.Pages.Common;

namespace SFA.DAS.AODP.UITests.Project.Tests.Pages.AO
{
    public class DfeReviewStartPage(ScenarioContext context) : AodpHomePage(context)
    {

        private ILocator DfeReviewerStartPage => page.GetByText("Review qualifications for funding approval");
        private ILocator NewQualStartButton => page.Locator(".govuk-button--start");
        private ILocator ChangedQualStartButton => page.Locator(".govuk-button--start");

        public override async Task VerifyPage() => await Assertions.Expect(DfeReviewerStartPage).ToBeVisibleAsync();

    }
}
