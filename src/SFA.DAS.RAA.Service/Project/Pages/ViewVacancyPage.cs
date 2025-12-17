using SFA.DAS.RAA.DataGenerator.Project;
using SFA.DAS.RAA.Service.Project.Helpers;

namespace SFA.DAS.RAA.Service.Project.Pages;

public abstract class VerifyDetailsBasePage(ScenarioContext context) : RaaBasePage(context)
{
    //protected virtual By EmployerName { get; }

    //protected virtual By EmployerNameInAboutTheEmployerSection { get; }

    //protected virtual By DisabilityConfident { get; }

    protected async Task VerifyEmployerName()
    {
        var empName = objectContext.GetEmployerNameAsShownInTheAdvert();

        await Assertions.Expect(page.Locator("form")).ToContainTextAsync(empName);

        await Assertions.Expect(page.Locator("#EmployerName")).ToContainTextAsync(empName);

    }

    public async Task VerifyDisabilityConfident() => await Assertions.Expect(page.GetByRole(AriaRole.Img, new() { Name = "A logo confirming that the" })).ToBeVisibleAsync();

    public async Task RAAQASignOut()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Sign out" }).ClickAsync();

        var signOutFrame = page.GetByRole(AriaRole.Heading);

        if (await signOutFrame.IsVisibleAsync())
        {
            var text = await signOutFrame.TextContentAsync();

            if (text.ContainsCompareCaseInsensitive("Pick an account"))
            {
                await page.Locator("[aria-label*='Sign out']").ClickAsync();
            }
        }
    }
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

    public async new Task VerifyDisabilityConfident()
    {
        await Assertions.Expect(page.GetByRole(AriaRole.Img, new() { Name = "Disability Confident" })).ToBeVisibleAsync();
    }
}
