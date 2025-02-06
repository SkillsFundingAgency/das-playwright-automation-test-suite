using SFA.DAS.Registration.UITests.Project;
using SFA.DAS.SupportTools.UITests.Project.Helpers;
using SFA.DAS.SupportTools.UITests.Project.Tests.Pages;
using System.Linq;
using TechTalk.SpecFlow.Assist;

namespace SFA.DAS.SupportTools.UITests.Project.Tests.Steps;

[Binding]
public class SupportToolsSteps(ScenarioContext context)
{
    private readonly ObjectContext _objectContext = context.Get<ObjectContext>();

    private readonly StepsHelper _stepsHelper = new(context);

    [Given(@"Opens the Pause Utility")]
    [When(@"user opens Pause Utility")]
    public async Task WhenUserOpensPauseUtility() => await new ToolSupportHomePage(context).ClickPauseApprenticeshipsLink();

    [Given(@"Opens the Resume Utility")]
    public async Task GivenOpensTheResumeUtility() => await new ToolSupportHomePage(context).ClickResumeApprenticeshipsLink();

    [Given(@"Opens the Stop Utility")]
    public async Task GivenOpensTheStopUtility() => await new ToolSupportHomePage(context).ClickStopApprenticeshipsLink();

    [Given(@"Search for Apprentices using following criteria")]
    [Then(@"following filters should return the expected number of TotalRecords")]
    public async Task ThenFollowingFiltersShouldReturnTheExpectedNumberOfTotalRecords(Table table)
    {
        var filters = table.CreateSet<Filters>().ToList();
        int row = 1;

        foreach (var item in filters)
        {
            _objectContext.Set($"FilterCriteria_{row}", item);

            var page = new SearchForApprenticeshipPage(context);

            await page.EnterEmployerName(item.EmployerName);

            await page.EnterProviderName(item.ProviderName);

            await page.EnterUkprn(item.Ukprn);

            await page.EnterEndDate(item.EndDate);

            await page.EnterULNorApprenticeName(item.Uln);

            await page.SelectStatus(item.Status);

            await page.ClickSubmitButton();

            var page1 = new SearchForApprenticeshipPage(context);

            if (item.TotalRecords == 0) await page1.GetNoRecordsFound();

            else Assert.GreaterOrEqual(await page1.GetNumberOfRecordsFound(), item.TotalRecords, $"Validate number of expected records on row: {row}{item}");

            row++;
        }
    }

    [When(@"User selects all records and click on Pause Apprenticeship button")]
    public async Task WhenUserSelectsAllRecordsAndClickOnPauseApprenticeshipButton() { var page = await SelectAllRecords(); await page.ClickPauseButton(); }

    [When(@"User selects all records and click on Resume Apprenticeship button")]
    public async Task WhenUserSelectsAllRecordsAndClickOnResumeApprenticeshipButton() { var page = await SelectAllRecords(); await page.ClickResumeButton(); }

    [When(@"User selects all records and click on Stop Apprenticeship button")]
    public async Task WhenUserSelectsAllRecordsAndClickOnStopApprenticeshipButton() { var page = await SelectAllRecords(); await page.ClickStopButton(); }

    [Given(@"the SCS User is logged into Support Tools")]
    public async Task GivenTheSCSUserIsLoggedIntoSupportTools() => await _stepsHelper.ValidUserLogsinToSupportSCSTools();

    [Given(@"the SCP User is logged into Support Tools")]
    public async Task GivenTheSCPUserIsLoggedIntoSupportTools() => await _stepsHelper.ValidUserLogsinToSupportSCPTools(false);

    private async Task<SearchForApprenticeshipPage> SelectAllRecords()
    {
        var page = new SearchForApprenticeshipPage(context);

        var ulns = await page.GetULNsFromApprenticeshipTable();

        await UpdateStatusInDb([.. ulns]);

        await page.SelectAllRecords();

        return await BasePage.VerifyPageAsync(() => new SearchForApprenticeshipPage(context));
    }

