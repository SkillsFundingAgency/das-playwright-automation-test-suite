using SFA.DAS.EmployerPortal.UITests.Project;
using SFA.DAS.EmployerPortal.UITests.Project.Pages.StubPages;

namespace SFA.DAS.TransferMatching.UITests.Project.Tests.Pages;

public class TransferFundDetailsPage : TransferMatchingBasePage
{
    private readonly bool? isAnonymousPledge;

    public TransferFundDetailsPage(ScenarioContext context) : base(context) { }
    public TransferFundDetailsPage(ScenarioContext context, bool isAnonymousPledge) : base(context) =>
        this.isAnonymousPledge = isAnonymousPledge;

    protected override string PageTitle => "Transfer fund details for";
    protected override ILocator ContinueButton => page.Locator("#apply-application-continue");

    public override async Task VerifyPage()
    {
        await base.VerifyPage();
        if (isAnonymousPledge is null) return;
        var title = isAnonymousPledge.Value ? $"opportunity ({GetPledgeId()})"
            : $"{objectContext.GetOrganisationName()} ({GetPledgeId()})";
        await Assertions.Expect(PageHeader).ToContainTextAsync($"Transfer fund details for {title}");
        await Assertions.Expect(page.Locator(".das-highlight")).ToContainTextAsync(
            isAnonymousPledge.Value ? "A levy-paying business wants to fund training"
                : $"{objectContext.GetOrganisationName()} ({GetPledgeId()}) wants to fund training");
    }

    public async Task<StubSignInEmployerPage> ApplyForTransferFunds()
    {
        await SelectYesAndContinue();
        return await VerifyPageAsync(() => new StubSignInEmployerPage(context));
    }

    public async Task<CreateATransfersApplicationPage> ApplyForTransferFundsAfterLogin()
    {
        await SelectYesAndContinue();
        return await VerifyPageAsync(() => new CreateATransfersApplicationPage(context));
    }

    private async Task SelectYesAndContinue()
    {
        await SelectRadioOptionByText("Yes, apply for transfer funds");
        await Continue();
    }
}
