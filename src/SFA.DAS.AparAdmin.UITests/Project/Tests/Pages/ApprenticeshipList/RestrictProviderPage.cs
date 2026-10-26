using System;

namespace SFA.DAS.AparAdmin.UITests.Project.Tests.Pages.ApprenticeshipList;

public class RestrictProviderPage(ScenarioContext context) : AparAdminBasePage(context)
{
    public override async Task VerifyPage()
    {
       await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Change how we manage this provider's delivery");
    }

    public async Task NavigateToConfirmControlProviderDelivery()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Control provider delivery", Exact = true}).ClickAsync();
    }

}