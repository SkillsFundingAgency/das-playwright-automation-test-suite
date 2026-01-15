using SFA.DAS.EmployerPortal.UITests.Project.Helpers;
using SFA.DAS.EmployerPortal.UITests.Project.Pages;

namespace SFA.DAS.EmployerPortal.UITests.Project.Steps;

[Binding]
public class TasksSteps
{
    private readonly ScenarioContext _context;
    private readonly TasksHelper _tasksHelper;
    private HomePage _homePage;

    private const string TaskQueryResultKey = nameof(TaskQueryResult);
    private readonly DateTime _currentDate = DateTime.UtcNow;

    public TasksSteps(ScenarioContext context)
    {
        _context = context;
        _tasksHelper = new TasksHelper(context);
        _homePage = new HomePage(context);
        InitializeTaskQueryResult();
    }

    private void InitializeTaskQueryResult()
    {
        _context.Set(new TaskQueryResult(), TaskQueryResultKey);
    }

    private TaskQueryResult GetTaskQueryResult()
    {
        return _context.Get<TaskQueryResult>(TaskQueryResultKey);
    }

    private void SetTaskQueryResult(TaskQueryResult taskQueryResult)
    {
        _context.Set(taskQueryResult, TaskQueryResultKey);
    }

    [When("the current date is in range 16 - 19")]
    public async Task WhenTheCurrentDateIsInRange()
    {
        var tasks = GetTaskQueryResult();

        var currentDay = _currentDate.Day;

        tasks.ShowLevyDeclarationTask = currentDay >= 16 && currentDay <= 19;

        SetTaskQueryResult(tasks);

        await Task.CompletedTask;
    }

    [Then("display task: Levy declaration due by 19 MMMM")]
    public async Task ThenDisplayTaskLevyDeclarationDueBy()
    {
        var tasks = GetTaskQueryResult();

        if (tasks.ShowLevyDeclarationTask)
        {
            await _homePage.VerifyLevyDeclarationDueTaskMessageShown();
        }
    }

    [When("there are X apprentice changes to review")]
    public async Task WhenThereAreApprenticeChangesToReview()
    {
        var tasks = GetTaskQueryResult();

        tasks.NumberOfApprenticesToReview = await _tasksHelper.GetNumberOfApprenticesToReview();

        SetTaskQueryResult(tasks);
    }

    [Then("display task: X apprentice changes to review")]
    public async Task ThenDisplayApprenticeChangesToReview()
    {
        var tasks = GetTaskQueryResult();

        await _homePage.VerifyApprenticeChangeToReviewMessageShown(tasks.NumberOfApprenticesToReview);
    }

    [Then("View changes link should navigate user to Manage your apprentices page")]
    public async Task ThenViewApprenticeChangesNavigatesToManageYourApprenticesPage()
    {
        var tasks = GetTaskQueryResult();

        _homePage = await TasksHelper.ClickViewApprenticeChangesLink(_homePage, tasks.NumberOfApprenticesToReview);
    }

    [When("there are X cohorts ready for approval")]
    public async Task WhenThereAreCohortsReadyToReview()
    {
        var tasks = GetTaskQueryResult();

        tasks.NumberOfCohortsReadyToReview = await _tasksHelper.GetNumberOfCohortsReadyToReview();

        SetTaskQueryResult(tasks);
    }

    [Then("display task: X cohorts ready for approval")]
    public async Task ThenDisplayNumberOfCohortsReadyToReview()
    {
        var tasks = GetTaskQueryResult();

        await _homePage.VerifyCohortsReadyToReviewMessageShown(tasks.NumberOfCohortsReadyToReview);
    }

    [Then("'View cohorts' link should navigate user to 'Apprentice Requests' page")]
    public async Task ThenViewCohortsReadyToReviewNavigatesToApprenticeRequestsPage()
    {
        _homePage = await TasksHelper.ClickViewCohortsToReviewLink(_homePage);
    }

    [When("there is pending Transfer request ready for approval")]
    public async Task WhenThereIsAPendingTransferRequestReadyForApproval()
    {
        var tasks = GetTaskQueryResult();

        tasks.NumberOfTransferRequestToReview = await _tasksHelper.GetNumberOfTransferRequestToReview();

        SetTaskQueryResult(tasks);
    }

    [Then("display task: Transfer request received'")]
    public async Task ThenDisplayTransferRequestReceived()
    {
        await _homePage.VerifyTransferRequestReceivedMessageShown();
    }

