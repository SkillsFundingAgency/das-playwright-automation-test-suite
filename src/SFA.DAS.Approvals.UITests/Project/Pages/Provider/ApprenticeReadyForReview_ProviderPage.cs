using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.Approvals.UITests.Project.Pages.Provider
{
    internal class ApprenticeReadyForReview_ProviderPage(ScenarioContext context) : ApprovalsBasePage(context)
    {
        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Apprentice requests");
        }

        protected void SelectCurrentCohortDetailsFromTable()
        {
           // page.Locator().ScrollIntoViewIfNeededAsync();

           // javaScriptHelper.ScrollToTheBottom();
           // tableRowHelper.SelectRowFromTableDescending("Details", objectContext.GetCohortReference());
        }

    }
}
