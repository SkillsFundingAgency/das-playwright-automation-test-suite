using Azure;

namespace SFA.DAS.EPAO.UITests.Project.Tests.Pages.EPAOWithdrawalPages;

public class AS_WithdrawFromAStandardsPage(ScenarioContext context) : EPAO_BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Withdraw from standards");

    public async Task<AS_WhatAreYouWithdrawingFromPage> ClickContinueOnWithdrawFromStandardsPageWhenNoWithdrawalsExist()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new AS_WhatAreYouWithdrawingFromPage(context));
    }

    public async Task<AS_YourWithdrawalRequestsPage> ClickContinueOnWithdrawFromStandardsPageWhenWithdrawalsExist()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new AS_YourWithdrawalRequestsPage(context));
    }
}

public class AS_WhatAreYouWithdrawingFromPage(ScenarioContext context) : EPAO_BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("What are you withdrawing from?");

    public async Task<AS_WhichStandardDoYouWantToWithdrawFromAssessingPage> ClickAssessingASpecificStandard()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "Assessing a specific standard" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Confirm and continue" }).ClickAsync();

        return await VerifyPageAsync(() => new AS_WhichStandardDoYouWantToWithdrawFromAssessingPage(context));
    }

    public async Task<AS_CheckWithdrawalRequestPage> ClickWithdrawFromRegister()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "Assessing all standards" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Confirm and continue" }).ClickAsync();

        return await VerifyPageAsync(() => new AS_CheckWithdrawalRequestPage(context));
    }
}

public class AS_YourWithdrawalRequestsPage(ScenarioContext context) : EPAO_BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Your withdrawal requests");


    public async Task<AS_WhatAreYouWithdrawingFromPage> ClickStartNewWithdrawalNotification()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Create a new withdrawal" }).ClickAsync();

        return await VerifyPageAsync(() => new AS_WhatAreYouWithdrawingFromPage(context));
    }

    public async Task ValidateStatus(string status) => await Assertions.Expect(page.Locator("tbody")).ToContainTextAsync(status);

    public async Task<AS_WithdrawalRequestOverviewPage> ClickOnViewLinkForInProgressApplication()
    {
        await page.GetByRole(AriaRole.Row, new() { Name = "Standard Brewer In progress" }).GetByRole(AriaRole.Link).ClickAsync();

        return await VerifyPageAsync(() => new AS_WithdrawalRequestOverviewPage(context));
    }

    public async Task<AS_FeedbackOnYourWithdrawalNotificationStartPage> ClickViewOnRegisterWithdrawalWithFeedbackAdded()
    {
        await page.GetByRole(AriaRole.Row, new() { Name = "Feedback added" }).GetByRole(AriaRole.Link, new() { Name = "View register withdrawal" }).ClickAsync();

        return await VerifyPageAsync(() => new AS_FeedbackOnYourWithdrawalNotificationStartPage(context));
    }

}

public class AS_FeedbackOnYourWithdrawalNotificationStartPage(ScenarioContext context) : EPAO_BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Feedback on your withdrawal request");

    public async Task<AS_WithdrawalRequestOverviewPage> ClickContinueButton()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new AS_WithdrawalRequestOverviewPage(context));
    }
}


public class AS_WhichStandardDoYouWantToWithdrawFromAssessingPage(ScenarioContext context) : EPAO_BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Which standard do you want to withdraw from assessing?");

    public async Task<AS_CheckWithdrawalRequestPage> ClickASpecificStandardToWithdraw()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Withdraw from standard  Brewer" }).ClickAsync();

        return await VerifyPageAsync(() => new AS_CheckWithdrawalRequestPage(context));
    }
}

public class AS_CheckWithdrawalRequestPage(ScenarioContext context) : EPAO_BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Check withdrawal request");

    public async Task<AS_WithdrawalRequestOverviewPage> ContinueWithWithdrawalRequest()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "Yes" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Confirm and continue" }).ClickAsync();

        return await VerifyPageAsync(() => new AS_WithdrawalRequestOverviewPage(context));
    }
}

