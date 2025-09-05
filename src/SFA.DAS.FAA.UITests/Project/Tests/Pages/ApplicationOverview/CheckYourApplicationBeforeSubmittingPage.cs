using SFA.DAS.FAA.UITests.Project.Tests.Pages;

namespace SFA.DAS.FAA.UITests.Project.Tests.Pages.ApplicationOverview;

public class CheckYourApplicationBeforeSubmittingPage(ScenarioContext context) : FAABasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Check your application before submitting");

    public async Task<ApplicationSubmittedPage> SubmitApplication()
    {
        await page.GetByRole(AriaRole.Checkbox, new() { Name = "I understand that I won't be" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Submit" }).ClickAsync();

        return await VerifyPageAsync(() => new ApplicationSubmittedPage(context));
    }
}

