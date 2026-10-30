using SFA.DAS.Approvals.UITests.Project.Helpers.DataHelpers.ApprenticeshipModel;
using System.Globalization;
using System.Text.RegularExpressions;

namespace SFA.DAS.Approvals.UITests.Project.Pages.Employer;

internal partial class EmployerApproveLearnerDetailsPage
{
    internal async Task VerifyManualEntryPage() =>
        await Assertions.Expect(page.Locator(".govuk-heading-l, .govuk-heading-xl").First)
            .ToContainTextAsync(new Regex(@"^(?:Approve (?:apprentice details|\d+ apprentices' details)|Check and approve (?:learner details|\d+ learners' details))$"));

    internal async Task VerifyManualApprentices(IReadOnlyList<Apprenticeship> apprentices)
    {
        await Assertions.Expect(page.Locator(".edit-apprentice")).ToHaveCountAsync(apprentices.Count);
        foreach (var apprentice in apprentices)
        {
            var row = page.GetByRole(AriaRole.Row).Filter(new() { HasText = apprentice.ApprenticeDetails.FullName });
            await Assertions.Expect(row).ToContainTextAsync(apprentice.TrainingDetails.TotalPrice.ToString("C0", CultureInfo.GetCultureInfo("en-GB")));
        }
    }

    internal async Task<ManualApprenticeshipCoursePage> AddAnotherManualApprentice()
    {
        await page.GetByRole(AriaRole.Link, new() { NameRegex = new Regex("^Add another (?:apprentice|learner)$") }).ClickAsync();
        return await VerifyPageAsync(() => new ManualApprenticeshipCoursePage(context, false));
    }

    internal async Task<ManualCohortConfirmationPage> SendManualCohortToProvider(bool approve, string message)
    {
        await page.Locator(approve ? "#radio-approve" : "#radio-send").CheckAsync();
        await page.Locator(approve ? "#approve-details" : "#send-details").FillAsync(message);
        await page.Locator("#main-content .govuk-button").ClickAsync();
        var title = approve ? "Apprentice details approved and sent to training provider" : "Notification sent to training provider";
        return await VerifyPageAsync(() => new ManualCohortConfirmationPage(context, title));
    }

    internal async Task ApproveManualCohort()
    {
        await page.Locator("#radio-approve").CheckAsync();
        await page.Locator("#main-content .govuk-button").ClickAsync();
        await VerifyPageAsync(() => new ApprenticeDetailsApproved(context));
    }

    internal async Task EditManualApprentice(Apprenticeship apprenticeship)
    {
        await page.Locator(".edit-apprentice").First.ClickAsync();
        var edit = await VerifyPageAsync(() => new EditLearnerDetailsPage(context));
        await edit.EditManualEntryDetails(apprenticeship);
        await VerifyManualEntryPage();
    }

    internal async Task VerifyTransferCohortStatus(string expected) =>
        await Assertions.Expect(page.Locator("#cohortStatus")).ToHaveTextAsync(expected);
}
