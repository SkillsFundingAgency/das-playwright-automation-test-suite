using Azure;
using SFA.DAS.Approvals.UITests.Project.Helpers.SqlHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.Approvals.UITests.Project.Pages.Provider
{
    internal class ChooseAnEmployerPage(ScenarioContext context) : ApprovalsBasePage(context)
    {
        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Choose an employer");
        }

        public async Task<ConfirmEmployerPage> ChooseLevyEmployer() => await ChooseAnEmployer("Levy");
        internal async Task<ConfirmEmployerPage> ChooseNonLevyEmployer() => await ChooseAnEmployer("NonLevy");
        internal async Task<ConfirmEmployerPage> ChooseNonLevyEmployerAtMaxReservationLimit() => await ChooseAnEmployer("NonLevyUserAtMaxReservationLimit");

        private async Task<ConfirmEmployerPage> ChooseAnEmployer(string employerType)
        {

            EasAccountUser employerUser = employerType switch
            {
                "NonLevy" => context.GetUser<NonLevyUser>(),
                "NonLevyUserAtMaxReservationLimit" => context.GetUser<NonLevyUserAtMaxReservationLimit>(),
                _ => context.GetUser<LevyUser>()
            };

            var employerName = employerUser.OrganisationName[..3] + "%";

            var agreementId = await context.Get<AccountsDbSqlHelper>().GetAgreementId(employerUser.Username, employerName);

            await page.GetByRole(AriaRole.Row, new() { Name = agreementId }).GetByRole(AriaRole.Link).ClickAsync();

            return await VerifyPageAsync(() => new ConfirmEmployerPage(context));
        }


    }
}
