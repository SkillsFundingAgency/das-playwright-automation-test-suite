using Azure;
using System;
using System.Collections.Generic;
using System.Text;

namespace SFA.DAS.Approvals.UITests.Project.Pages.Provider
{
    internal class HowManyLearnersWouldYouLikeToAddPage(ScenarioContext context) : ApprovalsBasePage(context)
    {

        #region locators
        private ILocator optionToAdd1LearnerAtATime => page.GetByRole(AriaRole.Radio, new() { Name = "learner at a time" });
        private ILocator optionToAdd2OrMoreLearnersAtOnce => page.GetByRole(AriaRole.Radio, new() { Name = "or more learners at once" });
        private ILocator ContinueButton => page.GetByRole(AriaRole.Button, new() { Name = "Continue" });
        #endregion

        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("How many learners would you like to add?");
        }

        internal async Task<ILRAddLearnersPage> SelectOptionToAddOneLearnerAtATime()
        {
            await optionToAdd1LearnerAtATime.CheckAsync();
            await ContinueButton.ClickAsync();
            return await VerifyPageAsync(() => new ILRAddLearnersPage(context));
        }

        internal async Task<ILRAddLearnersPage> SelectOptionToAddTwoOrMoreLearnersAtOnce()
        {
            await optionToAdd2OrMoreLearnersAtOnce.CheckAsync();
            await ContinueButton.ClickAsync();
            return await VerifyPageAsync(() => new ILRAddLearnersPage(context));
        }

    }
}
