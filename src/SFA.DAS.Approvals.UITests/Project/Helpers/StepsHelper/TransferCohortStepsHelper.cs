using System;
using SFA.DAS.Approvals.UITests.Project.Helpers.DataHelpers.ApprenticeshipModel;
using SFA.DAS.Approvals.UITests.Project.Helpers.TestDataHelpers;
using SFA.DAS.Approvals.UITests.Project.Pages.Employer;
using SFA.DAS.Approvals.UITests.Project.Pages.Provider;
using SFA.DAS.Approvals.UITests.Project.Steps;
using SFA.DAS.EmployerPortal.UITests.Project;

namespace SFA.DAS.Approvals.UITests.Project.Helpers.StepsHelper;

public class TransferCohortStepsHelper(ScenarioContext context)
{
    private readonly List<Apprenticeship> apprentices = [];
    private readonly IPage employerPage = context.Get<Driver>().Page;
    private string cohortReference;

    public string TotalCost => apprentices.Sum(x => x.TrainingDetails.TotalPrice)
        .ToString("C0", System.Globalization.CultureInfo.GetCultureInfo("en-GB"));

    public void UseEmployerPage() => context.Get<Driver>().Page = employerPage;

    public async Task PrepareIlrApprentices(AccountDetails receiver, int count)
    {
        if (count < 1)
            throw new ArgumentOutOfRangeException(nameof(count), "At least one ILR apprentice is required.");
        if (apprentices.Count > 0)
            throw new InvalidOperationException("ILR apprentices have already been prepared for this Transfers scenario.");

        var provider = context.GetProviderConfig<ProviderConfig>();
        var employer = new Employer
        {
            EmployerName = receiver.OrgName,
            EmployerType = EmployerType.Levy,
            Email = context.Get<ObjectContext>().GetRegisteredEmail(),
            AccountLegalEntityId = int.Parse(receiver.Aleid),
            AgreementId = receiver.AleAgreementid
        };

        apprentices.AddRange(await new ApprenticeDataHelper(context).CreateApprenticeshipObject(
            employer, count, provider, ApprenticeshipStatus.WaitingToStart));
        context.Set(apprentices, ScenarioKeys.ListOfApprenticeship);
        await new LearnerDataOuterApiSteps(context).SLDPushDataIntoAS(apprentices);
    }

    public async Task CreateEmptyCohort(string sender)
    {
        if (apprentices.Count == 0)
            throw new InvalidOperationException("Prepare receiver ILR apprentices before creating the Transfers cohort.");

        UseEmployerPage();
        var provider = context.GetProviderConfig<ProviderConfig>();
        await employerPage.GetByLabel("Service information")
            .GetByRole(AriaRole.Link, new() { Name = "Learners", Exact = true }).ClickAsync();
        var learners = new LearnersHomePage(context);
        await learners.VerifyPage();
        var add = await learners.GoToAddALearner();
        var funding = await add.ClickStartNowButtonNonLevyFlow();
        var trainingProvider = await funding.SelectDirectTransferFunds(sender);
        var confirm = await trainingProvider.SubmitValidUkprn(int.Parse(provider.Ukprn));
        var choice = await confirm.ConfirmTrainingProviderDetails();
        var sent = await choice.SelectProviderAddApprencticesAndSend();
        cohortReference = await sent.GetCohortId();
        if (string.IsNullOrWhiteSpace(cohortReference))
            throw new InvalidOperationException("Creating a Transfers cohort did not return a reference.");

        foreach (var apprentice in apprentices)
            apprentice.Cohort.Reference = cohortReference;
        await new CommonStepsHelper(context).SetCohortDetails(cohortReference, "Ready for review", "Under review with Provider");
        context.Get<ObjectContext>().SetDebugInformation(
            $"Created ILR-backed Transfers cohort {cohortReference} for {apprentices[0].EmployerDetails.EmployerName} using sender {sender}.");
    }

    public async Task ProviderAddsAndApproves()
    {
        var review = await OpenProviderCohort();
        var provider = new ProviderStepsHelper(context);
        var selectLearner = await review.ClickOnAddAnotherApprenticeLink();
        review = await provider.AddFirstApprenticeFromILRListWithRPLDetails(selectLearner);
        await provider.AddOtherApprenticesFromILRListWithRPL(review);
        await provider.ProviderApproveCohort(review, "Ready for review");
        await new CommonStepsHelper(context).SetCohortDetails(cohortReference, "Under review with Employer", "Ready for approval");
    }

    public async Task ProviderApproves()
    {
        var review = await OpenProviderCohort();
        await new ProviderStepsHelper(context).ProviderApproveCohort(review, "Ready for review");
        await new CommonStepsHelper(context).SetCohortDetails(cohortReference, "Under review with Employer", "Ready for approval");
    }

    public async Task EmployerApproves()
    {
        var review = await OpenEmployerCohort(false);
        foreach (var apprentice in apprentices)
            await review.VerifyCohort(apprentice);
        await review.EmployerApproveCohort();
    }

    public async Task VerifyCohortStatus(string status, bool withTransferSender)
    {
        var review = await OpenEmployerCohort(withTransferSender);
        await review.ValidateCohortStatus(status);
    }

    public async Task EmployerRequestsProviderReview()
    {
        var review = await OpenEmployerCohort(false);
        await review.RequestChangesFromProvider();
        await new CommonStepsHelper(context).SetCohortDetails(cohortReference, "Ready for review", "Under review with Provider");
    }

    public async Task VerifyLiveApprentices()
    {
        UseEmployerPage();
        await employerPage.GetByLabel("Service information")
            .GetByRole(AriaRole.Link, new() { Name = "Learners", Exact = true }).ClickAsync();
        var learners = new LearnersHomePage(context);
        await learners.VerifyPage();
        var manage = await learners.GoToManageYourLearners();
        foreach (var apprentice in apprentices)
            await manage.VerifyLiveApprentice(apprentice.ApprenticeDetails.ULN, apprentice.ApprenticeDetails.FullName);
    }

    private async Task<EmployerApproveLearnerDetailsPage> OpenEmployerCohort(bool withTransferSender)
    {
        UseEmployerPage();
        await employerPage.GetByLabel("Service information")
            .GetByRole(AriaRole.Link, new() { Name = "Learners", Exact = true }).ClickAsync();
        var learners = new LearnersHomePage(context);
        await learners.VerifyPage();
        var requests = await learners.GoToReviewLearnerRequests();
        return await requests.OpenTransferCohort(cohortReference, withTransferSender);
    }

    private async Task<ApproveApprenticeDetailsPage> OpenProviderCohort()
    {
        if (string.IsNullOrWhiteSpace(cohortReference))
            throw new InvalidOperationException("A Transfers cohort must be created before provider review.");

        return await new ProviderStepsHelper(context).ProviderOpenTheCohort(cohortReference);
    }
}
