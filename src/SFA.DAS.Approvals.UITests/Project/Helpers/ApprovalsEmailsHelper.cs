using SFA.DAS.Approvals.UITests.Project.Helpers.DataHelpers.ApprenticeshipModel;
using SFA.DAS.Framework.Helpers;

namespace SFA.DAS.Approvals.UITests.Project.Helpers
{
    internal class ApprovalsEmailsHelper
    {
        private readonly ScenarioContext context;
        private List<Apprenticeship> listOfApprenticeship;
        internal ApprovalsEmailsHelper(ScenarioContext context)
        {
            this.context = context;
            listOfApprenticeship = context.GetValue<List<Apprenticeship>>(ScenarioKeys.ListOfApprenticeship);
        }

        internal async Task VerifyEmailAsync(string recipient, string notificationType)
        {
            var apprentice = listOfApprenticeship.FirstOrDefault();
            var employerEmail = apprentice.EmployerDetails.Email;
            var apprenticeName = $"{apprentice.ApprenticeDetails.FullName}";
            var apprenticeEmail = apprentice.ApprenticeDetails.Email;
            var providerName = apprentice.ProviderDetails.ProviderName;
            var providerEmail = apprentice.ProviderDetails.Email;
            var cohortReference = apprentice.Cohort.Reference;
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

                case ("Reservation made on your behalf", "employer"):
                    rcvrEmail = employerEmail;
                    subject = "funding reservation made on your behalf";
                    body = $" reserved apprenticeship funding on your behalf for the following:";
                    break;

                case ("Learner record stopped", "employer"):
                    rcvrEmail = employerEmail;
                    subject = "Learner record stopped";
                    body = $"Your learner {apprenticeName} has been withdrawn from ";
                    break; 

                case ("Learner record has been paused", "employer"):
                    rcvrEmail = employerEmail;
                    subject = "Learner record has been paused";
                    body = $"{providerName} has changed the status of a learner record to paused.";
                    break; 

                //provider emails below this area
                case ("cohort ready for review", "provider"):
                    rcvrEmail = providerEmail;
                    subject = "Apprenticeship service cohort ready for review";
                    body = $"Cohort {cohortReference} has been updated and is ready for review.";
                    break;

                //apprentice emails below this area
                case ("Welcome to your apprenticeship", "apprentice"):
                    rcvrEmail = apprenticeEmail;
                    subject = "Welcome to your apprenticeship";
                    body = $"Congratulations on becoming an apprentice.";
                    break;

                default:
                    Assert.Fail($"Unknown notification type: {notificationType}");
                    break;
            }

            await mailosaurApiHelper.CheckEmail(rcvrEmail, subject, body);


        }


    }
}
