using SFA.DAS.Approvals.UITests.Project.Helpers.DataHelpers.ApprenticeshipModel;
using SFA.DAS.Approvals.UITests.Project.Helpers.TestDataHelpers;
using SFA.DAS.Approvals.UITests.Project.Pages.Employer;
using SFA.DAS.Approvals.UITests.Project.Pages.Provider;
using SFA.DAS.Login.Service.Project.Helpers;
using SFA.DAS.ProviderLogin.Service.Project.Helpers;
using SFA.DAS.EmployerPortal.UITests.Project;
using System;
using ProviderReviewPage = SFA.DAS.Approvals.UITests.Project.Pages.Provider.ApproveApprenticeDetailsPage;

namespace SFA.DAS.Approvals.UITests.Project.Helpers.StepsHelper;

public class TransferCohortStepsHelper(ScenarioContext context)
{
    private readonly List<Apprenticeship> apprentices = [];
    private readonly IPage employerPage = context.Get<Driver>().Page;
    private IPage providerPage;
    private string cohortReference;

    public string TotalCost => apprentices.Sum(x => x.TrainingDetails.TotalPrice).ToString("C0", System.Globalization.CultureInfo.GetCultureInfo("en-GB"));

    private string MessageToProvider => $"Apprentice {apprentices.First().ApprenticeDetails.FullName}, Total Cost {TotalCost}";

    public void UseEmployerPage() => context.Get<Driver>().Page = employerPage;

    public async Task CreateCohort(string sender, AccountDetails receiver, int count, bool employerAdds)
    {
        if (count < 1)
            throw new ArgumentOutOfRangeException(nameof(count));
        UseEmployerPage();
        var provider = context.GetProviderConfig<ProviderConfig>();
        var objectContext = context.Get<ObjectContext>();
        var mailosaurUser = context.Get<MailosaurUser>();
        for (var index = 0; index < count; index++)
        {
            var apprentice = await new ApprenticeFactory().CreateApprenticeAsync();
            apprentice.Email = $"{apprentice.FirstName}.{apprentice.LastName}@{mailosaurUser.DomainName}";
            mailosaurUser.AddToEmailList(apprentice.Email);
            apprentices.Add(new Apprenticeship
            {
                ApprenticeDetails = apprentice,
                TrainingDetails = await new TrainingFactory(DateTime.Today.AddMonths(-1)).CreateTrainingAsync(EmployerType.Levy),
                EmployerDetails = new Employer
                {
                    EmployerName = receiver.OrgName,
                    EmployerType = EmployerType.Levy,
                    Email = objectContext.GetRegisteredEmail(),
                    AccountLegalEntityId = int.Parse(receiver.Aleid),
                    AgreementId = receiver.AleAgreementid
                },
                ProviderDetails = new Provider { ProviderName = provider.Name, Ukprn = int.Parse(provider.Ukprn), Email = provider.Username }
            });
        }
        context.Set(apprentices, ScenarioKeys.ListOfApprenticeship);
        await employerPage.GetByLabel("Service information").GetByRole(AriaRole.Link, new() { Name = "Learners", Exact = true }).ClickAsync();
        var learners = new LearnersHomePage(context);
        await learners.VerifyPage();
        var add = await learners.GoToAddALearner();
        var funding = await add.ClickStartNowButtonNonLevyFlow();
        var trainingProvider = await funding.SelectDirectTransferFunds(sender);
        var confirm = await trainingProvider.SubmitValidUkprn(int.Parse(provider.Ukprn));
        var choice = await confirm.ConfirmTrainingProviderDetails();
        if (employerAdds)
        {
            var course = await choice.SelectEmployerManualEntry();
            EmployerApproveLearnerDetailsPage review = null;
            foreach (var apprentice in apprentices)
            {
                if (review != null)
                    course = await review.AddAnotherManualApprentice();
                var details = await course.SelectCourse(apprentice.TrainingDetails);
                await details.FillDetails(apprentice);
                review = await details.SaveEmployerDetails();
            }
            await review.VerifyManualApprentices(apprentices);
            var sent = await review.SendManualCohortToProvider(true, MessageToProvider);
            cohortReference = await sent.GetReference();
        }
        else
        {
            var sent = await choice.SendManualEntryRequestToProvider(MessageToProvider);
            cohortReference = await sent.GetReference();
        }
        if (string.IsNullOrWhiteSpace(cohortReference))
            throw new InvalidOperationException("Creating a Transfers cohort did not return a reference.");
        foreach (var apprentice in apprentices)
            apprentice.Cohort.Reference = cohortReference;
        objectContext.SetDebugInformation($"Created Transfers cohort {cohortReference} for {receiver.OrgName} using sender {sender}.");
    }

