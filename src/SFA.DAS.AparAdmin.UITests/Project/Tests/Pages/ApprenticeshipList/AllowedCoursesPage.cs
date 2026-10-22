using System;

namespace SFA.DAS.AparAdmin.UITests.Project.Tests.Pages.ApprenticeshipList;

public class AllowedCoursesPage(ScenarioContext context) : AparAdminBasePage(context)
{
    public override async Task VerifyPage()
    {
       await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Manage apprenticeships this provider is allowed to deliver");
    }

    public async Task SuccessBannerVisible()
    {
        await Assertions.Expect(page.Locator("#govuk-notification-banner-title")).ToContainTextAsync("Success");
    }

    public async Task ClickAddAnAppreticeship()
    {
        await page.GetByRole(AriaRole.Link, new() {Name = "Add an apprenticeship"}).ClickAsync();
    }

    public async Task GoBackToProviderDetailsPage()
    {
        await page.Locator(".govuk-back-link").ClickAsync();
    }
}