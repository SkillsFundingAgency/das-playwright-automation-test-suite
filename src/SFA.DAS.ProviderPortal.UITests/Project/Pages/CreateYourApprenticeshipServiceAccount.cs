using SFA.DAS.EmployerPortal.UITests.Project.Pages;

namespace SFA.DAS.ProviderPortal.UITests.Project.Pages;

public class CreateYourApprenticeshipServiceAccount(ScenarioContext context) : EmployerPortalBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Create your apprenticeship service account");
    }

    public async Task<ChangeEmployerName> ChangeName()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Change name" }).ClickAsync();

        return await VerifyPageAsync(() => new ChangeEmployerName(context));
    }

    public async Task<ReadTheEmployerAgreementPage> ReadAgreement(string orgName)
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "employer agreement between" }).ClickAsync();

        return await VerifyPageAsync(() => new ReadTheEmployerAgreementPage(context, orgName));
    }

    public async Task<ApprenticeshipServiceAccountCreatedPage> CreateAccount()
    {
        await page.GetByRole(AriaRole.Checkbox, new() { Name = "By accepting, you confirm" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Create account" }).ClickAsync();

        return await VerifyPageAsync(() => new ApprenticeshipServiceAccountCreatedPage(context));
    }

    public async Task<DoNotCreateAccountPage> DoNotCreateAccount()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Do not create an account" }).ClickAsync();

        return await VerifyPageAsync(() => new DoNotCreateAccountPage(context));
    }
}

public class ApprenticeshipServiceAccountCreatedPage(ScenarioContext context) : EmpAccountCreationBase(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Apprenticeship service account created and training provider permissions set");

        await SetEasNewUser();
    }

    public new async Task<HomePage> GoToHomePage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Go to Home" }).ClickAsync();

        return new HomePage(context);
    }
}

public class DoNotCreateAccountPage(ScenarioContext context) : EmployerPortalBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Are you sure you do not want to create an account?");
    }

    public async Task<ApprenticeshipServiceAccountNotCreatedPage> ConfirmDoNotCreateAccount()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Do not create account" }).ClickAsync();

        return await VerifyPageAsync(() => new ApprenticeshipServiceAccountNotCreatedPage(context));
    }
}

public class ApprenticeshipServiceAccountNotCreatedPage(ScenarioContext context) : EmployerPortalBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Apprenticeship service account not created");
    }
}


public class ChangeEmployerName(ScenarioContext context) : EmployerPortalBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Change your name");
    }

    public async Task<CreateYourApprenticeshipServiceAccount> ChangeName(string fName, string lName)
    {
        await page.Locator("#EmployerContactFirstName").FillAsync($"Updated {fName}");

        await page.Locator("#EmployerContactLastName").FillAsync($"Updated {lName}");

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new CreateYourApprenticeshipServiceAccount(context));
    }
}

public class ReadTheEmployerAgreementPage(ScenarioContext context, string orgName) : EmployerPortalBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Read the employer agreement");

        await Assertions.Expect(page.Locator("#main-content")).ToContainTextAsync($"You must read the agreement between {orgName.ToUpperInvariant()} and the Department for Education (DfE)");
    }

    public async Task<CreateYourApprenticeshipServiceAccount> ReturnToCreateYourApprenticeshipServiceAccount()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Previous   page   :  Return" }).First.ClickAsync();

        return await VerifyPageAsync(() => new CreateYourApprenticeshipServiceAccount(context));
    }
}
