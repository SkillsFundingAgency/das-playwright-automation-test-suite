﻿namespace SFA.DAS.SupportTools.UITests.Project.Tests.Pages;

public abstract class SupportConsoleBasePage(ScenarioContext context) : BasePage(context)
{
    #region Helpers and Context
    protected readonly SupportToolsConfig config = context.GetSupportConsoleConfig<SupportToolsConfig>();
    #endregion

    //protected static By OrganisationsMenuLink => By.LinkText("Organisations");
    //protected static By CommitmentsMenuLink => By.LinkText("Commitments");
    //protected static By FinanceLink => By.LinkText("Finance");
    //protected static By TeamMembersLink => By.LinkText("Team members");

    public async Task ClickFinanceMenuLink() => await page.GetByRole(AriaRole.Link, new() { Name = "Finance" }).ClickAsync();

    public async Task<SearchHomePage> GoToEmployerAccountSearchHomePage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Employer Account Search" }).ClickAsync();

        return await VerifyPageAsync(() => new SearchHomePage(context));
    }

    public async Task<SearchHomePage> GoToEmployerUserSearchHomePage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Employer User Search" }).ClickAsync();

        return await VerifyPageAsync(() => new SearchHomePage(context));
    }

    public async Task<SearchHomePage> GoBackToSearchHomePage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Back to Search" }).ClickAsync();

        return await VerifyPageAsync(() => new SearchHomePage(context));
    }

    public async Task<string> GetCohortRefNumber() => await page.Locator(".govuk-summary-list__value").First.TextContentAsync();
}