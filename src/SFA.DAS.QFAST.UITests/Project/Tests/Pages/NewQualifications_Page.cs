

using SFA.DAS.QFAST.UITests.Project.Helpers;
using System.Configuration;
using System.Text.RegularExpressions;

namespace SFA.DAS.QFAST.UITests.Project.Tests.Pages;

public class NewQualifications_Page(ScenarioContext context) : BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.GetByRole(AriaRole.Heading, new() { Name = "New Qualifications" })).ToBeVisibleAsync();
    protected readonly QfastDataHelpers qfastDataHelpers = context.Get<QfastDataHelpers>();
    public async Task<NewQualifications_Page> VerifyQANnumberIsLink()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Search" }).ClickAsync();
        var firstLink = page.Locator("table.govuk-table tbody tr").Nth(0).Locator("td").Nth(0).Locator("a");
        if (!await firstLink.IsVisibleAsync())
        {
            throw new Exception("The first qualification is not a link.");
        }
        var Qan = await firstLink.GetAttributeAsync("href");
        if (string.IsNullOrWhiteSpace(Qan))
        {
            throw new Exception("The first qualification link has no valid href.");
        }
        return await VerifyPageAsync(() => new NewQualifications_Page(context));
    }
    public async Task VerifyClickingOnLinkOpensNewTab()
    {
        var firstQanLink = page.Locator("table.govuk-table tbody tr").Nth(0).Locator("td").Nth(0).Locator("a");        
        var popupTask = page.WaitForPopupAsync();    
        await firstQanLink.ClickAsync();
        var popupPage = await popupTask;
        var URL = new Regex(@"^https://find-a-qualification.services.ofqual.gov.uk/qualifications/\d+$");
        await Assertions.Expect(popupPage).ToHaveURLAsync(URL);
    }
}

