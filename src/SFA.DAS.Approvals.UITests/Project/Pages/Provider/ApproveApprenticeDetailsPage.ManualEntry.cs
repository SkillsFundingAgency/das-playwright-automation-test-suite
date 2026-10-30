using SFA.DAS.Approvals.UITests.Project.Helpers.DataHelpers.ApprenticeshipModel;
using System.Text.RegularExpressions;

namespace SFA.DAS.Approvals.UITests.Project.Pages.Provider;

internal partial class ApproveApprenticeDetailsPage
{
    internal async Task VerifyManualEntryPage() =>
        await Assertions.Expect(page.Locator(".govuk-heading-xl").First)
            .ToContainTextAsync(new Regex(@"^(?:Approve (?:apprentice details|\d+ apprentices' details)|Check (?:learner details|\d+ learner's details))$"));

    internal async Task<ManualApprenticeshipCoursePage> AddManualApprentice()
    {
        await page.Locator(".govuk-link.add-apprentice").ClickAsync();
        await Assertions.Expect(page.GetByRole(AriaRole.Heading).Filter(new() { HasText = "Add apprentice details" })).ToBeVisibleAsync();
        await page.Locator("#confirm-Manual").CheckAsync();
        await page.GetByRole(AriaRole.Button, new() { Name = "Continue", Exact = true }).ClickAsync();
        return await VerifyPageAsync(() => new ManualApprenticeshipCoursePage(context, true));
    }

    internal async Task EnsureManualApprenticeUlns(IReadOnlyList<Apprenticeship> apprentices)
    {
        await Assertions.Expect(page.Locator(".edit-apprentice")).ToHaveCountAsync(apprentices.Count);
        for (var index = 0; index < apprentices.Count; index++)
        {
            var row = page.Locator("tbody tr").Filter(new() { HasText = apprentices[index].ApprenticeDetails.FullName });
            var uln = row.Locator("td").Nth(1);
            await Assertions.Expect(uln).ToBeVisibleAsync();
            if ((await uln.InnerTextAsync()).Trim() == "-")
            {
                await row.Locator(".edit-apprentice").ClickAsync();
                var edit = await VerifyPageAsync(() => new EditLearnerDetails_ProviderPage(context));
                await edit.AddManualEntryUln(apprentices[index].ApprenticeDetails.ULN);
                var rpl = await VerifyPageAsync(() => new RecognitionOfPriorLearningPage(context));
                await rpl.SelectNoForManualEntryRPL();
            }
            await Assertions.Expect(row.Locator("td").Nth(1)).ToHaveTextAsync(apprentices[index].ApprenticeDetails.ULN);
        }
    }

    internal async Task ApproveManualCohort()
    {
        await page.Locator("#radio-approve").CheckAsync();
        await page.Locator("#continue-button").ClickAsync();
        await VerifyPageAsync(() => new ManualCohortConfirmationPage(context, "Cohort approved"));
    }
}
