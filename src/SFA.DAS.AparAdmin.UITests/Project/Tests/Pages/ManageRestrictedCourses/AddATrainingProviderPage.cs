using System;

namespace SFA.DAS.AparAdmin.UITests.Project.Tests.Pages.ManageRestrictedCourses;

public class AddATrainingProviderPage(ScenarioContext context) : BasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Confirm that you want to allow this provider to offer this course    ");
    }

    public async Task ConfirmAddATrainingProvider()
    {
        await page.Locator("button:text('Confirm')").ClickAsync();
    }

    public async Task CancelAddATrainingProvider()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Cancel"}).ClickAsync();
    }

    public async Task ErrorMessage()
    {
        await Assertions.Expect(page.Locator(".govuk-error-summary__title")).ToBeVisibleAsync();
    }

    public async Task ProviderToRestrict(string UKPRN)
    {
        await page.Locator("#SelectedUkprn").FillAsync(UKPRN);
        bool valid = await page.Locator("#SelectedUkprn__option--0").IsVisibleAsync();
        if(valid)
        {
            await page.Locator("#SelectedUkprn__option--0").ClickAsync();
        }
        await page.Locator("#continue").ClickAsync();
    }


}