public class AS_WithdrawalRequestOverviewPage(ScenarioContext context) : EPAO_BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Withdrawal request overview");

    public async Task<AS_WithdrawalRequestQuestionsPage> ClickGoToStandardWithdrawalQuestions()
    {
        await Assertions.Expect(page.Locator("#main-content")).ToContainTextAsync("ST0580 Brewer");

        await Assertions.Expect(page.Locator("#main-content")).ToContainTextAsync("0 of 4 questions completed");

        await page.GetByRole(AriaRole.Link, new() { Name = "Go to withdrawal request" }).ClickAsync();

        return await VerifyPageAsync(() => new AS_WithdrawalRequestQuestionsPage(context));
    }

    public async Task<AS_WithdrawalRequestQuestionsPage> ClickGoToRegisterWithdrawalQuestions()
    {
        await Assertions.Expect(page.Locator("#main-content")).ToContainTextAsync("Withdrawing from all standards");

        await Assertions.Expect(page.Locator("#main-content")).ToContainTextAsync("0 of 4 questions completed");

        await page.GetByRole(AriaRole.Link, new() { Name = "Go to withdrawal request" }).ClickAsync();

        return await VerifyPageAsync(() => new AS_WithdrawalRequestQuestionsPage(context));
    }

    public async Task<AS_HowWillYouSupportTheLearnersYouAreNotGoingToAssessPage> ClickSupportingCurrentLearnersFeedback()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Supporting current learners" }).ClickAsync();

        return await VerifyPageAsync(() => new AS_HowWillYouSupportTheLearnersYouAreNotGoingToAssessPage(context));
    }

    public async Task AcceptAndSubmit()
    {
        await Assertions.Expect(page.Locator("#main-content")).ToContainTextAsync("4 of 4 questions completed");

        await page.GetByRole(AriaRole.Button, new() { Name = "Accept and submit" }).ClickAsync();
    }

    public async Task SubmitUpdatedAnswers() => await page.GetByRole(AriaRole.Button, new() { Name = "Submit your answers" }).ClickAsync();

    public async Task AcceptAndSubmitWithHowWillYouSuportQuestion()
    {
        await Assertions.Expect(page.Locator("#main-content")).ToContainTextAsync("5 of 5 questions completed");

        await page.GetByRole(AriaRole.Button, new() { Name = "Accept and submit" }).ClickAsync();
    }
}


public class AS_WithdrawalRequestQuestionsPage(ScenarioContext context) : EPAO_BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Withdrawal request questions");

    public async Task<AS_WhatIsTheMainReasonYouWantToWithdrawStandardPage> ClickGoToReasonForWithdrawingQuestionLink()
    {
        await Assertions.Expect(page.Locator("#main-content")).ToContainTextAsync("ST0580 Brewer");

        await page.GetByRole(AriaRole.Link, new() { Name = "Reason for withdrawing" }).ClickAsync();

        return await VerifyPageAsync(() => new AS_WhatIsTheMainReasonYouWantToWithdrawStandardPage(context));
    }

    public async Task<AS_WhatIsTheMainReasonYouWantToWithdrawFromAllStandardsPage> ClickGoToReasonForWithdrawingFromRegisterQuestionLink()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Reason for withdrawing" }).ClickAsync();

        return await VerifyPageAsync(() => new AS_WhatIsTheMainReasonYouWantToWithdrawFromAllStandardsPage(context));
    }

    public async Task<AS_WithdrawalRequestOverviewPage> VerifyAndReturnToApplicationOverviewPage()
    {
        await Assertions.Expect(page.Locator("ol")).ToContainTextAsync("Reason for withdrawing Completed");

        await Assertions.Expect(page.Locator("ol")).ToContainTextAsync("Completing assessments for registered learners Completed");

        await Assertions.Expect(page.Locator("ol")).ToContainTextAsync("Communicating your market exit to customers Completed");

        await Assertions.Expect(page.Locator("ol")).ToContainTextAsync("Withdrawal date Completed");

        await page.GetByRole(AriaRole.Link, new() { Name = "Return to application overview" }).ClickAsync();

        return await VerifyPageAsync(() => new AS_WithdrawalRequestOverviewPage(context));
    }

    public async Task<AS_WithdrawalRequestOverviewPage> VerifyWithSupportingLearnersQuestionAndReturnToApplicationOverviewPage()
    {
        await Assertions.Expect(page.Locator("ol")).ToContainTextAsync("Reason for withdrawing Completed");

        await Assertions.Expect(page.Locator("ol")).ToContainTextAsync("Completing assessments for registered learners Completed");

        await Assertions.Expect(page.Locator("ol")).ToContainTextAsync("Communicating your market exit to customers Completed");

        await Assertions.Expect(page.Locator("ol")).ToContainTextAsync("Withdrawal date Completed");

        await Assertions.Expect(page.Locator("ol")).ToContainTextAsync("Supporting current learners Completed");

        await page.GetByRole(AriaRole.Link, new() { Name = "Return to application overview" }).ClickAsync();

        return await VerifyPageAsync(() => new AS_WithdrawalRequestOverviewPage(context));
    }
}

public class AS_WhatIsTheMainReasonYouWantToWithdrawFromAllStandardsPage(ScenarioContext context) : EPAO_BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("What's the main reason you want to withdraw from all standards?");

    public async Task<AS_WillYouCompleteEPAOForAllRegisteredLearnersPage> ClickAssessmentPlanHasChangedAndEnterOptionalReason()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "Assessment plan has changed" }).CheckAsync();
        
        await page.GetByRole(AriaRole.Textbox, new() { Name = "Enter more details (optional)" }).FillAsync("Assessment plan has changed");

        await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();


        return await VerifyPageAsync(() => new AS_WillYouCompleteEPAOForAllRegisteredLearnersPage(context));
    }
}

