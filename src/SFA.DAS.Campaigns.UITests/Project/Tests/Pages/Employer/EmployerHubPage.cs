namespace SFA.DAS.Campaigns.UITests.Project.Tests.Pages.Employer;

public class EmployerHubPage(ScenarioContext context) : EmployerBasePage(context)
{
    public override async Task VerifyPage() =>
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Hire an apprentice");

    public async Task VerifySubHeadings() => await VerifyLinks();

    public async Task<ExploreFundingOptionsPage> NavigateToExploreFundingOptions()
    {
        await NavigateToEmployerCard("Explore funding options");
        return await VerifyPageAsync(() => new ExploreFundingOptionsPage(context));
    }

    public async Task<SignUpPage> NavigateToSignUpPage()
    {
        await NavigateToEmployerCard("Get tailored advice on hiring an apprentice");
        return await VerifyPageAsync(() => new SignUpPage(context));
    }

    public override async Task<IPage> NavigateToEmployerCard(string cardName)
    {
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