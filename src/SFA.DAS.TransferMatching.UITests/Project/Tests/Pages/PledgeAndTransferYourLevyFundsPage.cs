namespace SFA.DAS.TransferMatching.UITests.Project.Tests.Pages;

public class PledgeAndTransferYourLevyFundsPage(ScenarioContext context) : TransferMatchingBasePage(context)
{
    protected override string PageTitle => "Pledge and transfer your levy funds";

    public async Task<CreateATransferPledgePage> StartCreatePledge()
    {
        await page.Locator("[href*='/pledges/create?']").ClickAsync();
        return await VerifyPageAsync(() => new CreateATransferPledgePage(context));
    }

    public async Task<AccessibilityStatementPage> GoToAccessibilityStatementPage() =>
        await FollowLink("Accessibility statement", () => new AccessibilityStatementPage(context));

    public async Task<PledgeAndTransferYourLevyFundsPage> ValidateFooter()
    {
        foreach (var name in new[] { "Help", "Feedback", "Privacy", "Accessibility statement", "Cookies", "Department for Education", "© Crown copyright" })
            await Assertions.Expect(page.GetByRole(AriaRole.Link, new() { Name = name, Exact = true })).ToBeVisibleAsync();
        return this;
    }

    public async Task<PledgeAndTransferYourLevyFundsPage> ValidateHelpWidget()
    {
        await Assertions.Expect(page.Locator("#ze-snippet")).ToHaveCountAsync(1);
        return this;
    }

    public class AccessibilityStatementPage(ScenarioContext context) : TransferMatchingBasePage(context)
    {
        protected override string PageTitle => "Accessibility statement";
    }
}
