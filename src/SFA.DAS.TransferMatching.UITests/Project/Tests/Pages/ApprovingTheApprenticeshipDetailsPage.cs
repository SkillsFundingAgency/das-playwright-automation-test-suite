namespace SFA.DAS.TransferMatching.UITests.Project.Tests.Pages;

public class ApprovingTheApprenticeshipDetailsPage(ScenarioContext context) : TransferMatchingBasePage(context)
{
    protected override string PageTitle => "Approving the apprenticeship details";
    protected override ILocator ContinueButton => page.Locator("#main-content .govuk-button");

    public async Task<AppliationApprovedPage> ManuallyApproveApplication()
    {
        await SelectRadioOptionByText("Check and review apprenticeship details");
        await Continue();
        return await VerifyPageAsync(() => new AppliationApprovedPage(context));
    }

    public async Task<AppliationApprovedPage> AutoApproveApplication()
    {
        await SelectRadioOptionByText("Automatically approve the apprenticeship details");
        await Continue();
        return await VerifyPageAsync(() => new AppliationApprovedPage(context));
    }
}
