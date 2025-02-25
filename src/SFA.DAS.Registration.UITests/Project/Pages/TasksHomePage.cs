using Azure;

namespace SFA.DAS.Registration.UITests.Project.Pages;

public partial class TasksHomePage(ScenarioContext context) : HomePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.GetByRole(AriaRole.Tab, new() { Name = "Tasks" })).ToBeVisibleAsync();
    }
    
    public const string ApprenticeChangeToReview = "ApprenticeChangeToReview";
    public const string CohortReadyForApproval = "CohortReadyForApproval";
    public const string ReviewConnectionRequest = "ReviewConnectionRequest";
    public const string TransferRequestReceived = "TransferRequestReceived";

    public async Task VerifyStartAddingApprenticesNowTaskLink() => await Assertions.Expect(page.GetByRole(AriaRole.Link, new() { Name = "Start adding apprentices now" })).ToBeVisibleAsync();

    //public void AssertTaskCount(string taskType, int expectedNumberOfTasks)
    //{
    //    context.Get<RetryAssertHelper>().RetryOnTasksHomePage(() =>
    //    {
    //        Assert.That(pageInteractionHelper.FindElements(TaskSelector).Count > 0, "No Task list element found");

    //        int currentNumberOfTasks = GetTaskCount(taskType);

    //        Assert.That(currentNumberOfTasks == expectedNumberOfTasks, $"The task type '{taskType}' was expected to have '{expectedNumberOfTasks}' tasks but currently has '{currentNumberOfTasks}' task");
    //    }, () => { RefreshPage(); VerifyTasksHomePage(); });
    //}

    //private bool VerifyTasksHomePage() => VerifyPageAfterRefresh(PageHeader, PageTitle);

    //private int GetTaskCount(string taskType)
    //{
    //    return taskType switch
    //    {
    //        ApprenticeChangeToReview => GetCurrentNumberOfTasks("apprentice change", true),
    //        CohortReadyForApproval => GetCurrentNumberOfTasks("ready for approval", true),
    //        ReviewConnectionRequest => GetCurrentNumberOfTasks("connection request", true),
    //        TransferRequestReceived => GetCurrentNumberOfTasks("Transfer request received", false),
    //        _ => 0,
    //    }; ;
    //}

    //private int GetCurrentNumberOfTasks(string taskText, bool hasItemsDueCount)
    //{
    //    var taskLink = pageInteractionHelper.FindElement(TaskSelector).FindElements(TaskListSelector).FirstOrDefault(x => x.Text.ContainsCompareCaseInsensitive(taskText));

    //    if (taskLink != null && hasItemsDueCount)
    //    {
    //        var match = NoOfTasksRegex().Match(taskLink.Text);
    //        return match.Success && match.Groups.Count > 0 ? int.Parse(match.Groups[1].Value) : 0;
    //    }
    //    else if (taskLink != null)
    //    {
    //        return 1;
    //    }

    //    return 0;
    //}

    //[GeneratedRegex("^(\\d+).*\r?$", RegexOptions.Multiline)]
    //private static partial Regex NoOfTasksRegex();
}