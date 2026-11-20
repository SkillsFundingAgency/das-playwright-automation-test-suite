namespace SFA.DAS.TransferMatching.UITests.Project.Tests.Pages;

public class AboutYourApprenticeshipPage(ScenarioContext context) : TransferMatchingBasePage(context)
{
    protected override string PageTitle => "Provide more details about your training";
    protected override ILocator PageHeader => page.Locator(".govuk-label--xl");
    protected override ILocator ContinueButton => page.Locator("#opportunity-criteria-continue");

    public async Task<CreateATransfersApplicationPage> EnterMoreDetailsAndContinue(string pledgeId)
    {
        await page.Locator("#more-detail").FillAsync(!string.IsNullOrEmpty(pledgeId) ? pledgeId : TMDataHelper.ApprenticeshipMoreDetails);
        await Continue();
        return await VerifyPageAsync(() => new CreateATransfersApplicationPage(context));
    }
}
