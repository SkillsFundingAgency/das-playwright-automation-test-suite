using System;

namespace SFA.DAS.AparAdmin.UITests.Project.Tests.Pages.ApprenticeshipList;

public class ChangeRestrictionPage(ScenarioContext context) : AparAdminEditBasePage(context)
{
    public override async Task VerifyPage()
    {
       await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("What would you like to change?");
    }

    public async Task Confirm()
    {
        await page.GetByRole(AriaRole.Button,
            new()
            {
                Name = "Continue",
                Exact = true
            })
           .ClickAsync();
    }

    public async Task SelectOptionAddEditRemove(string editType)
    {
        string radioId = editType switch
        {
            "Edits" => "selected-option-add-or-change",
            "Removes"  => "selected-option-remove",
            _        => throw new System.ArgumentException($"Unknown edit type: {editType}")
        };
        await page.Locator($"#{radioId}").ClickAsync();
    }
}