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

    protected async Task FindOnPaginatedList(ILocator item)
    {
        while (!await item.IsVisibleAsync())
        {
            var next = page.GetByRole(AriaRole.Link, new() { Name = "Next" });
            if (!await next.IsVisibleAsync()) break;
            await next.ClickAsync();
            await VerifyPage();
        }
        await Assertions.Expect(item).ToBeVisibleAsync();
    }

    protected static int ReadAmount(string text) =>
        int.Parse(Regex.Replace(text, @"[^\d-]", ""), CultureInfo.InvariantCulture);
}
