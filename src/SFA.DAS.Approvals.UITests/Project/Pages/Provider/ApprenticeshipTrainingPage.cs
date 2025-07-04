﻿using Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.Approvals.UITests.Project.Pages.Provider
{
    internal class ApprenticeshipTrainingPage(ScenarioContext context) : ApprovalsBasePage(context)
    {
        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Apprenticeship training");
        }

        internal async Task<CheckYourInformationPage> ReserveFundsAsync(string courseName, DateTime reservationStartDate)
        {
            await page.GetByRole(AriaRole.Combobox, new() { Name = "Start typing to search" }).ClickAsync();
            await page.GetByRole(AriaRole.Combobox, new() { Name = "Start typing to search" }).FillAsync(courseName.Substring(0, 3));
            await page.GetByRole(AriaRole.Option, new() { Name = courseName }).ClickAsync();
            await page.GetByRole(AriaRole.Radio, new() { Name = reservationStartDate.ToString("MMMM yyyy") }).CheckAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();
            return await VerifyPageAsync(() => new CheckYourInformationPage(context));
        }

    }
}
