using Azure;
using SFA.DAS.RAA.DataGenerator.Project;

namespace SFA.DAS.FAA.UITests.Project.Tests.Pages;

public class FAA_ApplicationsPage(ScenarioContext context) : FAABasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Your applications");

    public async Task<FAA_SuccessfulApplicationPage> OpenSuccessfulApplicationPage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Successful", Exact = true }).ClickAsync();

        return await VerifyPageAsync(() => new FAA_SuccessfulApplicationPage(context));
    }

    public async Task<FAA_UnSuccessfulApplicationPage> OpenUnSuccessfulApplicationPage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Unsuccessful" }).ClickAsync();

        return await VerifyPageAsync(() => new FAA_UnSuccessfulApplicationPage(context));
    }

    public async Task<FAA_SubmittedApplicationPage> OpenSubmittedlApplicationPage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Submitted" }).ClickAsync();

        return await VerifyPageAsync(() => new FAA_SubmittedApplicationPage(context));
    }

    public async Task ViewApplication()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = $"View application   for {vacancyTitleDataHelper.VacancyTitle}" }).ClickAsync();

        if (IsFoundationAdvert)
        {
            await CheckFoundationTag();
        }
    }
}

public class FAA_SuccessfulApplicationPage(ScenarioContext context) : FAA_ApplicationsPage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("#main-content")).ToContainTextAsync("Successful");
}

public class FAA_UnSuccessfulApplicationPage(ScenarioContext context) : FAA_ApplicationsPage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("#main-content")).ToContainTextAsync("Unsuccessful");
}

public class FAA_SubmittedApplicationPage(ScenarioContext context) : FAA_ApplicationsPage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("#main-content")).ToContainTextAsync("Submitted");

    private static string GetWithdrawApplicationLink(string vacancyRef) => $"[id='VAC{vacancyRef}-withdraw']";

    private static string GetWithdrawnVacancyTitleLocator(string vacancyRef) => $"a[href*='/apprenticeship/VAC{vacancyRef}?source=applications&tab=Submitted']";

    private static string FirstWithdrawLink => "[id^='VAC'][id$='-withdraw']";

    public async Task<FAA_SubmittedApplicationPage> WithdrawSelectedApplication()
    {
        var vacancyRef = objectContext.GetVacancyReference();

        var vacancyTitle = await GetWithdrawnVacancyTitle(vacancyRef);

        objectContext.Set("vacancytitle", vacancyTitle);

        await ClickToWithdrawApplication(vacancyRef);

        await PerformVacancyWithdrawalActions();

        await AssertApplicationWithdrawnMessage(vacancyTitle);

        return await VerifyPageAsync(() => new FAA_SubmittedApplicationPage(context));
    }

    public async Task<FAA_SubmittedApplicationPage> WithdrawRandomlySelectedApplication()
    {
        await page.Locator(FirstWithdrawLink).First.ClickAsync();

        await PerformVacancyWithdrawalActions();

        return new FAA_SubmittedApplicationPage(context);
    }

    private async Task<string> GetWithdrawnVacancyTitle(string vacancyRef)
    {
        var vacancyTitleLocator = GetWithdrawnVacancyTitleLocator(vacancyRef);

        return await page.Locator(vacancyTitleLocator).TextContentAsync();
    }

    private async Task ClickToWithdrawApplication(string vacancyRef)
    {
        var withdrawApplicationLink = GetWithdrawApplicationLink(vacancyRef);

        await page.Locator(withdrawApplicationLink).ClickAsync();

    }

    private async Task PerformVacancyWithdrawalActions()
    {
        if (IsFoundationAdvert)
        {
            await CheckFoundationTag();
        }

        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Withdraw your application");

        await page.GetByRole(AriaRole.Radio, new() { Name = "Yes" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Confirm" }).ClickAsync();
    }

    private async Task AssertApplicationWithdrawnMessage(string vacancyTitle)
    {
        var organisationName = objectContext.GetEmployerName();

        var expectedMessage = $"Application withdrawn for {vacancyTitle} at {organisationName}.";

        await Assertions.Expect(page.Locator("#govuk-notification-banner-title")).ToContainTextAsync("Success");

        await Assertions.Expect(page.GetByLabel("Success")).ToContainTextAsync(expectedMessage);
    }
}