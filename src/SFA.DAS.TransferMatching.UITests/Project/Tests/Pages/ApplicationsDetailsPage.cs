namespace SFA.DAS.TransferMatching.UITests.Project.Tests.Pages;

public class ApplicationsDetailsPage(ScenarioContext context, string applicationStatus) : TransferMatchingBasePage(context)
{
    protected override string PageTitle => $"({GetPledgeId()}) application details";
    protected override ILocator ContinueButton => page.Locator("#fund-transfer-accept");

    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("#main-content strong.govuk-tag").First)
            .ToContainTextAsync(applicationStatus);
        // Accepted applications have a different heading but still identify their pledge.
        await Assertions.Expect(PageHeader).ToContainTextAsync(
            applicationStatus == ApplicationStatus.Accepted.GetLabelForReceiver() ? GetPledgeId() : PageTitle);
    }

    public async Task<ApplicationsDetailsPage> SetPledgeApplication()
    {
        var applicationId = new Uri(page.Url).AbsolutePath.TrimEnd('/').Split('/').Last();
        objectContext.SetPledgeApplication(GetPledgeId(), applicationId);
        await VerifyPage();
        return this;
    }

    public async Task<AcceptedTransferPage> VerifyAgreeToTermsIsMandatoryAndAcceptFunding()
    {
        await SelectRadioOptionByText("Yes, accept the funding");
        await ContinueAndVerifyTermsError();
        await page.Locator("#TruthfulInformation").ClickAsync();
        await ContinueAndVerifyTermsError();
        await page.Locator("#TruthfulInformation").ClickAsync();
        await page.Locator("#ComplyWithRules").ClickAsync();
        await ContinueAndVerifyTermsError();
        await page.Locator("#TruthfulInformation").ClickAsync();
        await Continue();
        return await VerifyPageAsync(() => new AcceptedTransferPage(context));
    }

    public async Task<SuccessfullyWithdrawnPage> WithdrawFunding()
    {
        await SelectRadioOptionByText("No, decline the funding and withdraw the application");
        await Continue();
        await Assertions.Expect(page.Locator(".govuk-error-summary"))
            .ToContainTextAsync("You must confirm that you want to decline the funding and withdraw the application");
        await page.Locator("input#IsDeclineConfirmed").CheckAsync();
        await Continue();
        return await VerifyPageAsync(() => new SuccessfullyWithdrawnPage(context));
    }

    public async Task<SuccessfullyWithdrawnYourApplicationPage> WithdrawBeforeApproval()
    {
        await SelectRadioOptionByText("Yes, withdraw the application");
        await page.Locator("input#IsWithdrawalConfirmed").CheckAsync();
        await page.Locator("#main-content > div > div.govuk-grid-column-two-thirds > form > div.govuk-button-group > button").ClickAsync();
        return await VerifyPageAsync(() => new SuccessfullyWithdrawnYourApplicationPage(context));
    }

    // The form posts back to the same URL (POST -> 302 -> GET), so the previous error summary is still on screen
    // until the new document loads; wait for that document before asserting or interacting with the form again.
    private async Task ContinueAndVerifyTermsError()
    {
        await page.RunAndWaitForResponseAsync(Continue,
            r => r.Request.IsNavigationRequest && r.Request.Method == "GET" && r.Url == page.Url);
        await page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
        await Assertions.Expect(page.Locator(".govuk-error-summary"))
            .ToContainTextAsync("You must agree to the terms and conditions before accepting funding for this application");
    }
}
