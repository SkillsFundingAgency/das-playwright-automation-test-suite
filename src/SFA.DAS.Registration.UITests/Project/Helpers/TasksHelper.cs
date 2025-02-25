
using Azure;
using SFA.DAS.Registration.UITests.Project.Helpers.SqlDbHelpers;
using SFA.DAS.Registration.UITests.Project.Pages;

namespace SFA.DAS.Registration.UITests.Project.Helpers;

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
        return await _transferMatchingSqlDataHelper.GetNumberTransferPledgeApplicationsToReview(accountId);
    }

    public static async Task<HomePage> ClickViewApprenticeChangesLink(HomePage homePage, int numberOfChanges)
    {
        var page = await homePage.ClickViewChangesForApprenticeChangesToReview(numberOfChanges);

        return await page.GoToHomePage();
    }

    public static async Task<HomePage> ClickViewCohortsToReviewLink(HomePage homePage, int numberOfChanges)
    {
        var page = await homePage.ClickViewCohortsForCohortsReadyToReview(numberOfChanges);

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

    public static async Task<HomePage> ClickTransferPledgeApplicationsLink(HomePage homePage, int numberOfChanges)
    {
        var page = await homePage.ClickViewTransferPledgeApplications(numberOfChanges);

        return await page.GoToHomePage();
    }
}
