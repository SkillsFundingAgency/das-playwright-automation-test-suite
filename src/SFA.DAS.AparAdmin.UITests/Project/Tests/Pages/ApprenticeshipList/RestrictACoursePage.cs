using System;

namespace SFA.DAS.AparAdmin.UITests.Project.Tests.Pages.ApprenticeshipList;

public class RestrictACoursesPage(ScenarioContext context) : AparAdminBasePage(context)
{
    public override async Task VerifyPage()
    {
       await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Search for the course you want to restrict");
    }

    public async Task SelectCourse(string courseName)
    {
        await page.Locator("#SelectedLarsCode").FillAsync(courseName);
        bool valid = await page.Locator("#SelectedLarsCode__option--0").IsVisibleAsync();
        if(valid)
        {
            await page.Locator("#SelectedLarsCode__option--0").ClickAsync();

        }
        await page.Locator("#continue").ClickAsync();
    }

    public async Task ConfirmRestriction()
    {
        await page.GetByRole(AriaRole.Button, new(){Name = "Confirm restriction",Exact = true}).ClickAsync();
    }
}