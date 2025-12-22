namespace SFA.DAS.RAA.Service.Project.Pages;

public class ConfirmApplicantSucessfulPage(ScenarioContext context) : RaaBasePage(context)
{
    public override async Task VerifyPage()
    {
        string PageTitle = $"Are you sure you want to make {rAADataHelper.CandidateFullName}'s application successful?";

        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync(PageTitle);
    }

    public async Task<ApplicationSuccessfulPage> NotifyApplicant()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "Yes, make this application" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new ApplicationSuccessfulPage(context));
    }
}


public class ConfirmApplicantUnsuccessfulPage(ScenarioContext context) : RaaBasePage(context)
{
    public override async Task VerifyPage()
    {
        string PageTitle = $"Are you sure you want to tell this applicant that they have not been accepted?";

        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync(PageTitle);
    }

    public async Task<ApplicationUnsuccessfulPage> NotifyApplicant()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "Yes, notify the applicant" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new ApplicationUnsuccessfulPage(context));
    }
}


public class ApplicationSuccessfulPage(ScenarioContext context) : ApplicationOutcomeBasePage(context, "successful")
{
}

public class ApplicationUnsuccessfulPage(ScenarioContext context) : ApplicationOutcomeBasePage(context, "unsuccessful")
{
}

public abstract class ApplicationOutcomeBasePage(ScenarioContext context, string message) : RaaBasePage(context)
{
    public override async Task VerifyPage()
    {
        string PageTitle = $"application has been marked as {message}";

        await Assertions.Expect(page.Locator("h3")).ToContainTextAsync(PageTitle);
    }
}