using System;

namespace SFA.DAS.AparAdmin.UITests.Project.Tests.Pages.ApprenticeshipList;

public class ConfirmRestrictProviderPage(ScenarioContext context) : AparAdminBasePage(context)
{
    public override async Task VerifyPage()
    {
       await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Confirm that you want to manage what apprenticeships this provider can deliver");
    }

    public async Task ConfirmProviderRestricted()
    {
        await page.GetByRole(AriaRole.Button, new() {Name = "Control provider delivery", Exact = true}).ClickAsync();
    }
}