using System.Text.RegularExpressions;

namespace SFA.DAS.EmployerPortal.UITests.Project.Pages;

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

    public async Task VerifyStartAddingApprenticesNowTaskLink() => await Assertions.Expect(page.GetByRole(AriaRole.Link, new() { Name = "Start adding learners now" })).ToBeVisibleAsync();

    public async Task AssertTaskCount(string taskType, int expectedNumberOfTasks)
    {
        if (expectedNumberOfTasks < 0)
            throw new ArgumentOutOfRangeException(nameof(expectedNumberOfTasks));

        var (taskPattern, hasCount) = taskType switch
        {
            ApprenticeChangeToReview => (@"(?:apprentice|learner)\s+changes?\s+to review", true),
            CohortReadyForApproval => (@"(?:apprentice|learner|cohort)\s+requests?\s+ready for (?:review|approval)", true),
            ReviewConnectionRequest => (@"connection requests?\s+to review", true),
            TransferRequestReceived => ("Transfer request received", false),
            _ => throw new ArgumentException($"Unknown task type '{taskType}'.", nameof(taskType))
        };

        var tasks = page.GetByLabel("Tasks");
        await Assertions.Expect(tasks).ToBeVisibleAsync();
        var matchingTasks = tasks.GetByRole(AriaRole.Listitem)
            .Filter(new() { HasTextRegex = new Regex(taskPattern, RegexOptions.IgnoreCase) });

        if (expectedNumberOfTasks == 0)
        {
            await Assertions.Expect(matchingTasks).ToHaveCountAsync(0);
            return;
        }

        if (!hasCount && expectedNumberOfTasks != 1)
            throw new ArgumentOutOfRangeException(nameof(expectedNumberOfTasks), "This task does not display a numeric count.");

        await Assertions.Expect(matchingTasks).ToHaveCountAsync(1);
        if (hasCount)
        {
            await Assertions.Expect(matchingTasks).ToContainTextAsync(
                new Regex($@"^\s*{expectedNumberOfTasks}\s+{taskPattern}", RegexOptions.IgnoreCase));
        }
    }
}