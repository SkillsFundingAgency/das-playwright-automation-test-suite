using SFA.DAS.Login.Service.Project;
using SFA.DAS.Login.Service.Project.Helpers;

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
        String buttonText = context.ScenarioInfo.Tags.Contains("raaemployer")
            ? "Continue" : "Confirm";

        await page.GetByRole(AriaRole.Radio, new() { Name = "Yes, make this application" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = buttonText }).ClickAsync();

        return await VerifyPageAsync(() => new ApplicationSuccessfulPage(context));
    }
}


public class ConfirmApplicantUnsuccessfulPage(ScenarioContext context) : RaaBasePage(context)
{
    public override async Task VerifyPage()
    {
        var faaUser = context.GetUser<FAAApplyUser>();
        string faauserFullName = $"{faaUser.FirstName} {faaUser.LastName}";

        string PageTitle = context.ScenarioInfo.Tags.Contains("raaemployer")
            ? "Are you sure you want to tell this applicant that they have not been accepted?"
            : $"Are you sure you want to make {faauserFullName}'s application unsuccessful?";
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync(PageTitle);
    }

    public async Task<ApplicationUnsuccessfulPage> NotifyApplicant()
    {
        string radioButtonText = context.ScenarioInfo.Tags.Contains("raaemployer")
            ? "Yes, notify the applicant" : "Yes, make this application unsuccessful and notify the applicant";

        string buttonText = context.ScenarioInfo.Tags.Contains("raaemployer")
            ? "Continue" : "Confirm";

        await page.GetByRole(AriaRole.Radio, new() { Name = radioButtonText }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = buttonText }).ClickAsync();

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
        string PageTitle = context.ScenarioInfo.Tags.Contains("raaemployer")
            ? $"application has been marked as {message}"
            :$"Application made {message}";
        
        await Assertions.Expect(page.Locator("h3")).ToContainTextAsync(PageTitle);
    }
}