    public async Task ProviderAddsAndApproves()
    {
        var review = await OpenProviderCohort();
        foreach (var apprentice in apprentices)
        {
            var course = await review.AddManualApprentice();
            var details = await course.SelectCourse(apprentice.TrainingDetails);
            await details.FillDetails(apprentice);
            review = await details.SaveProviderDetails();
        }
        await review.EnsureManualApprenticeUlns(apprentices);
        await review.ApproveManualCohort();
    }

    public async Task ProviderApproves()
    {
        var review = await OpenProviderCohort();
        await review.EnsureManualApprenticeUlns(apprentices);
        await review.ApproveManualCohort();
    }

    public async Task ProviderEditsApprentice()
    {
        var review = await OpenProviderCohort();
        await review.EnsureManualApprenticeUlns(apprentices);
    }

    public async Task EmployerApproves()
    {
        var review = await OpenEmployerCohort(false);
        await review.VerifyManualApprentices(apprentices);
        await review.ApproveManualCohort();
    }

    public async Task VerifyCohortStatus(string status, bool withTransferSender)
    {
        var review = await OpenEmployerCohort(withTransferSender);
        await review.VerifyTransferCohortStatus(status);
    }

    public async Task EmployerEditsAndSends()
    {
        var review = await OpenEmployerCohort(false);
        var apprentice = apprentices.First();
        apprentice.ApprenticeDetails.FirstName = RandomDataGenerator.GenerateRandomAlphabeticString(6);
        apprentice.ApprenticeDetails.LastName = RandomDataGenerator.GenerateRandomAlphabeticString(9);
        apprentice.ApprenticeDetails.DateOfBirth = DateTime.Today.AddYears(-25);
        apprentice.TrainingDetails.ConsumerReference = RandomDataGenerator.GenerateRandomAlphanumericString(10);
        await review.EditManualApprentice(apprentice);
        await review.SendManualCohortToProvider(true, MessageToProvider);
    }

    public async Task EmployerSendsForReview()
    {
        var review = await OpenEmployerCohort(false);
        await review.SendManualCohortToProvider(false, MessageToProvider);
    }

    public async Task VerifyLiveApprentices()
    {
        UseEmployerPage();
        await employerPage.GetByLabel("Service information").GetByRole(AriaRole.Link, new() { Name = "Learners", Exact = true }).ClickAsync();
        var learners = new LearnersHomePage(context);
        await learners.VerifyPage();
        var manage = await learners.GoToManageYourLearners();
        foreach (var apprentice in apprentices)
            await manage.VerifyLiveApprentice(apprentice.ApprenticeDetails.ULN, apprentice.ApprenticeDetails.FullName);
    }

    private async Task<EmployerApproveLearnerDetailsPage> OpenEmployerCohort(bool withTransferSender)
    {
        UseEmployerPage();
        await employerPage.GetByLabel("Service information").GetByRole(AriaRole.Link, new() { Name = "Learners", Exact = true }).ClickAsync();
        var learners = new LearnersHomePage(context);
        await learners.VerifyPage();
        var requests = await learners.GoToReviewLearnerRequests();
        return await requests.OpenManualEntryCohort(cohortReference, withTransferSender);
    }

    private async Task<ProviderReviewPage> OpenProviderCohort()
    {
        if (string.IsNullOrWhiteSpace(cohortReference))
            throw new InvalidOperationException("A Transfers cohort must be created before provider review.");
        var driver = context.Get<Driver>();
        if (providerPage != null)
            driver.Page = providerPage;
        var home = await new ProviderHomePageStepsHelper(context).GoToProviderHomePage(providerPage == null);
        providerPage = driver.Page;
        await home.GoToApprenticeRequestsPage();
        var requests = new ApprenticeRequests_ProviderPage(context);
        await requests.VerifyPage();
        await requests.NavigateToBingoBox(ApprenticeRequests.ReadyForReview);
        return await requests.OpenManualEntryCohort(cohortReference);
    }
}
