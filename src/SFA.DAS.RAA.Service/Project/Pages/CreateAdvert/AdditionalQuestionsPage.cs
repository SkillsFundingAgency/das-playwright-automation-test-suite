namespace SFA.DAS.RAA.Service.Project.Pages.CreateAdvert;

public class AdditionalQuestionsPage(ScenarioContext context) : RaaBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Application questions on Find an apprenticeship");
    }

    private readonly List<string> MandatoryQuestions =
    [
        "What are your skills and strengths?",
        "What interests you about this apprenticeship?"
    ];

    private readonly List<string> FoundationsMandatoryQuestions =
    [
        "What interests you about this apprenticeship?"
    ];

    public async Task<CreateAnApprenticeshipAdvertOrVacancyPage> CompleteAllAdditionalQuestionsForApplicants(bool isFoundationAdvert, bool enterQuestion1, bool enterQuestion2)
    {
        await CheckMandatoryQuestions(isFoundationAdvert);

        await EnterAdditionalQuestions(enterQuestion1, enterQuestion2);

        await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();

        return await VerifyPageAsync(() => new CreateAnApprenticeshipAdvertOrVacancyPage(context));
    }

    public async Task<CheckYourAnswersPage> UpdateAllAdditionalQuestionsAndGoToCheckYourAnswersPage(bool enterQuestion1, bool enterQuestion2)
    {
        await EnterAdditionalQuestions(enterQuestion1, enterQuestion2);

        await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();

        return await VerifyPageAsync(() => new CheckYourAnswersPage(context));
    }

    private async Task EnterAdditionalQuestions(bool enterQuestion1, bool enterQuestion2)
    {
        if (enterQuestion1)
        {
            await page.Locator("[id='AdditionalQuestion1']").FillAsync(advertDataHelper.AdditionalQuestion1);
        }

        if (enterQuestion2)
        {
            await page.Locator("[id='AdditionalQuestion2']").FillAsync(advertDataHelper.AdditionalQuestion2);
        }
    }

    private async Task CheckMandatoryQuestions(bool isFoundationAdvert)
    {
        var questionsToCheck = isFoundationAdvert ? FoundationsMandatoryQuestions : MandatoryQuestions;

        foreach (var question in questionsToCheck)
        {
            await Assertions.Expect(page.Locator("#main-content")).ToContainTextAsync(question);
        }
    }
}