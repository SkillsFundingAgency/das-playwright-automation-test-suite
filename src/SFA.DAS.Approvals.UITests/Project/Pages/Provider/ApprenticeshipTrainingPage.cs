using System;

namespace SFA.DAS.Approvals.UITests.Project.Pages.Provider
{
    internal class ApprenticeshipTrainingPage(ScenarioContext context) : ApprovalsBasePage(context)
    {
        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Apprenticeship training");
        }

        internal async Task<ConfirmYourReservationPage> ReserveFundsAsync(string courseName, DateTime reservationStartDate)
        {
            await page.GetByRole(AriaRole.Combobox, new() { Name = "Start typing to search" }).ClickAsync();
            await page.GetByRole(AriaRole.Combobox, new() { Name = "Start typing to search" }).FillAsync(courseName.Substring(0, 3));
            await page.GetByRole(AriaRole.Option, new() { Name = courseName }).First.ClickAsync();
            await page.GetByRole(AriaRole.Radio, new() { Name = reservationStartDate.ToString("MMMM yyyy") }).CheckAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();
            return await VerifyPageAsync(() => new ConfirmYourReservationPage(context));
        }

    }
}
