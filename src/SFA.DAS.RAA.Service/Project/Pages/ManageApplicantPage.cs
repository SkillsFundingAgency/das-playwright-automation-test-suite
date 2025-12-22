using static SFA.DAS.RAA.Service.Project.Pages.ConfirmApplicantPage;

namespace SFA.DAS.RAA.Service.Project.Pages;

public class ManageApplicantPage(ScenarioContext context) : RaaBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync(rAADataHelper.CandidateFullName);
    }

    private async Task OutcomeInterviewingRadioButton()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "Mark application for" }).CheckAsync();

        await SaveAndContinue();
    }

    private async Task OutcomeReviewed()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "Review" }).CheckAsync();

        await SaveAndContinue();
    }

    public async Task<ProviderAndEmployerReviewingApplicantPage> MarkApplicantInReview()
    {
        await OutcomeReviewed();

        return await VerifyPageAsync(() => new ProviderAndEmployerReviewingApplicantPage(context));
    }

    //public async Task<ProviderInteviewingApplicantPage> MarkApplicantInterviewWithEmployer()
    //{
    //    await OutcomeInterviewingRadioButton();

    //    return await VerifyPageAsync(() => new ProviderInteviewingApplicantPage(context));
    //}
    public async Task<EmployerInteviewingApplicantPage> MarkApplicantAsInterviewing()
    {
        await OutcomeInterviewingRadioButton();

        return await VerifyPageAsync(() => new EmployerInteviewingApplicantPage(context));
    }

    private async Task OutcomeSharedWithEmployer()
    {
        await SelectRadioOptionByForAttribute("outcome-shared");

        await SaveAndContinue();
    }

    public async Task<ConfirmApplicantSucessfulPage> MakeApplicantSucessful()
    {
        if (IsFoundationAdvert)
        {
            await CheckFoundationTag();
        }
        await Outcomesuccessful();

        return await VerifyPageAsync(() => new ConfirmApplicantSucessfulPage(context));
    }

    public async Task<ConfirmApplicantUnsuccessfulPage> MakeApplicantUnsucessful()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "Make unsuccessful and give" }).CheckAsync();

        await page.GetByRole(AriaRole.Textbox, new() { Name = "Feedback" }).FillAsync(rAADataHelper.OptionalMessage);

        await SaveAndContinue();

        return await VerifyPageAsync(() => new ConfirmApplicantUnsuccessfulPage(context));
    }


    //public async Task<ProviderAreYouSureSuccessfulPage> ProviderMakeApplicantSucessful()
    //{
    //    if (IsFoundationAdvert)
    //    {
    //        await CheckFoundationTag();
    //    }
    //    await Outcomesuccessful();

    //    return await VerifyPageAsync(() => new ProviderAreYouSureSuccessfulPage(context));
    //}

    //public async Task<ProviderGiveFeedbackPage> ProviderMakeApplicantUnsucessful()
    //{
    //    await SelectRadioOptionByForAttribute("outcome-unsuccessful");

    //    await SaveAndContinue();

    //    return await VerifyPageAsync(() => new ProviderGiveFeedbackPage(context));
    //}

    private async Task Outcomesuccessful()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "Make successful" }).CheckAsync();

        await SaveAndContinue();
    }

    private async Task SaveAndContinue() => await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();
}
