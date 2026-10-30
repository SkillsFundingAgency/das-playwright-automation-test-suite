using SFA.DAS.EmployerPortal.UITests.Project;

namespace SFA.DAS.TransferMatching.UITests.Project.Tests.Pages;

public class ApproveAppliationPage(ScenarioContext context) : TransferMatchingBasePage(context)
{
    protected override string PageTitle => objectContext.GetOrganisationName();
    protected override ILocator ContinueButton => page.Locator("#main-content .govuk-button");

    public async Task<ApprovingTheApprenticeshipDetailsPage> GoToApprovingTheApprenticeshipDetailsPage()
    {
        await Assertions.Expect(page.Locator("#main-content > div:nth-child(3) > p:nth-child(7)")).ToBeVisibleAsync();
        await Assertions.Expect(page.GetByRole(AriaRole.Link, new() { Name = "Date applied", Exact = true })).ToBeVisibleAsync();
        await SelectRadioOptionByText("Approve the application");
        await Assertions.Expect(page.Locator("#approval-content")).ToBeVisibleAsync();
        await Continue();
        return await VerifyPageAsync(() => new ApprovingTheApprenticeshipDetailsPage(context));
    }

    public async Task<TransferPledgePage> RejectApplication()
    {
        await SelectRadioOptionByText("Reject the application");
        await Continue();
        return await VerifyPageAsync(() => new TransferPledgePage(context));
    }
}
