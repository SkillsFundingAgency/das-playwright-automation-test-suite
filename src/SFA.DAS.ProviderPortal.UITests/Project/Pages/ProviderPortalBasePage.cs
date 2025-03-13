using Polly;
using SFA.DAS.EmployerPortal.UITests.Project.Pages;
using SFA.DAS.ProviderPortal.UITests.Project.Helpers;
using System.Text.RegularExpressions;

namespace SFA.DAS.ProviderPortal.UITests.Project.Pages;

public abstract class ProviderPortalBasePage(ScenarioContext context) : EmployerPortalBasePage(context)
{
    protected readonly EprDataHelper eprDataHelper = context.Get<EprDataHelper>();
}

public class AddAnEmployerPage(ScenarioContext context) : ProviderPortalBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Add an employer");
    }

    public async Task<SearchEmployerEmailPage> StartNowToAddAnEmployer()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Start now" }).ClickAsync();

        return await VerifyPageAsync(() => new SearchEmployerEmailPage(context));
    }

    public async Task<CheckEmployerDetailsPage> SubmitEmployerName()
    {
        await EnterEmployerName();

        return await VerifyPageAsync(() => new CheckEmployerDetailsPage(context));
    }

    public async Task<CheckEmployerDetailsPage> ChangeEmployerName()
    {
        await EnterEmployerName();

        return await VerifyPageAsync(() => new CheckEmployerDetailsPage(context));
    }

    private async Task EnterEmployerName()
    {
        await page.Locator("input[name=\"firstName\"]").FillAsync(eprDataHelper.EmployerFirstName);

        await page.Locator("input[name=\"lastName\"]").FillAsync(eprDataHelper.EmployerLastName);

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();
    }
}


public class CheckEmployerDetailsPage(ScenarioContext context) : ProviderPortalBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Check details");
    }

    public async Task<RequestSentToEmployerPage> SendInvitation()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Send invitation" }).ClickAsync();

        return await VerifyPageAsync(() => new RequestSentToEmployerPage(context));
    }

    public async Task<AddAnEmployerPage> AccessChangeEmployerName()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Change employer name" }).ClickAsync();

        return await VerifyPageAsync(() => new AddAnEmployerPage(context));
    }

    public async Task<AccAccountRequestPermissionsPage> AccessChangePermissions()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Change permissions" }).ClickAsync();

        return await VerifyPageAsync(() => new AccAccountRequestPermissionsPage(context));
    }
}


public class SearchEmployerEmailPage(ScenarioContext context) : ProviderPortalBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Search employer email");
    }

    public async Task<EmailAccountFoundPage> EnterEmployerEmail()
    {
        var email = await EnterEmail();

        return await VerifyPageAsync(() => new EmailAccountFoundPage(context, email));
    }

    public async Task<EmailAccountNotFoundPage> EnterNewEmployerEmail()
    {
        var email = await EnterEmail();

        return await VerifyPageAsync(() => new EmailAccountNotFoundPage(context, email));
    }

    public async Task<ContactEmployerShutterPage> EnterEmployerEmailAndGoToContactEmployer()
    {
        await EnterEmail();

        return await VerifyPageAsync(() => new ContactEmployerShutterPage(context));
    }

    public async Task<InviteSentShutterPage> EnterEmployerEmailAndGoToInviteSent()
    {
        await EnterEmail();

        return await VerifyPageAsync(() => new InviteSentShutterPage(context, false));
    }

    private async Task<string> EnterEmail()
    {
        var email = eprDataHelper.EmployerEmail;

        await page.GetByRole(AriaRole.Textbox, new() { Name = "Employer email address" }).FillAsync(email);

        await page.GetByRole(AriaRole.Button, new() { Name = "Search" }).ClickAsync();

        return email;
    }
}

