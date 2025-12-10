using SFA.DAS.RAA.DataGenerator.Project;
using System.Text.RegularExpressions;

namespace SFA.DAS.RAA.Service.Project.Pages.Reviewer;

public class Reviewer_HomePage(ScenarioContext context) : BasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("#main-content")).ToContainTextAsync("Review Vacancy");
    }

    public async Task<Reviewer_AnyVacancyPreviewPage> ReviewNextVacancy()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Review Vacancy" }).ClickAsync();

        return await VerifyPageAsync(() => new Reviewer_AnyVacancyPreviewPage(context));
    }

    public async Task<Reviewer_VacancyPreviewPage> ReviewVacancy()
    {
        var vacref = objectContext.GetVacancyReference();

        await page.GetByRole(AriaRole.Textbox, new() { Name = "Search vacancies" }).FillAsync(vacref);

        await page.GetByRole(AriaRole.Button, new() { Name = "Search" }).ClickAsync();

        await page.GetByRole(AriaRole.Row, new() { Name = vacref }).GetByRole(AriaRole.Link, new() { Name = "Review" }).ClickAsync();

        return await VerifyPageAsync(() => new Reviewer_VacancyPreviewPage(context));
    }
}

public class Reviewer_AnyVacancyPreviewPage(ScenarioContext context) : ApproveVacancyBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("form")).ToContainTextAsync("Summary");
    }
}

public class Reviewer_VacancyPreviewPage(ScenarioContext context) : ApproveVacancyBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync(vacancyTitleDataHelper.VacancyTitle);
    }

    public new async Task VerifyEmployerName()
    {
        if (IsFoundationAdvert) await CheckFoundationTag();

        await base.VerifyEmployerName();

    }
}

public abstract class ApproveVacancyBasePage(ScenarioContext context) : VerifyDetailsBasePage(context)
{
    private static string ErrorsCheckboxes => "[name='SelectedAutomatedQaResults']";

    private static string ReviewerComment => ("#ReviewerComment");

    private static string TitleFieldIdentifiers => ("#SelectedFieldIdentifiers-Title");

    public async Task<QAReviewsPage> Approve()
    {
        var errors = await page.Locator(ErrorsCheckboxes).AllAsync();

        foreach (var error in errors) await error.UncheckAsync();

        await Submit();

        return await VerifyPageAsync(() => new QAReviewsPage(context));
    }

    public async Task ReferTitle()
    {
        await page.Locator(TitleFieldIdentifiers).CheckAsync();

        await page.Locator(ReviewerComment).FillAsync("Refered - Title requires edit");

        await Submit();

        await page.GetByRole(AriaRole.Link, new() { Name = "Apprenticeship service vacancy QA" }).ClickAsync();
    }

    private async Task Submit() => await page.GetByRole(AriaRole.Button, new() { Name = "Approve" }).ClickAsync();
}

public class QAReviewsPage(ScenarioContext context) : VerifyDetailsBasePage(context)
{
    public override async Task VerifyPage()
    {
        await page.WaitForURLAsync(new Regex("reviews"), new PageWaitForURLOptions
        {
            WaitUntil = WaitUntilState.NetworkIdle,
            Timeout = 5000
        });

        await Assertions.Expect(page.Locator("form")).ToContainTextAsync("Summary");
    }
}