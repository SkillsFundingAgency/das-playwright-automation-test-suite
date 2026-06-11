using SFA.DAS.Login.Service.Project;
using SFA.DAS.Login.Service.Project.Helpers;

namespace SFA.DAS.RAA.Service.Project.Pages;

public class ConfirmApplicantSucessfulPage(ScenarioContext context) : RaaBasePage(context)
{
    public override async Task VerifyPage()
    {
        string PageTitle = $"Do you want to make this application successful?";

        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync(PageTitle);
    }

    public async Task<ApplicationSuccessfulPage> NotifyApplicant()
    {

        await page.GetByRole(AriaRole.Radio, new() { Name = "Yes, make this application" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Submit" }).ClickAsync();

        return await VerifyPageAsync(() => new ApplicationSuccessfulPage(context));
    }

    public async Task<ApplicationOutcomeArchivePage> NotifyApplicantAndArchive()
    {

        await page.GetByRole(AriaRole.Radio, new() { Name = "Yes, make this application" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Submit" }).ClickAsync();

        return await VerifyPageAsync(() => new ApplicationOutcomeArchivePage(context));
    }
}


public class ConfirmApplicantUnsuccessfulPage(ScenarioContext context) : RaaBasePage(context)
{
    public override async Task VerifyPage()
    {
        var faaUser = context.GetUser<FAAApplyUser>();
        string faauserFullName = $"{faaUser.FirstName} {faaUser.LastName}";

        string PageTitle = "Do you want to make this application unsuccessful?";
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync(PageTitle);
    }

    public async Task<ApplicationUnsuccessfulPage> NotifyApplicant()
    {

        await page.GetByRole(AriaRole.Radio, new() { Name = "Yes, make this application" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Submit" }).ClickAsync();

        return await VerifyPageAsync(() => new ApplicationUnsuccessfulPage(context));
    }

    public async Task<ApplicationOutcomeArchivePage> NotifyApplicantAndArchive()
    {

        await page.GetByRole(AriaRole.Radio, new() { Name = "Yes, make this application" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Submit" }).ClickAsync();

        return await VerifyPageAsync(() => new ApplicationOutcomeArchivePage(context));
    }
}

public class ApplicationOutcomeArchivePage(ScenarioContext context) : RaaBasePage(context)
{
    public override async Task VerifyPage()
    {
        string text = isRaaEmployer ? "advert" : "vacancy";
        string PageTitle = $"All applicants have been notified of their outcomes. You can now archive this {text}.";
        await Assertions.Expect(page.Locator(".govuk-notification-banner__heading")).ToContainTextAsync(PageTitle);
    }

    public async Task<ArchiveConfirmationPage> ArchiveAdvert()
    {
        string radioOptionText = isRaaEmployer ? "Yes, archive this advert now" : "Yes, archive this vacancy";
        await page.GetByRole(AriaRole.Radio, new() { Name = radioOptionText }).CheckAsync();
        await page.GetByRole(AriaRole.Button, new() { Name = "Submit" }).ClickAsync();
        return await VerifyPageAsync(() => new ArchiveConfirmationPage(context));
    }
}

public class ArchiveConfirmationPage(ScenarioContext context) : RaaBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator(".govuk-notification-banner__heading")).ToContainTextAsync("has been archived");
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
        string PageTitle = context.ScenarioInfo.Tags.Contains("raaemployer")
            ? $"application has been marked as {message}"
            :$"Application made {message}";
        
        await Assertions.Expect(page.Locator("h3")).ToContainTextAsync(PageTitle);
    }
}