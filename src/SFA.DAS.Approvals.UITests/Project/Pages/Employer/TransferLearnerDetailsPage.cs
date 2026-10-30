using SFA.DAS.Approvals.UITests.Project.Helpers.DataHelpers.ApprenticeshipModel;

namespace SFA.DAS.Approvals.UITests.Project.Pages.Employer;

internal class TransferLearnerDetailsPage(ScenarioContext context) : ApprovalsBasePage(context)
{
    public override async Task VerifyPage() =>
        await Assertions.Expect(page.GetByRole(AriaRole.Heading, new() { Level = 1 })).ToContainTextAsync("Add learner details");

    internal async Task<EmployerApproveLearnerDetailsPage> Submit(Apprenticeship apprenticeship)
    {
        var apprentice = apprenticeship.ApprenticeDetails;
        var training = apprenticeship.TrainingDetails;
        await page.Locator("[name=FirstName]").FillAsync(apprentice.FirstName);
        await page.Locator("[name=LastName]").FillAsync(apprentice.LastName);
        await page.Locator("[name=Email]").FillAsync(apprentice.Email);
        await page.Locator("[name=BirthDay]").FillAsync(apprentice.DateOfBirth.Day.ToString());
        await page.Locator("[name=BirthMonth]").FillAsync(apprentice.DateOfBirth.Month.ToString());
        await page.Locator("[name=BirthYear]").FillAsync(apprentice.DateOfBirth.Year.ToString());
        await page.Locator("[name=StartMonth]").FillAsync(training.StartDate.Month.ToString());
        await page.Locator("[name=StartYear]").FillAsync(training.StartDate.Year.ToString());
        await page.Locator("[name=EndMonth]").FillAsync(training.EndDate.Month.ToString());
        await page.Locator("[name=EndYear]").FillAsync(training.EndDate.Year.ToString());
        await page.Locator("[name=Cost]").FillAsync(training.TotalPrice.ToString());
        await page.Locator("#Reference").FillAsync(training.ConsumerReference);
        await page.Locator("#main-content .govuk-button").ClickAsync();
        return await VerifyPageAsync(() => new EmployerApproveLearnerDetailsPage(context));
    }
}

internal class TransferLearnerCoursePage(ScenarioContext context) : ApprovalsBasePage(context)
{
    public override async Task VerifyPage() =>
        await Assertions.Expect(page.GetByRole(AriaRole.Heading, new() { Level = 1 })).ToContainTextAsync("What is the training course?");

    internal async Task<TransferLearnerDetailsPage> SelectCourse(string title)
    {
        await page.Locator("#CourseCode").FillAsync(title);
        await page.GetByRole(AriaRole.Option).Filter(new() { HasText = title }).First.ClickAsync();
        await page.Locator("#main-content .govuk-button").ClickAsync();
        return await VerifyPageAsync(() => new TransferLearnerDetailsPage(context));
    }
}

internal class ChooseTransferFundsPage(ScenarioContext context) : ApprovalsBasePage(context)
{
    public override async Task VerifyPage() =>
        await Assertions.Expect(page.GetByRole(AriaRole.Heading, new() { Level = 1 })).ToContainTextAsync("Choose transfer funds");

    internal async Task<ChooseYourMainTrainingProviderPage> SelectTransfer(string applicationId, string senderAccountId)
    {
        await page.Locator($"[id='LevyTransferConnection-{applicationId}|{senderAccountId}']").CheckAsync();
        await page.Locator("#submit-levytransfer-connection").ClickAsync();
        return await VerifyPageAsync(() => new ChooseYourMainTrainingProviderPage(context));
    }
}
