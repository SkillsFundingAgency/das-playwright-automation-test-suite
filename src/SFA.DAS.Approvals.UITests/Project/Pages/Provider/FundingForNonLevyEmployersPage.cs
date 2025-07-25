﻿using Azure;
using SFA.DAS.Approvals.UITests.Project.Helpers.DataHelpers.ApprenticeshipModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.Approvals.UITests.Project.Pages.Provider
{
    internal class FundingForNonLevyEmployersPage(ScenarioContext context) : ApprovalsBasePage(context)
    {
        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Funding for non-levy employers");
        }

        internal async Task<ReserveFundingForNonLevyEmployersPage> ClickOnReserveMoreFundingLink()
        {
            await page.GetByRole(AriaRole.Link, new() { Name = "Reserve more funding" }).ClickAsync();
            return await VerifyPageAsync(() => new ReserveFundingForNonLevyEmployersPage(context));
        }

        internal async Task<AddApprenticeDetails_EntryMothodPage> SelectReservationToAddApprentice(Apprenticeship apprenticeship )
        {
            var rsrvStartDate = apprenticeship.TrainingDetails.StartDate.ToString("MMM yyyy", System.Globalization.CultureInfo.InvariantCulture);
            var rsrvEndDate = apprenticeship.TrainingDetails.StartDate.AddMonths(2).ToString("MMM yyyy", System.Globalization.CultureInfo.InvariantCulture);
            var rsrvWindow = $"{rsrvStartDate} to {rsrvEndDate}";
            
            await page.GetByLabel("Employer", new() { Exact = true }).SelectOptionAsync(new[] { apprenticeship.EmployerDetails.EmployerName });
            await page.GetByLabel("Start date", new() { Exact = true }).SelectOptionAsync(new[] { rsrvWindow });
            await page.GetByRole(AriaRole.Button, new() { Name = "Apply filters" }).ClickAsync();

            string partialHref = $"reservationId={apprenticeship.ReservationID}";
            var reservationLink = page.Locator($"a[href*='{partialHref}']:text('Add apprentice')");
            var nextPageButton = page.GetByRole(AriaRole.Link, new() { Name = "Next page" });

            // ✅ First, check if link is already present and visible
            if (await reservationLink.IsVisibleAsync())
            {
                await reservationLink.First.ClickAsync();
                return await VerifyPageAsync(() => new AddApprenticeDetails_EntryMothodPage(context));
            }

            // 🔁 If not, loop through pagination
            while (true)
            {
                if (await reservationLink.CountAsync() > 0 && await reservationLink.IsVisibleAsync())
                {
                    await reservationLink.First.ClickAsync();
                    break;
                }

                if (await nextPageButton.IsVisibleAsync())
                {
                    await nextPageButton.ClickAsync();
                    await page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
                }
                else
                {
                    throw new Exception($"Reservation link not found for reservationId='{apprenticeship.ReservationID}' and no next page is available to navigate");
                }
            }


            return await VerifyPageAsync(() => new AddApprenticeDetails_EntryMothodPage(context));
        }


    }
}
