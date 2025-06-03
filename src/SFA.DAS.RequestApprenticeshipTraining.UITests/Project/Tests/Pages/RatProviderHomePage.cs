namespace SFA.DAS.RequestApprenticeshipTraining.UITests.Project.Tests.Pages;

public class RatProviderHomePage(ScenarioContext context) : ProviderHomePage(context)
{
    public async Task<EmployerRequestsForApprenticeshipTrainingPage> NavigateToEmployerRequestPage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "View employer requests for" }).ClickAsync();

        return await VerifyPageAsync(() => new EmployerRequestsForApprenticeshipTrainingPage(context));
    }
}

public class EmployerRequestsForApprenticeshipTrainingPage(ScenarioContext context) : RatProjectBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Employer requests for apprenticeship training");

    public async Task<SelectRequestToSharePage> SelectStandard()
    {
        string trainingRequestName = objectContext.GetTrainingCourseName();

        objectContext.SetDebugInformation($"Finding request for - {trainingRequestName}");

        await page.GetByRole(AriaRole.Listitem).Filter(new() { HasText = trainingRequestName }).GetByRole(AriaRole.Link, new() { Name = "View requests" }).ClickAsync();

        return await VerifyPageAsync(() => new SelectRequestToSharePage(context));
    }
}

public class SelectRequestToSharePage(ScenarioContext context) : RatProjectBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Select requests to share your information");

    public async Task<SelectProviderEmailToSharePage> SelectRequest()
    {
        var locator = $"input.govuk-checkboxes__input[value='{ratDataHelper.RequestId}'][type='checkbox']";

        await page.Locator(locator).ClickAsync();

        //await page.GetByRole(AriaRole.Checkbox, new() { Name = "Request date 12 May 2025 New" }).UncheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new SelectProviderEmailToSharePage(context));
    }
}

public class SelectProviderEmailToSharePage(ScenarioContext context) : RatProjectBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Select the email address to share");

    public async Task<SelectProviderPhoneToSharePage> SelectEmail()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = ratDataHelper.ProviderEmail }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new SelectProviderPhoneToSharePage(context));
    }
}

public class SelectProviderPhoneToSharePage(ScenarioContext context) : RatProjectBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Select the telephone number to share");

    public async Task<ProCheckYourAnswersPage> SelectPhoneNumber()
    {
        var phonenumbers = await page.GetByRole(AriaRole.Radio).AllAsync();

        var randomNumber = RandomDataGenerator.GetRandomElementFromListOfElements(phonenumbers.ToList());

        await randomNumber.ClickAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new ProCheckYourAnswersPage(context));
    }
}

public class ProCheckYourAnswersPage(ScenarioContext context) : RatProjectBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Check your answers");

    public async Task<WeHaveSharedYourDetailsPage> SubmitAnswers()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Submit" }).ClickAsync();

        return await VerifyPageAsync(() => new WeHaveSharedYourDetailsPage(context));
    }
}

public class WeHaveSharedYourDetailsPage(ScenarioContext context) : RatProjectBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("We've shared your details");

}