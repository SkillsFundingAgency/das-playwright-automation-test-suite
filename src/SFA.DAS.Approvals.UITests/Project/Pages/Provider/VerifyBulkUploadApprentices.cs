using System.Collections.Generic;

namespace SFA.DAS.Approvals.UITests.Project.Pages.Provider
{
    public interface IVerifyBulkUploadApprentices
    {
        public Task VerifyCorrectInformationIsDisplayed(List<FileUploadReviewEmployerDetails> apprenticeList);
    }

    internal sealed class VerifyBulkUploadApprentices(ScenarioContext context) : ApprovalsBasePage(context), IVerifyBulkUploadApprentices
    {
        private ILocator CohortRows => page.Locator("tbody tr");

        public override async Task VerifyPage()
        {
            await Assertions.Expect(CohortRows).ToBeVisibleAsync();
        }

        public async Task VerifyCorrectInformationIsDisplayed(List<FileUploadReviewEmployerDetails> apprenticeList)
        {
            var expectedRows = apprenticeList
                .SelectMany(
                 x => x.CohortDetails,
                 (z, y) => new { z.EmployerName, CohortDetails = y })
                .ToList();

            await Assertions.Expect(CohortRows).ToHaveCountAsync(expectedRows.Count);

            for (var index = 0; index < expectedRows.Count; index++)
            {
                var expectedRow = expectedRows[index];
                var row = CohortRows.Nth(index);
                await Assertions.Expect(row.Locator("td[data-label='EmployerName']"))
                    .ToContainTextAsync(expectedRow.EmployerName);
                if (!string.IsNullOrWhiteSpace(expectedRow.CohortDetails.CohortRef))
                {
                    await Assertions.Expect(row.Locator("td[data-label='CohortReference']"))
                        .ToHaveTextAsync(expectedRow.CohortDetails.CohortRef);
                }
                await Assertions.Expect(row.Locator("td[data-label='NumberOfApprenticeships']"))
                    .ToHaveTextAsync(expectedRow.CohortDetails.NumberOfApprenticeshipAndTotalCost);
            }
        }
    }
}
