using System;

namespace SFA.DAS.AparAdmin.UITests.Project.Tests.Pages.ApprenticeshipList;

public class RemoveRestrictionPage(ScenarioContext context) : AparAdminBasePage(context)
{
    public override async Task VerifyPage()
    {
       await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Confirm that you want to remove the last date for new starts");
    }

    public async Task RemoveRestriction()
    {
        await page.GetByRole(AriaRole.Button, new() {Name = "Remove restriction", Exact = true}).ClickAsync();
    }
}