    [Then("'View details' for Transfer Request link should navigate user to Transfers page")]
    public async Task ThenViewTransferRequestDetailsNavigatesToTransferConnectionsPage()
    {
        _homePage = await TasksHelper.ClickViewDetailsForTransferRequestsLink(_homePage);
    }

    [When("there are X transfer connection requests to review")]
    public async Task WhenThereAreTransferConnectionRequestsToReview()
    {
        var tasks = GetTaskQueryResult();

        tasks.NumberOfPendingTransferConnections = await _tasksHelper.GetNumberOfPendingTransferConnections();

        SetTaskQueryResult(tasks);
    }

    [Then("display task: 'X connection requests to review'")]
    public async Task ThenDisplayTransferConnectionRequests()
    {
        var tasks = GetTaskQueryResult();

        await _homePage.VerifyTransferConnectionRequestsMessageShown(tasks.NumberOfPendingTransferConnections);
    }

    [Then("'View details' for Transfer Connection link should navigate user to Transfers page")]
    public async Task ThenViewTransferConnectionRequestDetailsNavigatesToTransferConnectionsPage()
    {
        _homePage = await TasksHelper.ClickViewDetailsForTransferConnectionRequestsLink(_homePage);
    }

    [When("there are X transfer pledge applications awaiting your approval")]
    public async Task WhenThereAreTransferPledgeApplicationsToReview()
    {
        var tasks = GetTaskQueryResult();

        tasks.NumberTransferPledgeApplicationsToReview = await _tasksHelper.GetNumberTransferPledgeApplicationsToReview();

        SetTaskQueryResult(tasks);
    }

    [Then("display task: 'X transfer pledge applications awaiting your approval'")]
    public async Task ThenDisplayNumberTransferPledgeApplicationsToReview()
    {
        var tasks = GetTaskQueryResult();

        await _homePage.VerifyTransferPledgeApplicationsToReviewMessageShown(tasks.NumberTransferPledgeApplicationsToReview);
    }

    [Then("'View applications' link should navigate user to 'My Transfer Pledges' page")]
    public async Task ThenViewTransferPledgeApplicationsNavigatesToMyTransfersPage()
    {
        _homePage = await TasksHelper.ClickTransferPledgeApplicationsLink(_homePage);
    }

    [When("there are X transfers applications available to add an apprentice")]
    public async Task WhenThereAreTransferPledgeApplicationsAvailableToAddAnApprentice()
    {
        var tasks = GetTaskQueryResult();

        tasks.NumberOfAcceptedTransferPledgeApplicationsWithNoApprentices = await _tasksHelper.GetNumberOfAcceptedTransferPledgeApplicationsWithNoApprentices();

    }

    [Then("display task: 'X transfers available to add an apprentice'")]
    public async Task ThenDisplayNumberTransfersAvailableToAddAnApprentice()
    {
        var tasks = GetTaskQueryResult();

        await _homePage.VerifyTransfersAvailableToAddAnApprenticeMessageShown(tasks.NumberOfAcceptedTransferPledgeApplicationsWithNoApprentices);
    }

    [Then("'View details' link should navigate user to 'My applications' page")]
    public async Task ThenViewTransferPledgeApplicationsNavigatesToUseTransferFundsPage()
    {
        var tasks = GetTaskQueryResult();

        _homePage = await TasksHelper.ClickTransfersAvailableToAddApprenticeLink(_homePage, tasks.NumberOfAcceptedTransferPledgeApplicationsWithNoApprentices);

    }


    [When("there are X transfer application has been approved")]
    public async Task WhenTransferApplicationsHaveBeenApproved()
    {
        var tasks = GetTaskQueryResult();
        tasks.NumberOfTransferPledgeApplicationsApproved = await _tasksHelper.GetNumberOfTransferPledgeApplicationsApproved();
        SetTaskQueryResult(tasks);
    }

    [Then("display task: 'X transfers to accept'")]
    public async Task ThenDisplayNumberOfTransferPledgeApplicationsApproved()
    {
        var tasks = GetTaskQueryResult();

        await _homePage.VerifyTransfersToAcceptMessageShown(tasks.NumberOfTransferPledgeApplicationsApproved);
    }

    [Then("'View details' link should navigate user to 'My applications' to Accept page")]
    public async Task ThenViewNumberOfTransferPledgeApplicationsApprovedMyApplicationsPage()
    {
        var tasks = GetTaskQueryResult();

        _homePage = await TasksHelper.ClickTransfersToAcceptLink(_homePage, tasks.NumberOfTransferPledgeApplicationsApproved);
    }
}
