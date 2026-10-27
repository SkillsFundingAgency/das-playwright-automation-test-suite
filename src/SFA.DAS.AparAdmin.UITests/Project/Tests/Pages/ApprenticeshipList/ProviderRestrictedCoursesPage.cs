using System;

namespace SFA.DAS.AparAdmin.UITests.Project.Tests.Pages.ApprenticeshipList;

public class ProviderRestrictedCoursesPage(ScenarioContext context) : AparAdminBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Manage the apprenticeships this provider cannot deliver");
    }

    public async Task ClickRestrictACourse()
    {
        await page.GetByRole(AriaRole.Link, new() {Name = "Restrict a course", Exact = true}).ClickAsync();
    }

    public async Task ClickChangeCourseDateDetails(string larsCode)
    {
        await page.Locator($"[href*='{larsCode}']").ClickAsync();
    }

    public async Task ChangeProviderDelivery()
    {
        await page.GetByRole(AriaRole.Link, new() {Name = "Change how we manage this provider", Exact = true}).ClickAsync();
    }

}
