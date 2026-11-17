using System;

namespace SFA.DAS.AparAdmin.UITests.Project.Tests.Pages.ApprenticeshipList;

public class RemoveLastStartDatePage(ScenarioContext context) : AparAdminEditBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Confirm that you want to remove the last date for new starts");
    }
    
    public async Task ConfirmRemoveDate()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Remove last start date" }).ClickAsync();
    }
}