using System.Linq;
using Microsoft.Playwright;
using SFA.DAS.Campaigns.UITests.Project.Tests.Pages.Home;

namespace SFA.DAS.Campaigns.UITests.Project.Tests.Pages.Employer;

public class SignUpPage(ScenarioContext context) : CampaignsVerifyLinks(context)
{
    public override async Task VerifyPage() =>
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Get emails about taking on your first apprentice");

    public async Task YourDetails()
    {
        await page.GetByLabel("First name").FillAsync(campaignsDataHelper.Firstname);
        await page.GetByLabel("Last name").FillAsync(campaignsDataHelper.Lastname);
        await page.GetByLabel("Email address").FillAsync(campaignsDataHelper.Email);
    }

    public async Task SelectCompanySize(string companySize)
    {
        var locator = companySize.ToLower() switch
        {
            var s when s.Contains("10") && !s.Contains("between") => page.Locator("#SizeOfYourCompany"),
            var s when s.Contains("10 and 49") => page.Locator("#between10and49employees"),
            var s when s.Contains("50 and 249") => page.Locator("#between50and249employees"),
            var s when s.Contains("250") => page.Locator("#over250employees"),
            _ => page.GetByLabel(companySize, new() { Exact = false })
        };

        await locator.CheckAsync();
    }

    public async Task SelectCompanySizeOption1() => await page.Locator("#SizeOfYourCompany").CheckAsync();
    public async Task SelectCompanySizeOption2() => await page.Locator("#between10and49employees").CheckAsync();
    public async Task SelectCompanySizeOption3() => await page.Locator("#between50and249employees").CheckAsync();
    public async Task SelectCompanySizeOption4() => await page.Locator("#over250employees").CheckAsync();

    public async Task<ThanksForSubscribingPage> RegisterInterest()
    {
        var industryAllOptions = await page.GetByLabel("Industry").Locator("option").AllTextContentsAsync();
        var industryoption = GetRandomOption(industryAllOptions.Where(x => x != "Choose your industry").ToList());
        await page.GetByLabel("Industry").SelectOptionAsync([industryoption]);

        var regionAllOptions = await page.GetByLabel("Region").Locator("option").AllTextContentsAsync();
        var regionoption = GetRandomOption(regionAllOptions.Where(x => x != "Choose your location").ToList());
        await page.GetByLabel("Region").SelectOptionAsync([regionoption]);

        await page.Locator("#IncludeInUR").CheckAsync();

        // Click and wait for the URL to change to the thank you page
        await Task.WhenAll(
            page.WaitForURLAsync("**/thank-you-for-signing-up**"),
            page.GetByRole(AriaRole.Button, new() { Name = "Sign up" }).ClickAsync()
        );

        return await VerifyPageAsync(() => new ThanksForSubscribingPage(context));
    }

    private static string GetRandomOption(List<string> options) => RandomDataGenerator.GetRandomElementFromListOfElements(options);
}