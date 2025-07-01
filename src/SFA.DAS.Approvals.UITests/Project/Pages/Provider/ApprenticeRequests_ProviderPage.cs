using Azure;
using SFA.DAS.Approvals.UITests.Project.Helpers.DataHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.Approvals.UITests.Project.Pages.Provider
{
    internal class ApprenticeRequests_ProviderPage(ScenarioContext context) : ApprovalsBasePage(context)
    {
        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Apprentice requests");
        }

        internal async Task NavigateToBingoBoxAndVerifyCohortExists(ApprenticeRequests apprenticeRequests, string cohortRef)
        {
            switch (apprenticeRequests)
            {
                case ApprenticeRequests.ReadyForReview:
                    await page.GetByRole(AriaRole.Link, new() { Name = "Ready for review" }).ClickAsync();
                    break;
                case ApprenticeRequests.WithEmployers:
                    await page.GetByRole(AriaRole.Link, new() { Name = "With employers" }).ClickAsync();
                    break;
                case ApprenticeRequests.Drafts:
                    await page.GetByRole(AriaRole.Link, new() { Name = "Drafts" }).ClickAsync();
                    break;
                case ApprenticeRequests.WithTransferSendingEmployers:
                    await page.GetByRole(AriaRole.Link, new() { Name = "With transfer sending employers" }).ClickAsync();
                    break;
            }

            int count = await page.Locator($"#details_link_{cohortRef}").CountAsync();

            if (count == 0)
            {
                throw new Exception($"Cohort with reference {cohortRef} not found in {apprenticeRequests} section.");
            }

            
        }

        internal async Task<ApproveApprenticeDetailsPage> OpenEditableCohortAsync(string cohortRef)
        {
            await page.Locator($"#details_link_{cohortRef}").ClickAsync();
            return await VerifyPageAsync(() => new ApproveApprenticeDetailsPage(context));
        }

        internal async Task<ViewApprenticeDetails_ProviderPage> OpenNonEditableCohortAsync(string cohortRef)
        {
            await page.Locator($"#details_link_{cohortRef}").ClickAsync();
            return await VerifyPageAsync(() => new ViewApprenticeDetails_ProviderPage(context));
        }

    }
}
