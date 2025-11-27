using SFA.DAS.RAA.DataGenerator.Project;
using SFA.DAS.RAA.Service.Project.Helpers;

namespace SFA.DAS.RAA.Service.Project.Pages;

public abstract class VerifyDetailsBasePage(ScenarioContext context) : RaaBasePage(context)
{
    //protected virtual By EmployerName { get; }

    //protected virtual By EmployerNameInAboutTheEmployerSection { get; }

    //protected virtual By DisabilityConfident { get; }

    //protected void VerifyEmployerName()
    //{
    //    var empName = objectContext.GetEmployerNameAsShownInTheAdvert();
    //    VerifyElement(EmployerName, empName);
    //    VerifyElement(EmployerNameInAboutTheEmployerSection, empName);
    //}

    //protected void VerifyDisabilityConfident() => VerifyElement(DisabilityConfident);

    //public void RAAQASignOut() => formCompletionHelper.ClickElement(By.CssSelector("#navigation a[data-automation='sign-out']"));
}

public class ViewVacancyPage(ScenarioContext context) : VerifyDetailsBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("#previewTitle")).ToContainTextAsync(vacancyTitleDataHelper.VacancyTitle);
    }

    //protected override By EmployerName => By.CssSelector(".govuk-caption-xl");

    //protected override By EmployerNameInAboutTheEmployerSection => By.CssSelector("div.govuk-grid-column-two-thirds > p:nth-child(4)");

    public async Task VerifyWageType(string wageType) => await VerifyWageAmount(wageType);

    public async Task VerifyEmployerWageType(string wageType) => await VerifyWageAmount(wageType);

    private static string GetWageAmount(string wageType)
    {
        return wageType switch
        {
            RAAConst.NationalMinWages => RAADataHelper.NationalMinimumWage,
            RAAConst.FixedWageType => RAADataHelper.FixedWageForApprentices,
            RAAConst.SetAsCompetitive => RAADataHelper.SetAsCompetitive,
            _ => RAADataHelper.NationalMinimumWageForApprentices,
        };
    }

    private async Task VerifyWageAmount(string wageType) => await Assertions.Expect(page.Locator("dl")).ToContainTextAsync(GetWageAmount(wageType));

    public async Task VerifyDisabilityConfident()
    {
        await Assertions.Expect(page.GetByRole(AriaRole.Img, new() { Name = "Disability Confident" })).ToBeVisibleAsync();
    }
}
