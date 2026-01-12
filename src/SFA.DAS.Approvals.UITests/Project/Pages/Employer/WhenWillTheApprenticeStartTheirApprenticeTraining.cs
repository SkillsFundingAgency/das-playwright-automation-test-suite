
using SFA.DAS.Approvals.UITests.Project.Pages.Provider;

namespace SFA.DAS.Approvals.UITests.Project.Pages.Employer
{
    internal class WhenWillTheApprenticeStartTheirApprenticeTraining(ScenarioContext context) : ApprovalsBasePage(context)
    {
        public override async Task VerifyPage() => await Assertions.Expect(page.Locator(".govuk-heading-xl").First).ToContainTextAsync("When will the apprentice start their apprenticeship training?");

        private ILocator alreadyStarted => page.Locator("#StartDate-alreadyStarted");

        internal async Task<ConfirmYourReservationPage> SelectAlreadyStartedDate()
        {
            await alreadyStarted.ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();
            return await VerifyPageAsync(() => new ConfirmYourReservationPage(context));
        }

    }
}