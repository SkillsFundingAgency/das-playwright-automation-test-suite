namespace SFA.DAS.SupportTools.UITests.Project.Tests.Pages;

public class SearchForApprenticeshipPage(ScenarioContext context) : ToolSupportBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.GetByRole(AriaRole.Heading)).ToContainTextAsync("Search for an apprenticeship.");

    public async Task EnterEmployerName(string employerName)
    {
        await page.Locator("#searchForm #employerName").FillAsync(employerName);
    }

    public async Task EnterProviderName(string providerName)
    {
        await page.Locator("#searchForm #providerName").FillAsync(providerName);
    }

    public async Task EnterUkprn(string ukprn)
    {
        await page.GetByRole(AriaRole.Spinbutton).FillAsync(ukprn);
    }

    public async Task EnterCourseName(string courseName)
    {
        await page.Locator("#searchForm #courseName").FillAsync(courseName);
    }

    public async Task EnterEndDate(string endDate)
    {
        // endDate should be in format "2026-01-22"

        if (!string.IsNullOrEmpty(endDate)) await page.Locator("#searchForm #endDate").FillAsync(endDate);
    }

    public async Task EnterULNorApprenticeName(string apprenticeNameOrUln)
    {
        await page.Locator("input[name='ApprenticeNameOrUln']").FillAsync(apprenticeNameOrUln);
    }
    public async Task SelectStatus(string status)
    {
        status = status == "" ? "Any" : status;

        await page.GetByLabel("Apprenticeship status").SelectOptionAsync([status]);
    }

    public async Task SelectAllRecords() => await page.GetByRole(AriaRole.Row, new() { Name = "Id Uln Cohort Ref First Name" }).GetByLabel("").CheckAsync();


    public async Task ClickSubmitButton()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Submit" }).ClickAsync();
    }

    public async Task<PauseApprenticeshipsPage> ClickPauseButton()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Pause apprenticeship(s)" }).ClickAsync();

        return await VerifyPageAsync(() => new PauseApprenticeshipsPage(context));
    }

    public async Task<ResumeApprenticeshipsPage> ClickResumeButton()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Resume apprenticeship(s)" }).ClickAsync();

        return await VerifyPageAsync(() => new ResumeApprenticeshipsPage(context));
    }

    public async Task<StopApprenticeshipsPage> ClickStopButton()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Stop apprenticeship(s)" }).ClickAsync();

        return await VerifyPageAsync(() => new StopApprenticeshipsPage(context));
    }

    public async Task<string> GetNoRecordsFound() => await page.GetByRole(AriaRole.Cell, new() { Name = "No matching records found" }).TextContentAsync();

    public async Task<int> GetNumberOfRecordsFound()
    {
        await page.GetByText("Showing 1 to").WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible });

        var paginationInfo = await page.GetByText("Showing 1 to").TextContentAsync();

        objectContext.SetDebugInformation($"pagination info - {paginationInfo}");

        var arrPaginationInfo = paginationInfo.Trim().Split(" ");

        if (arrPaginationInfo.Length < 5)
            return 0;
        else
            return Convert.ToInt32(arrPaginationInfo[5]);
    }

    public async Task<IReadOnlyList<string>> GetULNsFromApprenticeshipTable()
    {
        return await page.Locator("#apprenticeshipResultsTable tr td:nth-child(3)").AllTextContentsAsync();
    }
}

public class PauseApprenticeshipsPage(ScenarioContext context) : ToolSupportBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.GetByRole(AriaRole.Heading)).ToContainTextAsync("Pause apprenticeships");

    public async Task ClickPauseBtn()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Pause apprenticeship(s)" }).ClickAsync();
    }

    public async Task<IReadOnlyList<string>> GetStatusColumn() => await GetStatusColumn(page.Locator("#apprenticeshipsTable tr td:nth-child(11)"));
}

public class ResumeApprenticeshipsPage(ScenarioContext context) : ToolSupportBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.GetByRole(AriaRole.Heading)).ToContainTextAsync("Resume apprenticeships");

    public async Task ClickResumeBtn()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Resume apprenticeship(s)" }).ClickAsync();
    }

    public async Task<IReadOnlyList<string>> GetStatusColumn() => await GetStatusColumn(page.Locator("#apprenticeshipsTable tr td:nth-child(11)"));
}

public class StopApprenticeshipsPage(ScenarioContext context) : ToolSupportBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.GetByRole(AriaRole.Heading)).ToContainTextAsync("Stop apprenticeships");

    public async Task ClickStopBtn()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Stop apprenticeship(s)" }).ClickAsync();
    }

    public async Task ValidateErrorMessage()
    {
        await Assertions.Expect(page.GetByRole(AriaRole.Listitem)).ToContainTextAsync("Not all Apprenticeship rows have been supplied with a stop date.");
    }

    public async Task<IReadOnlyList<string>> GetStatusColumn() => await GetStatusColumn(page.Locator("#apprenticeshipsTable tr td:nth-child(12)"));

    public async Task EnterStopDateAndClickSetbutton()
    {
        string stopDate = DateTime.Now.Year.ToString("0000") + "-" + DateTime.Now.Month.ToString("00") + "-01";

        await page.Locator("#bulkDate").FillAsync(stopDate);

        await page.GetByRole(AriaRole.Button, new() { Name = "Set" }).ClickAsync();
    }

    public async Task ValidateStopDateApplied()
    {
        var actualDate = await page.Locator("#date_0").InputValueAsync();

        string expectedDate1 = DateTime.Now.Year + "-" + DateTime.Now.Month.ToString("00") + "-01";

        string expectedDate2 = DateTime.Now.Year + "-01-" + DateTime.Now.Month.ToString("00");

        Assert.IsTrue(expectedDate1 == actualDate || expectedDate2 == actualDate, $"Validate correct stop date has been set in the table - {actualDate}, {expectedDate1}, {expectedDate2}");
    }
}