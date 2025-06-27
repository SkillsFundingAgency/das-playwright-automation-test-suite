namespace SFA.DAS.EPAO.UITests.Project.Tests.Pages.AssessmentService;

public class AS_ConfirmApprenticePage(ScenarioContext context) : EPAO_BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1").First).ToContainTextAsync("Confirm this is the correct apprentice");

    public async Task<AS_AssesmentAlreadyRecorded> GoToAssesmentAlreadyRecordedPage()
    {
        await SelectStandard(true);

        return await VerifyPageAsync(() => new AS_AssesmentAlreadyRecorded(context));
    }

    public async Task<AS_ConfirmApprenticePage> ViewCertificateHistory()
    {
        await page.GetByText("View certificate history").ClickAsync();

        return await VerifyPageAsync(() => new AS_ConfirmApprenticePage(context));
    }

    public async Task<AS_WhichVersionPage> GoToWhichVersionPage(bool hasMultiStandards)
    {
        await SelectStandard(hasMultiStandards);

        return await VerifyPageAsync(() => new AS_WhichVersionPage(context));
    }

    public async Task<AS_WhichLearningOptionPage> GoToWhichLearningOptionPage(bool hasMultiStandards)
    {
        await SelectStandard(hasMultiStandards);

        return await VerifyPageAsync(() => new AS_WhichLearningOptionPage(context));
    }

    public async Task<AS_DeclarationPage> GoToDeclarationPage(bool hasMultiStandards)
    {
        await SelectStandard(hasMultiStandards);

        return await VerifyPageAsync(() => new AS_DeclarationPage(context));
    }

    public async Task<AS_DeclarationPage> ConfirmAndContinue()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Confirm and continue" }).ClickAsync();

        return await VerifyPageAsync(() => new AS_DeclarationPage(context));
    }

    private async Task SelectStandard(bool hasMultiStandards)
    {
        if (hasMultiStandards)
        {
            var standardName = objectContext.GetLearnerStandardName();

            if (string.IsNullOrEmpty(standardName)) await SelectRandomRadioOption(); 

            else await page.GetByRole(AriaRole.Radio, new() { Name = standardName, Exact = true }).CheckAsync();

        }

        await page.GetByRole(AriaRole.Button, new() { Name = "Confirm and continue" }).ClickAsync();
    }
}

public class AS_DeclarationPage(ScenarioContext context) : EPAO_BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Declaration");

    public async Task ClickConfirmInDeclarationPage() => await page.GetByRole(AriaRole.Button, new() { Name = "Confirm and record a grade" }).ClickAsync();
}

public class AS_WhatGradePage(ScenarioContext context) : EPAOAssesment_BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("What grade did the apprentice achieve?");

    public async Task SelectGradeAndEnterDate(string grade)
    {
        var page = await SelectGrade(grade);

        await page.EnterAchievementGradeDateAndContinue();
    }

    public async Task<AS_AchievementDatePage> SelectGradeAsPass() => await SelectGradeAsPass("Pass");

    private async Task<AS_GradeDateBasePage> SelectGrade(string grade)
    {
        return grade switch
        {
            "PassWithExcellence" => await SelectGradeAsPass("Pass with excellence"),
            "fail" => await SelectGradeAsFail(),
            _ => await SelectGradeAsPass("Pass"),
        };
    }

    private async Task<AS_ApprenticeFailedDatePage> SelectGradeAsFail()
    {
        await SelectAGrade("Fail");

        return await VerifyPageAsync(() => new AS_ApprenticeFailedDatePage(context));
    }

    private async Task<AS_AchievementDatePage> SelectGradeAsPass(string name)
    {
        await SelectAGrade(name);

        return await VerifyPageAsync(() => new AS_AchievementDatePage(context));
    }

    private async Task SelectAGrade(string name)
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = name, Exact = true }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();
    }
}

public class AS_ApprenticeFailedDatePage(ScenarioContext context) : AS_GradeDateBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("What is the date the apprentice failed?");
}

public class AS_AchievementDatePage(ScenarioContext context) : AS_GradeDateBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("What is the apprenticeship achievement date?");
}

