using SFA.DAS.EmployerPortal.UITests.Project;

namespace SFA.DAS.TransferMatching.UITests.Project.Tests.Pages;

public class ApproveAppliationPage(ScenarioContext context) : TransferMatchingBasePage(context)
{
    protected override string PageTitle => objectContext.GetOrganisationName();
    protected override ILocator ContinueButton => page.Locator("#main-content .govuk-button");

    public async Task<ApprovingTheApprenticeshipDetailsPage> GoToApprovingTheApprenticeshipDetailsPage()
    {
        await Assertions.Expect(page.GetByRole(AriaRole.Heading, new() { Name = "Estimated cost of training and assessment", Exact = true }))
            .ToBeVisibleAsync();
        await Assertions.Expect(page.Locator("dt").Filter(new() { HasText = "Date applied" })).ToBeVisibleAsync();
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
