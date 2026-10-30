using SFA.DAS.Approvals.UITests.Project.Helpers;
using SFA.DAS.Approvals.UITests.Project.Helpers.DataHelpers.ApprenticeshipModel;
using SFA.DAS.Approvals.UITests.Project.Pages.Employer;
using SFA.DAS.Approvals.UITests.Project.Pages.Provider;
using SFA.DAS.EmployerPortal.UITests.Project.Pages.InterimPages;
using SFA.DAS.ProviderLogin.Service.Project;
using SFA.DAS.ProviderLogin.Service.Project.Helpers;
using SFA.DAS.TransferMatching.UITests.Project.Tests.Pages;

namespace SFA.DAS.TransferMatching.UITests.Project.Tests.Steps;

[Binding]
public class TransferLearnerSteps(ScenarioContext context)
{
    [Then("the non levy employer can add apprentice to the pledgeApplication")]
    public async Task AddLearnerFromApplication()
    {
        var provider = await new UseTransferFundsPage(context).ClickOnStartNowButton();
        await AddLearner(provider);
    }

    [Then("the non levy employer can add apprentice to the pledgeApplication via 'Add an apprentice' route")]
    public async Task AddLearnerFromLearnersSection()
    {
        await new EmployerHomePageStepsHelper(context).GotoEmployerHomePage(false);
        var page = context.Get<Driver>().Page;
        await page.GetByLabel("Service information").GetByRole(AriaRole.Link, new() { Name = "Learners", Exact = true }).ClickAsync();
        var learner = await new LearnersHomePage(context).GoToAddALearner();
        var funding = await learner.ClickStartNowButtonNonLevyFlow();
        var transfers = await funding.SelectTransferFunds();
        var objectContext = context.Get<ObjectContext>();
        var pledge = objectContext.GetPledgeDetail();
        var provider = await transfers.SelectTransfer(objectContext.GetPledgeApplication(pledge.PledgeId), pledge.SenderHashedAccountId);
        await AddLearner(provider);
    }

    private async Task AddLearner(ChooseYourMainTrainingProviderPage providerPage)
    {
        var data = context.Get<TMDataHelper>();
        var provider = context.GetProviderConfig<ProviderConfig>();
        var apprentice = await new ApprenticeFactory().CreateApprenticeAsync();
        var contact = context.Get<EmployerPortalDataHelper>();
        apprentice.Email = contact.RandomEmail;
        var apprenticeship = new Apprenticeship
        {
            ApprenticeDetails = apprentice,
            EmployerDetails = new()
            {
                EmployerName = context.Get<ObjectContext>().GetOrganisationName(),
                EmployerType = SFA.DAS.Approvals.UITests.Project.Helpers.TestDataHelpers.EmployerType.NonLevy
            },
            ProviderDetails = new() { Ukprn = int.Parse(provider.Ukprn), ProviderName = provider.Name },
            TrainingDetails = new()
            {
                CourseTitle = data.Course,
                StartDate = data.CourseStartDate,
                EndDate = data.CourseStartDate.AddMonths(data.Duration),
                TotalPrice = data.Cost,
                ConsumerReference = "TM" + RandomDataGenerator.GenerateRandomAlphabeticString(8)
            }
        };
        context.Set(new List<Apprenticeship> { apprenticeship }, ScenarioKeys.ListOfApprenticeship);
        var confirmation = await providerPage.SubmitValidUkprn(int.Parse(provider.Ukprn));
        var add = await confirmation.ConfirmTrainingProviderDetails();
        await add.SelectEmployerAddApprencticesAndSend();
        var course = await VerifyPageHelper.VerifyPageAsync(context, () => new TransferLearnerCoursePage(context));
        var details = await course.SelectCourse(data.Course);
        var approval = await details.Submit(apprenticeship);
        var cohortRow = context.Get<Driver>().Page.Locator("dt").Filter(new() { HasText = "Reference" }).Locator("xpath=following-sibling::dd").First;
        apprenticeship.Cohort.Reference = (await cohortRow.InnerTextAsync()).Trim();
        await approval.EmployerApproveCohort();
    }

    [Then("the provider adds Ulns and approves the cohorts")]
    public async Task ProviderApproves()
    {
        var apprenticeship = context.Get<List<Apprenticeship>>(ScenarioKeys.ListOfApprenticeship).Single();
        var home = await new ProviderHomePageStepsHelper(context).GoToProviderHomePage(true);
        await home.GoToApprenticeRequestsPage();
        var page = new ApprenticeRequests_ProviderPage(context);
        await page.NavigateToBingoBox(SFA.DAS.Approvals.UITests.Project.Helpers.TestDataHelpers.ApprenticeRequests.ReadyForReview);
        var cohort = await page.OpenEditableCohortAsync(apprenticeship.Cohort.Reference);
        await cohort.AddUlnToEmployerCreatedLearner(apprenticeship.ApprenticeDetails.ULN);
        await cohort.ProviderApprovesCohortAfterEmployerApproval();
    }

    [Then("Verify a new live apprenticeship record is created")]
    public async Task VerifyLiveLearner()
    {
        var apprenticeship = context.Get<List<Apprenticeship>>(ScenarioKeys.ListOfApprenticeship).Single();
        await new EmployerHomePageStepsHelper(context).GotoEmployerHomePage();
        await context.Get<Driver>().Page.GetByLabel("Service information")
            .GetByRole(AriaRole.Link, new() { Name = "Learners", Exact = true }).ClickAsync();
        var learners = await new LearnersHomePage(context).GoToManageYourLearners();
        await learners.SearchApprentice(apprenticeship.ApprenticeDetails.ULN, apprenticeship.ApprenticeDetails.FullName);
    }
}