public abstract class AS_GradeDateBasePage : EPAOAssesment_BasePage
{
    public AS_GradeDateBasePage(ScenarioContext context) : base(context) => VerifyPage();

    /*PASS and FAIL scenarios return two different pages, so do NOT return any page for this method*/
    public async Task EnterAchievementGradeDateAndContinue()
    {
        await page.GetByRole(AriaRole.Textbox, new() { Name = "Day" }).FillAsync($"{ePAOAssesmentServiceDataHelper.CurrentDay}");

        await page.GetByRole(AriaRole.Textbox, new() { Name = "Month" }).FillAsync($"{ePAOAssesmentServiceDataHelper.CurrentMonth}");

        await page.GetByRole(AriaRole.Textbox, new() { Name = "Year" }).FillAsync($"{ePAOAssesmentServiceDataHelper.CurrentYear}");

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();
    }
}

public class AS_WhoWouldYouLikeUsToSendTheCertificateToPage(ScenarioContext context) : EPAOAssesment_BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Who would you like us to send the certificate to?");

    public async Task<AS_SearchEmployerAddressPage> ClickAprenticeRadioButton()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "Apprentice" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new AS_SearchEmployerAddressPage(context));
    }

    public async Task<AS_SearchEmployerOrAddressPage> ClickEmployerRadioButton()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "Employer" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new AS_SearchEmployerOrAddressPage(context));
    }
}
public class AS_SearchEmployerAddressPage(ScenarioContext context) : EPAOAssesment_BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Search for the address that you'd like us to send the certificate to Add the address that you’d like us to send the certificate to Where will the certificate be sent?");

    public async Task<AS_AddEmployerAddress> ClickEnterAddressManuallyLinkInSearchEmployerPage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Enter address manually" }).ClickAsync();

        return await VerifyPageAsync(() => new AS_AddEmployerAddress(context));
    }
}


public class AS_SearchEmployerOrAddressPage(ScenarioContext context) : EPAOAssesment_BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Search for an employer or address");

    public async Task<AS_AddEmployerAddress> ClickEnterAddressManuallyLinkInSearchEmployerPage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Enter address manually" }).ClickAsync();

        return await VerifyPageAsync(() => new AS_AddEmployerAddress(context));
    }
}

public class AS_AddEmployerAddress(ScenarioContext context) : EPAO_BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Add the address that you’d like us to send the certificate to");

    public async Task<AS_ConfirmAddressPage> EnterEmployerAddressAndContinue()
    {
        await EnterAddressAndContinue();

        return await VerifyPageAsync(() => new AS_ConfirmAddressPage(context));
    }

    public async Task<AS_AddRecipientsDetailsPage> EnterEmployerNameAndAddressAndContinue()
    {
        await page.GetByRole(AriaRole.Textbox, new() { Name = "Employer name" }).FillAsync("Nasdaq");

        await EnterAddressAndContinue();

        return await VerifyPageAsync(() => new AS_AddRecipientsDetailsPage(context));
    }

    private async Task EnterAddressAndContinue()
    {
        await page.GetByRole(AriaRole.Textbox, new() { Name = "Building and street" }).FillAsync(EPAODataHelper.AddressLine1);

        await page.GetByRole(AriaRole.Textbox, new() { Name = "AccessiblyAddressLine2" }).FillAsync(EPAODataHelper.AddressLine2);

        await page.GetByRole(AriaRole.Textbox, new() { Name = "AccessiblyAddressLine3" }).FillAsync(EPAODataHelper.AddressLine3);

        await page.GetByRole(AriaRole.Textbox, new() { Name = "Town or city" }).FillAsync(EPAODataHelper.TownName);

        await page.GetByRole(AriaRole.Textbox, new() { Name = "Postcode" }).FillAsync(EPAODataHelper.PostCode);

        await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();

    }
}

public class AS_AddRecipientsDetailsPage(ScenarioContext context) : EPAOAssesment_BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Add recipient's details");

    public async Task<AS_ConfirmAddressPage> AddRecipientAndContinue()
    {
        await page.GetByRole(AriaRole.Textbox, new() { Name = "Recipient's name" }).FillAsync("Tesco");

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new AS_ConfirmAddressPage(context));
    }
}

