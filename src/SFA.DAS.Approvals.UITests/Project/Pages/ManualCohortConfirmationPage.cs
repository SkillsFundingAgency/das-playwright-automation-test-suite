using System;
using System.Text.RegularExpressions;

namespace SFA.DAS.Approvals.UITests.Project.Pages;

internal class ManualCohortConfirmationPage(ScenarioContext context, string title) : ApprovalsBasePage(context)
{
    public override async Task VerifyPage() =>
        await Assertions.Expect(page.GetByRole(AriaRole.Heading).Filter(new() { HasText = title })).ToBeVisibleAsync();

    internal async Task<string> GetReference()
    {
        var row = page.Locator(".govuk-summary-list__row, .instructionSent tr")
            .Filter(new() { HasTextRegex = new Regex(@"(?:Cohort )?reference", RegexOptions.IgnoreCase) });
        await Assertions.Expect(row).ToHaveCountAsync(1);
        var reference = RegexHelper.GetCohortReference(await row.InnerTextAsync());
        if (!Regex.IsMatch(reference, "^[A-Z0-9]{6}$"))
            throw new InvalidOperationException("The cohort confirmation did not contain a cohort reference.");
        return reference;
    }
}
