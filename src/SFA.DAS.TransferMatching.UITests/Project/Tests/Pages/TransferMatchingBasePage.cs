using System.Globalization;
using System.Text.RegularExpressions;
using SFA.DAS.EmployerPortal.UITests.Project;

namespace SFA.DAS.TransferMatching.UITests.Project.Tests.Pages;

public abstract class TransferMatchingBasePage(ScenarioContext context) : BasePage(context)
{
    protected TMDataHelper tMDataHelper => context.Get<TMDataHelper>();
    protected abstract string PageTitle { get; }
    protected virtual ILocator PageHeader => page.Locator("h1").First;
    protected virtual ILocator ContinueButton => page.GetByRole(AriaRole.Button, new() { Name = "Continue", Exact = true });

    public override async Task VerifyPage() =>
        await Assertions.Expect(PageHeader).ToContainTextAsync(PageTitle);

    public async Task<string> GetErrorMessage() => await page.Locator(".govuk-error-summary").InnerTextAsync();

    protected string GetPledgeId() => objectContext.GetPledgeDetail().PledgeId;

    protected async Task Continue() => await ContinueButton.ClickAsync();

    protected async Task SelectRadioOptionByText(string text) =>
        await page.GetByRole(AriaRole.Radio, new() { Name = text, Exact = true }).CheckAsync();

    protected async Task<T> FollowLink<T>(string text, Func<T> nextPage) where T : BasePage
    {
        await page.GetByRole(AriaRole.Link, new() { Name = text, Exact = true }).ClickAsync();
        return await VerifyPageAsync(nextPage);
    }

    protected async Task<CreateATransferPledgePage> SelectAndContinue()
    {
        await SelectRandomCheckbox();
        await Continue();
        return await VerifyPageAsync(() => new CreateATransferPledgePage(context));
    }

    protected async Task SelectRandomCheckbox()
    {
        var choices = page.Locator(".govuk-checkboxes .govuk-checkboxes__input");
        await Assertions.Expect(choices.First).ToBeVisibleAsync();
        var count = await choices.CountAsync();
        await choices.Nth(count > 1 ? Random.Shared.Next(1, count) : 0).CheckAsync();
    }

    /// <param name="newestLast">Set when the list is sorted oldest first, so a newly created item is searched for from the last page backwards.</param>
    protected async Task FindOnPaginatedList(ILocator item, bool newestLast = false)
    {
        if (!await item.IsVisibleAsync())
        {
            var direction = newestLast && await GoToLastPage() ? "Previous page" : "Next page";
            var pageLink = page.GetByRole(AriaRole.Link, new() { Name = direction, Exact = true });

            while (!await item.IsVisibleAsync() && await pageLink.IsVisibleAsync())
                await GoToPage(await pageLink.GetAttributeAsync("href"), () => pageLink.ClickAsync());
        }
        await Assertions.Expect(item).ToBeVisibleAsync();
    }

    private async Task<bool> GoToLastPage()
    {
        var next = page.GetByRole(AriaRole.Link, new() { Name = "Next page", Exact = true });
        var summary = page.Locator(".das-pagination__summary");
        if (!await next.IsVisibleAsync() || await summary.CountAsync() == 0) return false;

        // Summary is either "Showing 50 of 2149 ..." or "Showing 1 to 50 of 2149 ...", possibly split across lines.
        var match = Regex.Match(await summary.InnerTextAsync(), @"Showing\s+(?:(\d+)\s+to\s+)?(\d+)\s+of\s+(\d+)");
        var href = await next.GetAttributeAsync("href");
        if (!match.Success || href is null) return false;

        var endRow = int.Parse(match.Groups[2].Value, CultureInfo.InvariantCulture);
        var startRow = match.Groups[1].Success ? int.Parse(match.Groups[1].Value, CultureInfo.InvariantCulture) : 1;
        var totalItems = int.Parse(match.Groups[3].Value, CultureInfo.InvariantCulture);
        var lastPage = (int)Math.Ceiling(totalItems / (double)(endRow - startRow + 1));
        if (lastPage <= 1) return false;

        var lastPageHref = Regex.Replace(href, @"page=\d+", $"page={lastPage}");
        await GoToPage(lastPageHref, () => page.GotoAsync(new Uri(new Uri(page.Url), lastPageHref).ToString()));
        return true;
    }

    private async Task GoToPage(string href, Func<Task> navigate)
    {
        var target = href is null ? null : new Uri(new Uri(page.Url), href).ToString();
        await navigate();
        if (target is not null) await page.WaitForURLAsync(target);
        await VerifyPageAsync(() => this);
    }

    protected static int ReadAmount(string text) =>
        int.Parse(Regex.Replace(text, @"[^\d-]", ""), CultureInfo.InvariantCulture);
}
