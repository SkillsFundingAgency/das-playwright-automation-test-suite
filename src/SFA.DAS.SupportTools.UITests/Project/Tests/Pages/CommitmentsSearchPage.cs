using SFA.DAS.SupportTools.UITests.Project.Helpers.SqlHelpers;

namespace SFA.DAS.SupportTools.UITests.Project.Tests.Pages;

public class CommitmentsSearchPage : SupportConsoleBasePage
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("form .govuk-heading-l")).ToContainTextAsync("Search");

    public static string SearchSectionHeaderText => "Search";
    public static string UlnSearchTextBoxHelpTextContent => "Enter ULN number";
    public static string CohortSearchTextBoxHelpTextContent => "Enter Cohort Reference number";
    public static string InvalidUln => "1234567";
    public static string InvalidUlnWithSpecialChars => "!£$%^&*()@?|#";
    public static string InvalidCohort => "ABCD";
    public static string InvalidCohortWithSpecialChars => "!£$%^&*()@?|#";
    public static string UlnSearchErrorMessage => "Please enter a valid unique learner number";
    public static string CohortSearchErrorMessage => "Please enter a 6 or 7-digit Cohort number";
    public static string UnauthorisedCohortSearchErrorMessage => "Account is unauthorised to access this Cohort.";


    public CommitmentsSqlDataHelper SqlDataHelper { get; }


    public CommitmentsSearchPage(ScenarioContext context) : base(context)
    {
        SqlDataHelper = new CommitmentsSqlDataHelper(objectContext, context.Get<DbConfig>());
    }

    public async Task SelectUlnSearchTypeRadioButton() => await page.GetByRole(AriaRole.Radio, new() { Name = "ULN" }).CheckAsync();

    private async Task ClickSearchButton() => await page.GetByRole(AriaRole.Button, new() { Name = "Search" }).ClickAsync();

    public async Task<UlnSearchResultsPage> SearchForULN(string uln)
    {
        await SelectUlnSearchTypeRadioButton();

        await SearchUln(uln);

        return await VerifyPageAsync(() => new UlnSearchResultsPage(context));
    }

    public async Task SearchWithInvalidULN() => await SearchUln(InvalidUln);

    public async Task SearchWithInvalidULNWithSpecialChars() => await SearchUln(InvalidUlnWithSpecialChars);

    public async Task SelectCohortRefSearchTypeRadioButton() => await page.GetByRole(AriaRole.Radio, new() { Name = "Cohort Ref" }).CheckAsync();

    public async Task SearchWithInvalidCohort() => await Search(InvalidCohort);

    public async Task SearchWithUnauthorisedCohortAccess() => await Search(config.CohortNotAssociatedToAccount.CohortRef);

    public async Task SearchWithInvalidCohortWithSpecialChars() => await Search(InvalidCohortWithSpecialChars);

    public async Task<string> GetCommitmentsSearchPageErrorText()
    {
        var content = await page.Locator("#error-message-SearchTerm").TextContentAsync();

        return content.Trim();
    }

    public async Task<CohortDetailsPage> SearchCohort(string text)
    {
        await SelectCohortRefSearchTypeRadioButton();

        await Search(text);

        return await VerifyPageAsync(() => new CohortDetailsPage(context));
    }

    private async Task Search(string text) 
    { 
        await page.Locator("#SearchTerm").FillAsync(text); 

        await ClickSearchButton(); 
    }

    private async Task SearchUln(string text) 
    { 
        await page.Locator("#SearchTerm").FillAsync(text);

        await ClickSearchButton(); 
    }
}