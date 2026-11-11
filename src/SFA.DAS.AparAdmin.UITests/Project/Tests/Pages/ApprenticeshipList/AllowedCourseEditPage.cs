using System;

namespace SFA.DAS.AparAdmin.UITests.Project.Tests.Pages.ApprenticeshipList;

public class AllowedCourseEditPage(ScenarioContext context) : AparAdminEditBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("What would you like to change?");
    }

    public async Task ContinueToEditDate()
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