public class InviteSentShutterPage(ScenarioContext context, bool verifyPayeandAorn) : ProviderPortalBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Invitation sent");

        await Assertions.Expect(page.Locator("#main-content")).ToContainTextAsync("Your organisation has already invited this employer to create an account or add you as a training provider.");

        if (verifyPayeandAorn)
        {
            await Assertions.Expect(page.Locator("#main-content .govuk-inset-text")).ToContainTextAsync(eprDataHelper.EmployerPaye);

            await Assertions.Expect(page.Locator("#main-content .govuk-inset-text")).ToContainTextAsync(eprDataHelper.EmployerAorn);
        }
    }

    public async Task<EmployerAccountDetailsPage> GoToEmpAccountDetails()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Employer account details for" }).ClickAsync();

        return await VerifyPageAsync(() => new EmployerAccountDetailsPage(context));
    }
}

public class EmailAccountNotFoundPage(ScenarioContext context, string email) : ProviderPortalBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Search employer details");

        await Assertions.Expect(page.Locator("#main-content")).ToContainTextAsync($"We cannot find an account linked to {email}");
    }

    public async Task<AddAnEmployerPage> SubmitPayeAndContinueToInvite()
    {
        await EnterPayeAndContinue();

        return await VerifyPageAsync(() => new AddAnEmployerPage(context));
    }

    public async Task<InviteSentShutterPage> SubmitPayeAndContinueToInviteSent()
    {
        await EnterPayeAndContinue();

        return await VerifyPageAsync(() => new InviteSentShutterPage(context, true));
    }

    private async Task EnterPayeAndContinue()
    {
        await page.GetByRole(AriaRole.Textbox, new() { Name = "Employer PAYE reference" }).FillAsync(eprDataHelper.EmployerPaye);

        await page.GetByRole(AriaRole.Textbox, new() { Name = "Accounts Office reference" }).FillAsync(eprDataHelper.EmployerAorn);

        await page.GetByRole(AriaRole.Button, new() { Name = "Search" }).ClickAsync();
    }
}





public class ContactEmployerShutterPage(ScenarioContext context) : ProviderPortalBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Contact the employer");

        await Assertions.Expect(page.Locator("#main-content")).ToContainTextAsync("You cannot add this employer because there are multiple organisations linked to their details.");
    }
}


public class ViewEmpAndManagePermissionsPage(ScenarioContext context) : ProviderPortalBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("View employers and manage permissions");
    }

    public async Task<AddAnEmployerPage> ClickAddAnEmployer()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Add an employer" }).ClickAsync();

        return await VerifyPageAsync(() => new AddAnEmployerPage(context));
    }

    public async Task<EmployerAccountDetailsPage> ViewEmployer()
    {
        await page.GetByRole(AriaRole.Textbox, new() { Name = "Search by employer name or" }).FillAsync(eprDataHelper.EmployerOrganisationName);

        await page.GetByRole(AriaRole.Button, new() { Name = "Apply filters" }).ClickAsync();

        await page.Locator($"a[href*='{eprDataHelper.AgreementId}']").ClickAsync();

        return await VerifyPageAsync(() => new EmployerAccountDetailsPage(context));
    }

    public async Task VerifyPendingRequest()
    {
        await page.GetByRole(AriaRole.Checkbox, new() { Name = "Request pending" }).CheckAsync();

        await page.GetByRole(AriaRole.Textbox, new() { Name = "Search by employer name or" }).FillAsync(eprDataHelper.EmployerOrganisationName);

        await page.GetByRole(AriaRole.Button, new() { Name = "Apply filters" }).ClickAsync();

        await Assertions.Expect(page.Locator("tbody")).ToContainTextAsync($"Request pending {eprDataHelper.EmployerOrganisationName.ToUpper()}");

    }
}

public class EmployerAccountDetailsPage(ScenarioContext context) : ProviderPortalBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Employer account details");
    }

    public async Task<RequestPermissionsPage> ChangePermissions()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Request permissions" }).ClickAsync();

        return await VerifyPageAsync(() => new RequestPermissionsPage(context));
    }

    public async Task<ViewEmpAndManagePermissionsPage> ViewEmployersAndManagePermissionsPage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "View employers and manage permissions" }).ClickAsync();

        return await VerifyPageAsync(() => new ViewEmpAndManagePermissionsPage(context));
    }
}

