﻿using Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.Approvals.UITests.Project.Pages.Provider
{
    internal class ConfirmApprenticeDeletionPage(ScenarioContext context) : ApprovalsBasePage(context)
    {
        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1").First).ToContainTextAsync("Confirm apprentice deletion");
        }

        internal async Task<ApproveApprenticeDetailsPage> ConfirmDeletion()
        {
            await page.GetByRole(AriaRole.Radio, new() { Name = "Yes, delete the record" }).CheckAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();
            return await VerifyPageAsync(() => new ApproveApprenticeDetailsPage(context));
        }

    }
}
