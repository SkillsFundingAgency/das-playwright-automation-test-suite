using System;

namespace SFA.DAS.AparAdmin.UITests.Project.Tests.Pages.ApprenticeshipList;

public class ChangeRestrictionPage(ScenarioContext context) : AparAdminBasePage(context)
{
    public override async Task VerifyPage()
    {
       await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("What would you like to change?");
    }

    public async Task SelectOption(string editType)
    {
        string radioId = editType switch
        {
            "Edits" => "selected-option-add-or-change",
            "Removes"  => "selected-option-remove",
             _        => throw new ArgumentException($"Unknown message type: {editType}")
        };
        await  page.Locator($"#{radioId}").ClickAsync();
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
}