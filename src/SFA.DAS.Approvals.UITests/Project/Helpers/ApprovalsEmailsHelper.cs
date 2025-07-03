using Polly;
using SFA.DAS.Approvals.UITests.Project.Helpers.DataHelpers;
using SFA.DAS.MailosaurAPI.Service.Project.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.Approvals.UITests.Project.Helpers
{
    internal class ApprovalsEmailsHelper
    {
        private readonly ScenarioContext context;
        private List<Apprenticeship> listOfApprenticeship;
        internal ApprovalsEmailsHelper(ScenarioContext context)
        {
            this.context = context;
            listOfApprenticeship = context.GetValue<List<Apprenticeship>>();
        }

        internal async Task VerifyEmailAsync(string recipient, string notificationType)
        {
            var apprentice = listOfApprenticeship.FirstOrDefault();
            var employerEmail = apprentice.EmployerDetails.Email;
            var apprenticeName = $"{apprentice.ApprenticeDetails.FirstName}";
            var apprenticeEmail = apprentice.ApprenticeDetails.Email;
            var providerEmail = "das-automation@l38cxwya.mailosaur.net";
            var cohortReference = apprentice.CohortReference;
            var mailosaurApiHelper = context.Get<MailosaurApiHelper>();

            string rcvrEmail = null;
            string subject = null;
            string body = null;

            switch (notificationType, recipient.ToLower())
            {
                //employer emails below this area
                case ("Apprentice details ready to approve", "employer"):
                    rcvrEmail = employerEmail;
                    subject = "Apprentice details ready to approve";
                    body = $" sent you apprentice details to approve (reference {cohortReference}).";
                    break;

                //provider emails below this area
                case ("cohort ready for review", "provider"):
                    rcvrEmail = providerEmail;
                    subject = "Apprenticeship service cohort ready for review";
                    body = $"Cohort {cohortReference} has been updated and is ready for review.";
                    break;

                //apprentice emails below this area
                case ("Confirm apprenticeship details", "apprentice"):
                    rcvrEmail = apprenticeEmail;
                    subject = "You need to confirm your apprenticeship details";
                    body = $"Hello {apprenticeName}, Congratulations on becoming an apprentice.";
                    break;

                default:
                    Assert.Fail($"Unknown notification type: {notificationType}");
                    break;
            }

            await mailosaurApiHelper.CheckEmail(rcvrEmail, subject, body);


        }


    }
}
