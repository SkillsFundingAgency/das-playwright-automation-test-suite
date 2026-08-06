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

        string PageTitle = "Give feedback to the unsuccessful applicant";
        await Assertions.Expect(page.Locator("h1").First).ToContainTextAsync(PageTitle);
    }

    public async Task<ApplicationUnsuccessfulPage> NotifyApplicant()
    {

        if (!isRaaEpc)
        {
            await page.Locator("#CandidateFeedback").FillAsync(rAADataHelper.OptionalMessage);
            //await page.GetByRole(AriaRole.Button, new() { Name = "Confirm" }).ClickAsync();
        }

        //await page.GetByRole(AriaRole.Radio, new() { Name = "Yes" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Submit" }).ClickAsync();

        return await VerifyPageAsync(() => new ApplicationUnsuccessfulPage(context));
    }

    public async Task<ApplicationOutcomeArchivePage> NotifyApplicantAndArchive()
    {
        if (!isRaaEpc)
        {
            await page.Locator("#CandidateFeedback").FillAsync(rAADataHelper.OptionalMessage);
            //await page.GetByRole(AriaRole.Button, new() { Name = "Confirm" }).ClickAsync();
        }

        //await page.GetByRole(AriaRole.Radio, new() { Name = "Yes, make this application" }).CheckAsync();

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


public class ApplicationSuccessfulPage(ScenarioContext context) : RaaBasePage(context)
{
    public override async Task VerifyPage()
    {
        bool isEmpPage = page.Url.Contains("eas.apprenticeships");
        string PageTitle = isEmpPage
            ? $"application has been marked as successful"
            : $"Application made successful";

        await Assertions.Expect(page.Locator("h3")).ToContainTextAsync(PageTitle);
    }
}

public class ApplicationUnsuccessfulPage(ScenarioContext context) : RaaBasePage(context)
{
    public override async Task VerifyPage()
    {
        string PageTitle = "Feedback sent to applicant";

        await Assertions.Expect(page.Locator("h3")).ToContainTextAsync(PageTitle);
    }
}
