using Azure;
using SFA.DAS.Approvals.UITests.Project.Helpers.DataHelpers;
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
            var link = page.Locator($"a[href*='{partialHref}']:text('Add apprentice')");
            await link.First.ClickAsync();

            return await VerifyPageAsync(() => new AddApprenticeDetails_EntryMothodPage(context));
        }


    }
}