    [Then(@"User should be able to stop all the records")]
    public async Task ThenUserShouldBeAbleToStopAllTheRecords()
    {
        var page = new StopApprenticeshipsPage(context);

        await page.ClickStopBtn();

        await page.ValidateErrorMessage();

        await page.EnterStopDateAndClickSetbutton();

        await page.ValidateStopDateApplied();

        await page.ClickStopBtn();

        var ststusList = await page.GetStatusColumn();

        ValidateStopSuccessful(ststusList);
    }

    [Then(@"User should be able to pause all the live records")]
    public async Task ThenUserShouldBeAbleToPauseAllTheLiveRecords()
    {
        var page = new PauseApprenticeshipsPage(context);

        await page.ClickPauseBtn();

        await page.VerifyPage();

        var ststusList = await page.GetStatusColumn();

        ValidatePausedSuccessful(ststusList);
    }

    [Then(@"User should be able to resume all the paused records")]
    public async Task ThenUserShouldBeAbleToResumeAllThePausedRecords()
    {
        var page = new ResumeApprenticeshipsPage(context);

        await page.ClickResumeBtn();

        await page.VerifyPage();

        var ststusList = await page.GetStatusColumn();

        ValidateResumeSuccessful(ststusList);
    }

    [Given(@"User should NOT be able to see Pause, Resume, Suspend and Reinstate utilities")]
    public async Task ThenUserShouldNOTBeAbleToSeePauseResumeSuspendAndReinstateUtilities()
    {
        ToolSupportHomePage toolSupportHomePage = new(context);

        Assert.IsFalse(await toolSupportHomePage.IsPauseApprenticeshipLinkVisible());
        Assert.IsFalse(await toolSupportHomePage.IsResumeApprenticeshipLinkVisible());
        Assert.IsFalse(await toolSupportHomePage.IsReinstateApprenticeshipLinkVisible());
        Assert.IsFalse(await toolSupportHomePage.IsSuspendApprenticeshipLinkVisible());
        Assert.IsTrue(await toolSupportHomePage.IsStopApprenticeshipLinkVisible());
    }


    [When(@"that account is suspended using bulk utility")]
    public async Task WhenThatAccountIsSuspendedUsingBulkUtility()
    {
        var page = await _stepsHelper.ValidUserLogsinToSupportSCPTools(true);

        var page1 = await page.ClickSuspendUserAccountsLink();

        await page1.EnterHashedAccountId(GetHashedAccountId());

        await page1.ClickSubmitButton();

        await page1.SelectAllRecords();

        var page2 = await page1.ClickSuspendUserButton();

        await page2.ClicSuspendUsersbtn();

        await page2.VerifyStatusColumn("Submitted successfully");
    }

    [When(@"that account is reinstated using bulk utility")]
    public async Task WhenThatAccountIsReinstatedUsingBulkUtility()
    {
        string expectedStatusBefore = "Suspended " + DateTime.Now.ToString("dd/MM/yyyy");

        string expectedStatusAfter = "Submitted successfully";

        var page = await _stepsHelper.ValidUserLogsinToSupportSCPTools(true);

        var page1 = await page.ClickReinstateUserAccountsLink();

        await page1.EnterHashedAccountId(GetHashedAccountId());

        await page1.ClickSubmitButton();

        await page1.SelectAllRecords();

        var page2 = await page1.ClickReinstateUserButton();

        await page2.VerifyStatusColumn(expectedStatusBefore);

        await page2.ClickReinstateUsersbtn();

        await page2.VerifyStatusColumn(expectedStatusAfter);
    }

