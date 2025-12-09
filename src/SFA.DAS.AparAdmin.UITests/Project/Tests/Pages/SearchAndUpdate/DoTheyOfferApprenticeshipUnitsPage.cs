using SFA.DAS.AparAdmin.UITests.Project.Tests.Pages.SearchAndUpdate;
using System;

public class DoTheyOfferApprenticeshipUnitsPage(ScenarioContext context) : BasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1"))
            .ToContainTextAsync("Do they offer apprenticeship units?");
    }
    public async Task<ProviderDetailsPage> YesOfferApprenticeshipsUnits()
    {
        await page.GetByLabel("Yes").CheckAsync();
        await page.GetByRole(AriaRole.Button, new() { Name = "Confirm" }).ClickAsync();
        return await VerifyPageAsync(() => new ProviderDetailsPage(context));
    }
    public async Task<ProviderDetailsPage> NoDoNotOfferApprenticeshipsUnits()
    {
        await page.GetByLabel("No").CheckAsync();
        await page.GetByRole(AriaRole.Button, new() { Name = "Confirm" }).ClickAsync();
        return await VerifyPageAsync(() => new ProviderDetailsPage(context));
    }
    public async Task TrainingProviderMustOfferEitherApprenticeshipsOrApprenticeshipUnits()
    {
        await page.GetByLabel("No").CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Confirm" }).ClickAsync();

        await VerifyApprenticeshipUnitsErrorMessage();
    }
    public async Task VerifyApprenticeshipUnitsErrorMessage()
    {
        var errorSummary = page.Locator("div.govuk-error-summary");
        await Assertions.Expect(errorSummary).ToBeVisibleAsync();
        var errorMessage = await errorSummary.Locator("ul.govuk-error-summary__list li a").InnerTextAsync();

        const string expectedMessage = "Training providers must offer either apprenticeships or apprenticeship units";

        if (!errorMessage.Trim().Contains(expectedMessage, StringComparison.OrdinalIgnoreCase))
        {
            throw new Exception($"Apprenticeship units error verification failed. Expected: '{expectedMessage}', Actual: '{errorMessage}'");
        }
    }
}