public class AS_WhatIsTheMainReasonYouWantToWithdrawStandardPage(ScenarioContext context) : EPAO_BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("What's the main reason you want to withdraw from assessing the standard?");

    public async Task<AS_WillYouCompleteEPAOForAllRegisteredLearnersPage> ClickExternalQualityAssuranceProviderHasChanged()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "External quality assurance" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();

        return await VerifyPageAsync(() => new AS_WillYouCompleteEPAOForAllRegisteredLearnersPage(context));
    }
}

public class AS_WillYouCompleteEPAOForAllRegisteredLearnersPage(ScenarioContext context) : EPAO_BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Will you complete the end-point assessments for all registered learners?");

    public async Task<AS_HowWillYouCommunicateMarketExitToCustomersPage> ClickYesAndContinue()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "Yes" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();

        return await VerifyPageAsync(() => new AS_HowWillYouCommunicateMarketExitToCustomersPage(context));
    }

    public async Task<AS_HowWillYouSupportTheLearnersYouAreNotGoingToAssessPage> ClickNoAndContinue()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "No" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();

        return await VerifyPageAsync(() => new AS_HowWillYouSupportTheLearnersYouAreNotGoingToAssessPage(context));
    }
}


public class AS_HowWillYouSupportTheLearnersYouAreNotGoingToAssessPage(ScenarioContext context) : EPAO_BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("label")).ToContainTextAsync("How will you support the learners you are not going to assess?");

    public async Task<AS_HowWillYouCommunicateMarketExitToCustomersPage> EnterAnswerForHowWillYouSupportLearnerYouAreNotGoingToAssess()
    {
        await page.GetByRole(AriaRole.Textbox, new() { Name = "How will you support the" }).FillAsync("Learners will be supported");

        await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();

        return await VerifyPageAsync(() => new AS_HowWillYouCommunicateMarketExitToCustomersPage(context));
    }

    public async Task<AS_WithdrawalRequestOverviewPage> UpdateAnswerForHowWillYouSupportLearnersYouAreNotGoingToAssess()
    {
        await page.GetByRole(AriaRole.Textbox, new() { Name = "How will you support the" }).FillAsync("Learners will be supported by another training provider");

        await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();

        return await VerifyPageAsync(() => new AS_WithdrawalRequestOverviewPage(context));
    }
}


public class AS_HowWillYouCommunicateMarketExitToCustomersPage(ScenarioContext context) : EPAO_BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("label")).ToContainTextAsync("How will you communicate your market exit to customers?");

    public async Task<AS_WhenDoYouWantToWithdrawFromTheStandardPage> EnterSupportingInformationForStandardWithdrawal()
    {
        await page.GetByRole(AriaRole.Textbox, new() { Name = "How will you communicate your" }).FillAsync(EPAOApplyStandardDataHelper.GenerateRandomAlphanumericString(250));

        await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();

        return await VerifyPageAsync(() => new AS_WhenDoYouWantToWithdrawFromTheStandardPage(context));
    }

    public async Task<AS_WhenDoYouWantToWithdrawFromAllStandardsPage> EnterSupportingInformationForRegisterWithdrawal()
    {
        await page.GetByRole(AriaRole.Textbox, new() { Name = "How will you communicate your" }).FillAsync(EPAOApplyStandardDataHelper.GenerateRandomAlphanumericString(250));

        await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();

        return await VerifyPageAsync(() => new AS_WhenDoYouWantToWithdrawFromAllStandardsPage(context));
    }
}

public class AS_WhenDoYouWantToWithdrawFromAllStandardsPage(ScenarioContext context) : AS_WhenDoYouWantToWithdrawBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("When do you want to withdraw from all standards?");
}

public class AS_WhenDoYouWantToWithdrawFromTheStandardPage(ScenarioContext context) : AS_WhenDoYouWantToWithdrawBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("When do you want to withdraw from assessing the standard?");

}

public abstract class AS_WhenDoYouWantToWithdrawBasePage : EPAO_BasePage
{
    public AS_WhenDoYouWantToWithdrawBasePage(ScenarioContext context) : base(context) => VerifyPage();

    public async Task<AS_WithdrawalRequestQuestionsPage> EnterDateToWithdraw()
    {
        var date = DateTime.Now.AddMonths(7).AddDays(2);

        await page.GetByRole(AriaRole.Spinbutton, new() { Name = "Day" }).FillAsync(date.ToString("dd"));

        await page.GetByRole(AriaRole.Spinbutton, new() { Name = "Month" }).FillAsync(date.ToString("MM"));

        await page.GetByRole(AriaRole.Spinbutton, new() { Name = "Year" }).FillAsync(date.ToString("yyyy"));

        await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();

        return await VerifyPageAsync(() => new AS_WithdrawalRequestQuestionsPage(context));
    }
}

public class AS_WithdrawalApplicationSubmittedPage(ScenarioContext context) : EPAO_BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Withdrawal request submitted");

}