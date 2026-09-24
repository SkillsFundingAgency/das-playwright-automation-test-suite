using System;
using System.Collections.Generic;
using System.Text;
using SFA.DAS.Approvals.UITests.Project.Helpers.DataHelpers.ApprenticeshipModel;
using SFA.DAS.ProviderLogin.Service.Project.Pages;

namespace SFA.DAS.Approvals.UITests.Project.Pages.Provider;

internal class DraftApprenticeshipOverlapOptionsPage(ScenarioContext context) : ApprovalsBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Planned start date overlaps with existing training");
    }

    public async Task VerifyButtonText()
    {
        await Assertions.Expect(page.Locator("button.govuk-button:has-text('Save and return to cohort')")).ToContainTextAsync("Save and return to cohort");
    }
}
