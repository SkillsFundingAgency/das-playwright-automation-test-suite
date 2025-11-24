using System;

namespace SFA.DAS.AparAdmin.UITests.Project.Tests.Pages.SearchAndUpdate;

public class SuccessPage(ScenarioContext context) : BasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1"))
            .ToContainTextAsync("");
    }

    public async Task VerifyOrganisationStatus(string organisationName, string expectedStatus)
    {
        var title = page.Locator("h1.govuk-panel__title");
        string actualText = await title.InnerTextAsync();

        await Assertions.Expect(title).ToContainTextAsync(organisationName, new() { IgnoreCase = true });
        await Assertions.Expect(title).ToContainTextAsync(expectedStatus, new() { IgnoreCase = true });

        Console.WriteLine($"✅ Verified: '{actualText}' contains '{organisationName}' and '{expectedStatus}'.");
    }

    public async Task<ProviderDetailsPage> GoBackToProviderDetailsPage()
    {
        var editLink = page.GetByRole(AriaRole.Link, new() { Name = "edit their details" });

        await Assertions.Expect(editLink).ToBeVisibleAsync(new() { Timeout = 10000 });
        await editLink.ClickAsync();

        return await VerifyPageAsync(() => new ProviderDetailsPage(context));
    }

}
