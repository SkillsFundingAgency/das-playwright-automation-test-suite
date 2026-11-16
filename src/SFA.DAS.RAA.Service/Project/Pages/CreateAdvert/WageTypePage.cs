using System.Security.AccessControl;

namespace SFA.DAS.RAA.Service.Project.Pages.CreateAdvert;

public class WageTypePage(ScenarioContext context) : RaaBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("How much will the apprentice be paid?");
    }

    public async Task TriggerValidationErrorsOnWageTypes()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();
        await CheckValidationErrors("Select how much the apprentice will be paid");

        await EnterNationalMinWages();
        await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();
        await page.GetByRole(AriaRole.Link, new() { Name = "Back", Exact = true }).ClickAsync();

        await page.GetByRole(AriaRole.Radio, new() { Name = "Set wage yourself" }).CheckAsync();
        await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();
        await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();
        await CheckValidationErrors("Yearly wage must be at least £16,640");

        await page.GetByRole(AriaRole.Link, new() { Name = "Back", Exact = true }).ClickAsync();
        await EnterSetAsCompetitive();
        await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();
        await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();
        var errorMessage = isRaaEmployer
            ? "Select if the salary is above National Minimum Wage"
            : "Select whether the salary will be competitive or not.";
        await CheckValidationErrors(errorMessage);

        await page.GetByRole(AriaRole.Link, new() { Name = "Back", Exact = true }).ClickAsync();
    }
    public async Task<ExtraInformationAboutPayPage> ChooseWage_Employer(string wageType)
    {
        await ChooseWage(wageType);

        await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();

        return await VerifyPageAsync(() => new ExtraInformationAboutPayPage(context));
    }

    public async Task<ExtraInformationAboutPayPage> ChooseWage_Provider(string wageType)
    {
        await ChooseWage(wageType);

        await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();

        return await VerifyPageAsync(() => new ExtraInformationAboutPayPage(context));
    }

    private async Task ChooseWage(string wageType)
    {
        if (wageType == RAAConst.NationalMinWages) await EnterNationalMinWages();

        else if (wageType == RAAConst.FixedWageType) await EnterFixedWageType();
        else if (wageType == RAAConst.SetAsCompetitive)
        {
            await EnterSetAsCompetitive();
            await ExtraInformationAboutWage();
        }

        else if (wageType == RAAConst.NationalMinWagesForApprentices)
        {
            await EnterNationalMinimumWageForApprentices();
        }
        else
        {
            await EnterNationalMinWages();

            await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();

            await page.GetByRole(AriaRole.Link, new() { Name = "Back", Exact = true }).ClickAsync();

            await EnterFixedWageType();

            await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();

            await page.GetByRole(AriaRole.Link, new() { Name = "Back", Exact = true }).ClickAsync();

            await page.GetByRole(AriaRole.Link, new() { Name = "Back", Exact = true }).ClickAsync();

            await EnterSetAsCompetitive();

            await ExtraInformationAboutWage();

            await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();

            await page.GetByRole(AriaRole.Link, new() { Name = "Back", Exact = true }).ClickAsync();

            await page.GetByRole(AriaRole.Link, new() { Name = "Back", Exact = true }).ClickAsync();

            await EnterNationalMinimumWageForApprentices();
        }
    }

    public async Task<CompetitiveWagePage> ExtraInformationAboutWage()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();

        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Set as competitive");

        await SubmitYes();

        return await VerifyPageAsync(() => new CompetitiveWagePage(context));
    }

    private async Task EnterNationalMinWages() => await page.GetByRole(AriaRole.Radio, new() { Name = "National Minimum Wage", Exact = true }).CheckAsync();

    private async Task EnterNationalMinimumWageForApprentices() => await page.GetByRole(AriaRole.Radio, new() { Name = "National Minimum Wage for" }).CheckAsync();

    private async Task EnterSetAsCompetitive() => await page.GetByRole(AriaRole.Radio, new() { Name = "Set as competitive" }).CheckAsync();


    private async Task EnterFixedWageType()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "Set wage yourself" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();

        await page.Locator("#FixedWageYearlyAmount").FillAsync(RAADataHelper.FixedWageYearlyAmount);
    }

    public async Task<CompetitiveWagePage> SubmitYes()
    {
        await SelectYesIfSalaryIsAboveNationalMinWage();

        return await VerifyPageAsync(() => new CompetitiveWagePage(context));
    }

    private async Task SelectYesIfSalaryIsAboveNationalMinWage()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "Yes" }).CheckAsync();
    }
}


public class ExtraInformationAboutPayPage(ScenarioContext context) : RaaBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Extra information about pay");
    }

    public async Task TriggerValidationErrorOnPayInformation()
    {
        await IFrameFillAsync("WageAdditionalInformation_ifr", RAADataHelper.RandomAlphabeticString(510));
        await IFrameFillAsync("CompanyBenefitsInformation_ifr", RAADataHelper.RandomAlphabeticString(510));
        await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();
        await CheckValidationErrors("Information about pay must be 500 characters or less", "Company benefits must be 500 characters or less");
    }
    public async Task<SubmitNoOfPositionsPage> SubmitExtraInformationAboutPay()
    {
        await IFrameFillAsync("WageAdditionalInformation_ifr", rAADataHelper.OptionalMessage);

        await IFrameFillAsync("CompanyBenefitsInformation_ifr", rAADataHelper.OptionalMessage);

        await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();

        return await VerifyPageAsync(() => new SubmitNoOfPositionsPage(context));
    }
}

public class SubmitNoOfPositionsPage(ScenarioContext context) : RaaBasePage(context)
{
    public override async Task VerifyPage()
    {
        string PageTitle = (isRaaEpc || isRaaTransfer) ? "How many positions are available?" : isRaaEmployer ? "How many positions are there for this apprenticeship?" : "How many positions are available?";

        await Assertions.Expect(page.Locator("label")).ToContainTextAsync(PageTitle);
    }

    public async Task TriggerValidationErrorOnNumberOfPositions()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();
        await CheckValidationErrors("Enter the number of positions for this apprenticeship");
    }
    public async Task<ChooseApprenticeshipLocationPage> SubmitNoOfPositionsAndNavigateToChooseLocationPage()
    {
        await EnterNumberOfPositionsAndContinue();

        return await VerifyPageAsync(() => new ChooseApprenticeshipLocationPage(context));
    }

    public async Task<SelectOrganisationPage> SubmitNoOfPositionsAndNavigateToSelectOrganisationPage()
    {
        await EnterNumberOfPositionsAndContinue();

        return await VerifyPageAsync(() => new SelectOrganisationPage(context));
    }

    public async Task EnterNumberOfPositionsAndContinue()
    {
        string labelText = (isRaaEpc || isRaaTransfer) ? "How many positions are available?" :
            isRaaEmployer ? "How many positions are there for this apprenticeship?" : "How many positions are available?";

        await page.GetByRole(AriaRole.Spinbutton, new() { Name = labelText }).FillAsync(RAADataHelper.NumberOfVacancy);

        await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();
    }
}

public class CompetitiveWagePage(ScenarioContext context) : RaaBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Set as competitive?");
    }
}