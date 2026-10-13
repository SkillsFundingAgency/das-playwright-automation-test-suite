using System;
using System.Collections.Generic;
using System.Text;

namespace SFA.DAS.Approvals.UITests.Project.Pages.Provider;

internal class ConfirmDetailsPage(ScenarioContext context) : ApprovalsBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1").First).ToContainTextAsync("Confirm details");
    }
}