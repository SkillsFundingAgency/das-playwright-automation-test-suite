namespace SFA.DAS.TransferMatching.UITests.Project.Tests.Pages;

public class ApprovingTheApprenticeshipDetailsPage(ScenarioContext context) : TransferMatchingBasePage(context)
{
    protected override string PageTitle => "Approving the apprenticeship details";
    protected override ILocator ContinueButton => page.Locator("#main-content .govuk-button");

    public async Task<AppliationApprovedPage> ManuallyApproveApplication()
    {
        await page.GetByRole(AriaRole.Radio, new() { NameRegex = new("^Check and review apprenticeship details\\b") }).CheckAsync();
        await Continue();
        return await VerifyPageAsync(() => new AppliationApprovedPage(context));
    }

    public async Task<AppliationApprovedPage> AutoApproveApplication()
    {
        await page.GetByRole(AriaRole.Radio, new() { NameRegex = new("^Automatically approve the apprenticeship details\\b") }).CheckAsync();
        await Continue();
        return await VerifyPageAsync(() => new AppliationApprovedPage(context));
    }
}
