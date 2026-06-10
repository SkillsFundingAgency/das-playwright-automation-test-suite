namespace SFA.DAS.RAA.Service.Project.Pages.CreateAdvert;

public class VacancyCompletedAllSectionsPage(ScenarioContext context) : PreviewYourAdvertOrVacancyPage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator(".govuk-notification-banner__heading")).ToBeVisibleAsync();
    }

    private ILocator NotificationBanner => page.Locator(".govuk-notification-banner__heading");
    private ILocator ContinueButton => page.Locator(".govuk-button[type='submit']");
    private ILocator ResubmitVacancyToEmployerButton => page.Locator("[data-automation='continue-button']");
    private ILocator RejectedReasonTextArea => page.Locator("textarea#RejectedReason");
    private ILocator SignoutButton => page.Locator("a[href=\"/signout/\"]");

    public async Task<AreYouSureYouWantToSubmitPage> SubmitAdvert()
    {
        await page.GetByText("Submit advert to DfE for checking and publication").ClickAsync();
        await ContinueButton.ClickAsync();
        return await VerifyPageAsync(() => new AreYouSureYouWantToSubmitPage(context));
    }

    public async Task<AreYouSureYouWantToRejectPage> RejectAdvert()
    {
        await page.GetByText("Reject advert and return to training provider").ClickAsync();
        await RejectedReasonTextArea.FillAsync($"Rejected {vacancyTitleDataHelper.VacancyTitle} by the employer");
        await ContinueButton.ClickAsync();
        return await VerifyPageAsync(() => new AreYouSureYouWantToRejectPage(context));
    }

    public async Task<VacancyReferencePage> ResubmitVacancyToEmployer()
    {
        await ResubmitVacancyToEmployerButton.ClickAsync();
        return await VerifyPageAsync(() => new VacancyReferencePage(context));
    }

    public async Task ClickSignoutButton()
    {
        await SignoutButton.ClickAsync();
    }

    public async Task<string> GetNotificationBanner()
    {
        return await NotificationBanner.TextContentAsync() ?? string.Empty;
    }
}

public class AreYouSureYouWantToSubmitPage(ScenarioContext context) : RaaBasePage(context)
{
    public override async Task VerifyPage()
    {
        string pageTitle = isRaaEmployer ? "Are you sure you want to submit this advert?" : "Are you sure you want to submit this vacancy?";
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync(pageTitle);
    }

    public async Task<VacancyReferencePage> ConfirmSubmit()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Yes, submit" }).ClickAsync();
        return await VerifyPageAsync(() => new VacancyReferencePage(context));
    }

    public async Task<PreviewYourAdvertOrVacancyPage> CancelSubmit()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "No, go back" }).ClickAsync();
        return await VerifyPageAsync(() => new PreviewYourAdvertOrVacancyPage(context));
    }
}

public class AreYouSureYouWantToRejectPage(ScenarioContext context) : RaaBasePage(context)
{
    public override async Task VerifyPage()
    {
        string pageTitle = isRaaEmployer ? "Are you sure you want to reject this advert?" : "Are you sure you want to reject this vacancy?";
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync(pageTitle);
    }

    public async Task<VacancyReferencePage> ConfirmReject()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Yes, reject" }).ClickAsync();
        return await VerifyPageAsync(() => new VacancyReferencePage(context));
    }

    public async Task<PreviewYourAdvertOrVacancyPage> CancelReject()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "No, go back" }).ClickAsync();
        return await VerifyPageAsync(() => new PreviewYourAdvertOrVacancyPage(context));
    }
}
