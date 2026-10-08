using Microsoft.Playwright;
using SFA.DAS.Campaigns.UITests.Project.Tests.Pages.Home;

namespace SFA.DAS.Campaigns.UITests.Project.Tests.Pages.Employer;

public class EmployerHubPage(ScenarioContext context) : EmployerBasePage(context)
{
    public override async Task VerifyPage() =>
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Hire an apprentice");

    public async Task<ExploreFundingOptionsPage> NavigateToExploreFundingOptions()
    {
        var cardLink = page.Locator(".fiu-stepper__link, .fiu-cta-panel")
                           .Filter(new() { HasText = "Explore funding options" })
                           .First;

        await cardLink.ScrollIntoViewIfNeededAsync();
        await cardLink.ClickAsync();
        await page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);

        var pageInstance = new ExploreFundingOptionsPage(context);
        await pageInstance.VerifyPage();
        return pageInstance;
    }

    public async Task<SignUpPage> NavigateToSignUpPage()
    {
        var signUpLink = page.Locator(".fiu-stepper__link, .fiu-cta-panel")
                             .Filter(new() { HasText = "Get tailored advice on hiring an apprentice" })
                             .First;

        await signUpLink.ScrollIntoViewIfNeededAsync();
        await signUpLink.ClickAsync();
        await page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);

        var pageInstance = new SignUpPage(context);
        await pageInstance.VerifyPage();
        return pageInstance;
    }

    public override async Task<IPage> NavigateToEmployerCard(string cardName)
    {
        if (cardName.Equals("Explore funding options", StringComparison.OrdinalIgnoreCase) ||
            cardName.Equals("Funding calculator", StringComparison.OrdinalIgnoreCase) ||
            cardName.Equals("Estimate funding", StringComparison.OrdinalIgnoreCase))
        {
            await NavigateToExploreFundingOptions();
            return page;
        }

        var cardLink = page.Locator(".fiu-stepper__link, .fiu-cta-panel, .fiu-card")
                           .Filter(new() { HasText = cardName })
                           .First;

        await Assertions.Expect(cardLink).ToBeVisibleAsync();
        await cardLink.ScrollIntoViewIfNeededAsync();
        await cardLink.ClickAsync();

        await page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
        return page;
    }
}