    private async Task UpdateStatusInDb(List<string> UlnList)
    {
        var _commitmentsSqlDataHelper = new ToolsCommitmentsSqlDataHelper(_objectContext, context.Get<DbConfig>());

        int i = 0;

        string query = string.Empty;

        foreach (var uln in UlnList)
        {
            if (i >= 0 && i < 4)
                query += ToolsCommitmentsSqlDataHelper.GetUpdateApprenticeshipStatus(uln, 1);
            else if (i == 4 || i == 5 || i == 6)
                query += ToolsCommitmentsSqlDataHelper.GetUpdateApprenticeshipStatus(uln, 2);
            else if (i == 7 || i == 8)
                query += ToolsCommitmentsSqlDataHelper.GetUpdateApprenticeshipStatus(uln, 3);
            else
                query += ToolsCommitmentsSqlDataHelper.GetUpdateApprenticeshipStatus(uln, 4);

            i++;
        }

        await _commitmentsSqlDataHelper.UpdateApprenticeshipStatus(query);
    }

    private static void ValidatePausedSuccessful(IReadOnlyList<string> StatusList)
    {
        Assert.IsTrue(StatusList.Count == 10, "Validate total number of records");
        string todaysDate = DateTime.Now.ToString("dd/MM/yyyy");
        int i = 0;
        MultipleAssert(() =>
        {
            foreach (var status in StatusList)
            {
                if (i >= 0 && i < 4)
                    StringAssert.AreEqualIgnoringCase($"Submitted successfully {todaysDate}", status, $"failed at index [{i}]");
                else if (i == 4 || i == 5 || i == 6)
                    StringAssert.AreEqualIgnoringCase($"Paused {todaysDate} - Only Active record can be paused", status, $"failed at index [{i}]");
                else if (i == 7 || i == 8)
                    StringAssert.AreEqualIgnoringCase($"Stopped {todaysDate} - Only Active record can be paused", status, $"failed at index [{i}]");
                else
                    StringAssert.AreEqualIgnoringCase($"Completed - Only Active record can be paused", status, $"failed at index [{i}]");

                i++;
            }
        });
    }

    private static void ValidateResumeSuccessful(IReadOnlyList<string> StatusList)
    {
        Assert.IsTrue(StatusList.Count == 10, "Validate total number of records");
        string todaysDate = DateTime.Now.ToString("dd/MM/yyyy");
        int i = 0;
        MultipleAssert(() =>
        {
            foreach (var status in StatusList)
            {
                if (i >= 0 && i < 4)
                    Assert.That(status == "Live - Only paused record can be activated" || status == "WaitingToStart - Only paused record can be activated", "Resuming a Live Record", $"failed at index [{i}]");
                else if (i == 4 || i == 5 || i == 6)
                    Assert.That(status == $"Submitted successfully {todaysDate}", "Resuming a Paused Record", $"failed at index [{i}]");
                else if (i == 7 || i == 8)
                    Assert.That(status == $"Stopped {todaysDate} - Only paused record can be activated", "Resuming a Stopped Record", $"failed at index [{i}]");
                else
                    Assert.That(status == "Completed - Only paused record can be activated", "Resuming a Stopped Record", $"failed at index [{i}]");

                i++;
            }
        });
    }

    private static void ValidateStopSuccessful(IReadOnlyList<string> StatusList)
    {

        Assert.IsTrue(StatusList.Count == 10, $"Validate total number of records. Expected: 10 | Actual {StatusList.Count}");
        string todaysDate = "01/" + DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString("0000");
        string todaysDate2 = DateTime.Now.Month.ToString("00") + "/01/" + DateTime.Now.Year.ToString("0000");
        int i = 0;

        MultipleAssert(() =>
        {
            foreach (var status in StatusList)
            {
                if (i >= 0 && i < 9)
                    Assert.IsTrue(status == $"Submitted successfully {todaysDate}" || status == $"Submitted successfully {todaysDate2}", $"validation failed at index [{i}]. Expected was [Submitted successfully {todaysDate}]  but actual value displayed is [{status}]");
                else
                    Assert.IsTrue(status == "Apprenticeship must be Active or Paused. Unable to stop apprenticeship", $"validation failed at index [{i}]. Expected was [Apprenticeship must be Active or Paused. Unable to stop apprenticeship]  but actual value displayed is [{status}]");

                i++;
            }

        });
    }

    private static void MultipleAssert(Action action) => Assert.Multiple(() => { action(); });

    private string GetHashedAccountId() => _objectContext.GetHashedAccountId();
}