public class AS_ConfirmAddressPage(ScenarioContext context) : EPAO_BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Confirm where we are sending the certificate");

    public async Task<AS_CheckAndSubmitAssessmentPage> ClickContinueInConfirmEmployerAddressPage()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new AS_CheckAndSubmitAssessmentPage(context));
    }
}

public class AS_CheckAndSubmitAssessmentPage(ScenarioContext context) : EPAOAssesment_BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Check and submit the assessment details");

    public async Task<AS_AssessmentRecordedPage> ClickContinueInCheckAndSubmitAssessmentPage()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Confirm and submit" }).ClickAsync();

        return await VerifyPageAsync(() => new AS_AssessmentRecordedPage(context));
    }

    public async Task<AS_WhatGradePage> ClickGradeChangeLink()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Change grade" }).ClickAsync();

        return await VerifyPageAsync(() => new AS_WhatGradePage(context));
    }

    public async Task<AS_WhichLearningOptionPage> ClickOptionChangeLink()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Change option" }).ClickAsync();

        return await VerifyPageAsync(() => new AS_WhichLearningOptionPage(context));
    }

    public async Task<AS_AchievementDatePage> ClickAchievementDateChangeLink()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Change date" }).ClickAsync();

        return await VerifyPageAsync(() => new AS_AchievementDatePage(context));
    }

    public async Task<AS_WhoWouldYouLikeUsToSendTheCertificateToPage> ClickCertificateReceiverLink()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Change certificate receiver" }).ClickAsync();

        return await VerifyPageAsync(() => new AS_WhoWouldYouLikeUsToSendTheCertificateToPage(context));
    }

    public async Task<AS_AddRecipientsDetailsPage> ClickDepartmentChangeLinkForEmployerJourney()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Change department" }).ClickAsync();

        return await VerifyPageAsync(() => new AS_AddRecipientsDetailsPage(context));
    }

    public async Task<AS_SearchEmployerAddressPage> ClickCertificateAddressChangeLinkvForApprenticeJourney()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Change address" }).ClickAsync();

        return await VerifyPageAsync(() => new AS_SearchEmployerAddressPage(context));
    }

    public async Task<AS_SearchEmployerOrAddressPage> ClickCertificateAddressChangeLinkForEmployerJourney()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Change address" }).ClickAsync();

        return await VerifyPageAsync(() => new AS_SearchEmployerOrAddressPage(context));
    }
}

public class AS_AssessmentRecordedPage(ScenarioContext context) : EPAO_BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Assessment recorded");

    public async Task<AS_RecordAGradePage> ClickRecordAnotherGradeLink()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Record another grade" }).ClickAsync();

        return await VerifyPageAsync(() => new AS_RecordAGradePage(context));
    }
}

public class AS_AssesmentAlreadyRecorded(ScenarioContext context) : EPAO_BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("An assessment has already been recorded against this apprentice");
}


public class AS_WhichLearningOptionPage(ScenarioContext context) : EPAOAssesment_BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Choose the option for");

    public async Task<AS_DeclarationPage> SelectLearningOptionAndContinue()
    {
        await SelectRandomRadioOption();

        await page.GetByRole(AriaRole.Button, new() { Name = "Confirm and continue" }).ClickAsync();

        return await VerifyPageAsync(() => new AS_DeclarationPage(context));
    }
}

public class AS_WhichVersionPage(ScenarioContext context) : EPAO_BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Which version of the");

    public async Task<AS_WhichLearningOptionPage>  ClickConfirmInConfirmVersionPage()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "Version 1.0" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Confirm and continue" }).ClickAsync();

        return await VerifyPageAsync(() => new AS_WhichLearningOptionPage(context));
    }

    public async Task<AS_DeclarationPage>  ClickConfirmInConfirmVersionPageNoOption()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "Version 1.0" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Confirm and continue" }).ClickAsync();

        return await VerifyPageAsync(() => new AS_DeclarationPage(context));
    }
}