public class RequestPermissionsPage(ScenarioContext context) : PermissionBasePageForTrainingProviderPage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Request permissions");
    }

    public async Task<RequestSentToEmployerPage> ProviderRequestPermissions((AddApprenticePermissions cohortpermission, RecruitApprenticePermissions recruitpermission) permisssion)
    {
        await SetAddApprentice(permisssion.cohortpermission);

        await SetRecruitApprentice(permisssion.recruitpermission);

        return await VerifyPageAsync(() => new RequestSentToEmployerPage(context));
    }
}

public class AccAccountRequestPermissionsPage(ScenarioContext context) : PermissionBasePageForTrainingProviderPage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Request permissions");
    }

    public async Task<CheckEmployerDetailsPage> ProviderRequestNewPermissions((AddApprenticePermissions cohortpermission, RecruitApprenticePermissions recruitpermission) permisssion)
    {
        await SetAddApprentice(permisssion.cohortpermission);

        await SetRecruitApprentice(permisssion.recruitpermission);

        return await VerifyPageAsync(() => new CheckEmployerDetailsPage(context));

    }
}

public class RequestSentToEmployerPage(ScenarioContext context) : ProviderPortalBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("#main-content")).ToContainTextAsync("Request sent to employer");
    }

    public async Task<ViewEmpAndManagePermissionsPage> GoToViewEmployersPage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "View employers and manage" }).ClickAsync();

        return await VerifyPageAsync(() => new ViewEmpAndManagePermissionsPage(context));
    }

    public async Task<ViewEmpAndManagePermissionsPage> GoToViewCurrentEmployersPage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "View current employers and" }).ClickAsync();

        return await VerifyPageAsync(() => new ViewEmpAndManagePermissionsPage(context));

    }
}

public class EmailAccountFoundPage(ScenarioContext context, string email) : ProviderPortalBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Account found");

        await Assertions.Expect(page.GetByRole(AriaRole.Strong)).ToContainTextAsync(email);
    }

    public async Task VerifyAlreadyLinkedToThisEmployer()
    {
        await Assertions.Expect(page.Locator("#main-content")).ToContainTextAsync($"We’ve found an apprenticeship service account linked to {email}. Your organisation is already linked to this employer.");
    }

    public async Task<AddEmployerAndRequestPermissionsPage> ContinueToInvite()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new AddEmployerAndRequestPermissionsPage(context));
    }
}

public class AddEmployerAndRequestPermissionsPage(ScenarioContext context) : PermissionBasePageForTrainingProviderPage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Add employer and request permissions");
    }

    public async Task<RequestSentToEmployerPage> ProviderRequestPermissions((AddApprenticePermissions cohortpermission, RecruitApprenticePermissions recruitpermission) permisssion)
    {
        await SetAddApprentice(permisssion.cohortpermission);

        await SetRecruitApprentice(permisssion.recruitpermission);

        return await VerifyPageAsync(() => new RequestSentToEmployerPage(context));
    }

    protected new async Task SetAddApprentice(AddApprenticePermissions permission)
    {
        if (permission == AddApprenticePermissions.YesAddApprenticeRecords)
        {
            await page.GetByRole(AriaRole.Radio, new() { Name = "Yes, employer will have final" }).CheckAsync();
        }

        if (permission == AddApprenticePermissions.NoToAddApprenticeRecords)
        {
            await page.Locator("#addRecords-2").CheckAsync();
        }
    }

    protected new async Task SetRecruitApprentice(RecruitApprenticePermissions permission)
    {
        if (permission == RecruitApprenticePermissions.YesRecruitApprentices)
        {
            await page.GetByRole(AriaRole.Radio, new() { Name = "Yes", Exact = true }).CheckAsync();
        }

        if (permission == RecruitApprenticePermissions.YesRecruitApprenticesButEmployerWillReview)
        {
            await page.GetByRole(AriaRole.Radio, new() { Name = "Yes, employer will review" }).CheckAsync();
        }

        if (permission == RecruitApprenticePermissions.NoToRecruitApprentices)
        {
            await page.Locator("#recruitApprentices-3").CheckAsync();
        }

        await page.GetByRole(AriaRole.Button, new() { Name= "Confirm" }).ClickAsync();
    }
}

