
using Azure;
using SFA.DAS.EmployerPortal.UITests.Project.Helpers.SqlDbHelpers;
using SFA.DAS.EmployerPortal.UITests.Project.Pages;
namespace SFA.DAS.EmployerPortal.UITests.Project.Helpers;

public class TasksHelper(ScenarioContext context)
{
    private readonly CommitmentsSqlHelper _commitmentsSqlHelper = context.Get<CommitmentsSqlHelper>();
    private readonly EmployerFinanceSqlHelper _employerFinanceSqlHelper = context.Get<EmployerFinanceSqlHelper>();
    private readonly TransferMatchingSqlDataHelper _transferMatchingSqlDataHelper = context.Get<TransferMatchingSqlDataHelper>();
    private readonly ObjectContext _objectContext = context.Get<ObjectContext>();
    
    public async Task<int> GetNumberOfApprenticesToReview()
    {
        var accountId = _objectContext.GetDBAccountId();
        return await _commitmentsSqlHelper.GetNumberOfApprenticesToReview(accountId);
    }

    public async Task<int> GetNumberOfCohortsReadyToReview()
    {
        var accountId = _objectContext.GetDBAccountId();
        return await _commitmentsSqlHelper.GetNumberOfCohortsReadyToReview(accountId);
    }

    public async Task<int> GetNumberOfTransferRequestToReview()
    {
        var accountId = _objectContext.GetDBAccountId();
        return await _commitmentsSqlHelper.GetNumberOfTransferRequestToReview(accountId);
    }

    public async Task<int> GetNumberOfPendingTransferConnections()
    {
        var accountId = _objectContext.GetDBAccountId();
        return await _employerFinanceSqlHelper.GetNumberOfPendingTransferConnections(accountId);
    }

    public async Task<int> GetNumberTransferPledgeApplicationsToReview()
    {
        var accountId = _objectContext.GetDBAccountId();
        return await _transferMatchingSqlDataHelper.GetNumberTransferPledgeApplicationsByApplicationStatus(accountId,"0");
    }

    public async Task<int> GetNumberOfTransferPledgeApplicationsApproved()
    {
        var accountId = _objectContext.GetDBAccountId();
        var approvedApplications = await _transferMatchingSqlDataHelper.GetTransferPledgeApplicationsByApplicationStatus(accountId, "1");
        return approvedApplications?.Count ?? 0;
    }

    public async Task<int> GetNumberOfAcceptedTransferPledgeApplicationsWithNoApprentices()
    {
        var accountId = _objectContext.GetDBAccountId();

        var acceptedApplications = await _transferMatchingSqlDataHelper
            .GetTransferPledgeApplicationsByApplicationStatus(accountId, "3");

        if (acceptedApplications == null || acceptedApplications.Count == 0)
        {
            return 0;
        }
        var cohortResult = await _commitmentsSqlHelper
            .GetPledgeApplicationIdsAndNumberOfDraftApprentices(accountId);

        if (cohortResult == null || cohortResult.Count == 0)
        {
            return acceptedApplications.Count;
        }
        var acceptedApplicationIdsWithoutApprentices = acceptedApplications
            .Where(appId =>
            {
                var cohorts = cohortResult.Where(x => x.PledgeApplicationId == appId);
                return !cohorts.Any() || cohorts.All(x => x.NumberOfDraftApprentices == 0);
            })
            .ToList();

        return acceptedApplicationIdsWithoutApprentices.Count;
    }

    public static async Task<HomePage> ClickViewApprenticeChangesLink(HomePage homePage, int numberOfChanges)
    {
        var page = await homePage.ClickViewChangesForApprenticeChangesToReview(numberOfChanges);
        
        return await page.GoToHomePage();
    }

    public static async Task<HomePage> ClickTransfersAvailableToAddApprenticeLink(HomePage homePage, int numberOfChanges)
    {
        if (numberOfChanges == 1)
        {
            var page = await homePage.ClickViewTransfersAvailableToAddApprentice();
            return await page.GoToHomePage();  
        }
        var multiplePage = await homePage.ClickViewMultipleTransfersAvailableToAddApprenticeLink(); 
        return await multiplePage.GoToHomePage();
    }

    public static async Task<HomePage> ClickTransfersToAcceptLink(HomePage homePage, int numberOfChanges)
    {
        if (numberOfChanges == 1)
        {
            var page = await homePage.ClickViewTransferToAccept();
            return await page.GoToHomePage();
        }
        var multiplePage = await homePage.ClickViewMultipleTransfersToAccept();
        return await multiplePage.GoToHomePage();
    }

    public static async Task<HomePage> ClickViewCohortsToReviewLink(HomePage homePage)
    {
        var page = await homePage.ClickViewCohortsForCohortsReadyToReview();

        return await page.GoToHomePage();
    }

    public static async Task<HomePage> ClickViewDetailsForTransferRequestsLink(HomePage homePage)
    {
        var page = await homePage.ClickViewDetailsForTransferRequests();

        return await page.GoToHomePage();
    }

    public static async Task<HomePage> ClickViewDetailsForTransferConnectionRequestsLink(HomePage homePage)
    {
        var page = await homePage.ClickViewDetailsForTransferConnectionRequests();

        return await page.GoToHomePage();
    }

    public static async Task<HomePage> ClickTransferPledgeApplicationsLink(HomePage homePage)
    {
        var page = await homePage.ClickViewTransferPledgeApplications();

        return await page.GoToHomePage();
    }
}