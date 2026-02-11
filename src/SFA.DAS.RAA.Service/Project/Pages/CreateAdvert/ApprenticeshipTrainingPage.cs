namespace SFA.DAS.RAA.Service.Project.Pages.CreateAdvert;

public class ApprenticeshipTrainingPage(ScenarioContext context) : RaaBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("What training course will the apprentice take?");
    }

    public async Task<ConfirmApprenticeshipTrainingPage> EnterTrainingTitle()
    {
        var trainingTitle = IsFoundationAdvert ? RAADataHelper.FoundationTrainingTitle : RAADataHelper.TrainingTitle;

        await EnterTrainingTitleAction(trainingTitle);

        return await VerifyPageAsync(() => new ConfirmApprenticeshipTrainingPage(context));
    }

    private async Task EnterTrainingTitleAction(string trainingTitle)
    {
        await page.GetByRole(AriaRole.Combobox, new() { Name = "Enter apprenticeship training" }).FillAsync(trainingTitle);

        await page.GetByRole(AriaRole.Option, new() { Name = trainingTitle }).ClickAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();
    }
}

public class HaveYouAlreadyFoundTrainingPage(ScenarioContext context) : RaaBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Have you already found apprenticeship training?");
    }

    public async Task<ApprenticeshipTrainingPage> SelectYes()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "Yes" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();

        return await VerifyPageAsync(() => new ApprenticeshipTrainingPage(context));
    }
}
