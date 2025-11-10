using SFA.DAS.AparAdmin.UITests.Project.Tests.Pages.SearchAndUpdate;
using System;

public class StatusChangePage(ScenarioContext context) : BasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1"))
            .ToContainTextAsync("Update the status for this provider");
    }

    public async Task<BasePage> ChangeOrganisationStatus(string status)
    {
        var normalized = (status ?? string.Empty).Trim().ToLowerInvariant();

        string value = normalized switch
        {
            "active" => "1",
            "active but not taking on apprentices" or "active not taking on apprentices" => "2",
            "on-boarding" or "onboarding" => "3",
            "removed" => "0",
            _ => throw new ArgumentException($"Invalid status: {status}")
        };

        var radio = page.Locator($"input[name='OrganisationStatus'][value='{value}']");
        await radio.CheckAsync();

        var continueButton = page.GetByRole(AriaRole.Button, new() { Name = "Continue" });
        await continueButton.ClickAsync();

        if (value == "0")
            return await VerifyPageAsync(() => new RemovedReasonsPage(context));

        return await VerifyPageAsync(() => new SuccessPage(context));
    }

    public async Task<ProviderDetailsPage> NoDoNotChangeStatus()
    {
        await page.GetByLabel("No").CheckAsync();
        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();
        return await VerifyPageAsync(() => new ProviderDetailsPage(context));
    }
}
