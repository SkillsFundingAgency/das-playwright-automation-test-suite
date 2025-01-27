namespace SFA.DAS.SupportConsole.UITests.Project.Tests.Pages;

public abstract class SupportConsoleBasePage(ScenarioContext context) : BasePage(context)
{
    #region Helpers and Context
    protected readonly SupportConsoleConfig config = context.GetSupportConsoleConfig<SupportConsoleConfig>();
    #endregion

    //protected static By OrganisationsMenuLink => By.LinkText("Organisations");
    //protected static By CommitmentsMenuLink => By.LinkText("Commitments");
    //protected static By FinanceLink => By.LinkText("Finance");
    //protected static By TeamMembersLink => By.LinkText("Team members");

    //public void ClickFinanceMenuLink() => formCompletionHelper.Click(FinanceLink);

    public async Task<SearchHomePage> GoToSearchHomePage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Apprenticeship service" }).ClickAsync();

        return await VerifyPageAsync(() => new SearchHomePage(context));
    }

    public async Task<string> GetCohortRefNumber() => await page.GetByRole(AriaRole.Row, new() { Name = "Cohort reference" }).TextContentAsync();
}