using NUnit.Framework;
using SFA.DAS.EmployerPortal.UITests.Project;
using SFA.DAS.Framework.Helpers;
using SFA.DAS.Login.Service.Project.Helpers;
using SFA.DAS.RAA.DataGenerator.Project;
using SFA.DAS.RAA.DataGenerator.Project.Helpers;

namespace SFA.DAS.RAAEmployer.UITests.Project.Tests.Steps;

[Binding]
public class EmployerEmailNotificationsSteps(ScenarioContext context)
{
    private readonly ObjectContext objectContext = context.Get<ObjectContext>();
    private readonly MailosaurApiHelper mailosaurApiHelper = context.Get<MailosaurApiHelper>();
    protected readonly VacancyTitleDatahelper vacancyTitleDataHelper = context.Get<VacancyTitleDatahelper>();

    [Then(@"the '(.*)' receives '(.*)' email notification")]
    public async Task ThenTheEmployerReceivesEmailNotification(string userType, string notificationType)
    {
        string GetProviderEmail()
        {

            return RAADataHelper.ProviderEmail;
            //var providerConfig = context.GetProviderConfig<ProviderConfig>();

            //return providerConfig.Username;
        }
        string GetEmployerEmail() => objectContext.GetRegisteredEmail();

        string GetApplicantEmail()
        {
            bool isFoundationAdvert = context.ContainsKey("isFoundationAdvert") && (bool)context["isFoundationAdvert"];

            string applicantEmail = context.GetUser<FAAApplyUser>().Username;

            string foundationApplicantEmail = context.GetUser<FAAFoundationUser>().Username;

            return isFoundationAdvert? foundationApplicantEmail : applicantEmail;
        }

        string emailText = null;
        string subject = null;
        string userEmail = null;

        switch (notificationType, userType)
        {
            case ("new application", "employer"):
                emailText = "Your advert has received a new application";
                subject = $"New application for {vacancyTitleDataHelper.VacancyTitle} apprenticeship";
                userEmail = GetEmployerEmail();
                break;

            case ("rejected advert", "employer"):
                emailText = "DfE has rejected this advert. We’ve left a comment to explain why.";
                subject = $"Rejected by DfE: make changes to {vacancyTitleDataHelper.VacancyTitle} apprenticeship";
                userEmail = GetEmployerEmail();
                break;

            case ("rejected vacancy", "provider"):
                emailText = "The vacancy needs some changes";
                subject = $"Rejected: Updates needed to your vacancy (VAC{objectContext.GetVacancyReference()})";
                userEmail = GetProviderEmail();
                break;

            case ("employer review", "employer"):
                emailText = $"{GetProviderEmail()} has sent you this advert";
                subject = "New apprenticeship advert to review";
                userEmail = GetEmployerEmail();
                break;

            case ("approved advert", "employer"):
                emailText = "DfE has approved this advert. It’s now live on Find an apprenticeship.";
                subject = $"Approved by DfE: {vacancyTitleDataHelper.VacancyTitle} apprenticeship is now live on Find an apprenticeship";
                userEmail = GetEmployerEmail();
                break;

            case ("employer approved vacancy", "provider"):
                emailText = "The employer has approved this vacancy. It’ll now be sent to DfE – you’ll get an email after we’ve reviewed it.";
                subject = $"Approved by employer: {vacancyTitleDataHelper.VacancyTitle} apprenticeship now sent to DfE";
                userEmail = GetProviderEmail();
                break;

            case ("employer rejected vacancy", "provider"):
                emailText = "This vacancy has been reviewed and was rejected";
                subject = $"Rejected: Updates needed to your vacancy (VAC{objectContext.GetVacancyReference()})";
                userEmail = GetProviderEmail();
                break;

            case ("new application", "applicant"):
                emailText = "We’ve received your application for:";
                subject = $"Application submitted: {vacancyTitleDataHelper.VacancyTitle} apprenticeship";
                userEmail = GetApplicantEmail();
                break;

            case ("successful application", "applicant"):
                emailText = "Congratulations, you’ve been offered an apprenticeship:";
                subject = $"Successful application: {vacancyTitleDataHelper.VacancyTitle} apprenticeship";
                userEmail = GetApplicantEmail();
                break;

            case ("unsuccessful application", "applicant"):
                emailText = "An application you’ve made has been unsuccessful:";
                subject = $"Unsuccessful application: read your feedback for {vacancyTitleDataHelper.VacancyTitle} apprenticeship";
                userEmail = GetApplicantEmail();
                break;

            case ("withdrawn application", "applicant"):
                emailText = "You’ve withdrawn your application for:";
                subject = $"Application withdrawn: {vacancyTitleDataHelper.VacancyTitle}";
                userEmail = GetApplicantEmail();
                break;

            case ("shared application", "employer"):
                emailText = $"has sent you a new application to review for {vacancyTitleDataHelper.VacancyTitle}";
                subject = "New apprenticeship application to review";
                userEmail = GetEmployerEmail();
                break;

            case ("employer listed you as training provider", "provider"):
                emailText = $"An employer’s listed you as the training provider on this vacancy. Contact the employer if you were not expecting this.";
                subject = "An employer’s listed you as the training provider on a vacancy";
                userEmail = GetProviderEmail();
                break;

            default:
                Assert.Fail($"Unknown notification type: {notificationType}");
                break;
        }

        var emailMessage = await mailosaurApiHelper.GetEmailBody(userEmail, subject, emailText);
        StringAssert.Contains(emailText, emailMessage.Text.Body);
    }
}
