using System.Linq;

namespace SFA.DAS.Campaigns.UITests.Project.Tests.Pages.Employer;

public class SignUpPage(ScenarioContext context) : CampaingnsVerifyLinks(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Get emails about taking on your first apprentice");

    public async Task YourDetails()
    {
        await page.GetByLabel("First name").FillAsync(campaignsDataHelper.Firstname);

        await page.GetByLabel("Last name").FillAsync(campaignsDataHelper.Lastname);

        await page.GetByLabel("Email address").FillAsync(campaignsDataHelper.Email);
    }

    public async Task SelectCompanySizeOption1() => await page.GetByLabel("Less than 10 employees").CheckAsync();
    public async Task SelectCompanySizeOption2() => await page.Locator("#between10and49employees").CheckAsync();
    public async Task SelectCompanySizeOption3() => await page.Locator("#between50and249employees").CheckAsync();
    public async Task SelectCompanySizeOption4() => await page.Locator("#over250employees").CheckAsync();

    public async Task<ThanksForSubscribingPage> RegisterInterest()
    {
        var industryAllOptions = await page.GetByLabel("Industry").Locator("option").AllTextContentsAsync();

        var industryoption = GetRandomOption(industryAllOptions.ToList().Where(x => x != "Choose your industry").ToList());

        await page.GetByLabel("Industry").SelectOptionAsync([industryoption]);

        var regionAllOptions = await page.GetByLabel("Region").Locator("option").AllTextContentsAsync();

        var regionoption = GetRandomOption(regionAllOptions.ToList().Where(x => x != "Choose your location").ToList());

        await page.GetByLabel("Region").SelectOptionAsync([regionoption]);

        await page.GetByLabel("I'am happy to be take part in").CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Sign up" }).ClickAsync();

        return await VerifyPageAsync(() => new ThanksForSubscribingPage(context));
    }

    private static string GetRandomOption(List<string> options) => RandomDataGenerator.GetRandomElementFromListOfElements(options);
}
