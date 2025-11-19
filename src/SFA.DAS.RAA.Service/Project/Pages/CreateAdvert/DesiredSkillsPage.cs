namespace SFA.DAS.RAA.Service.Project.Pages.CreateAdvert;

public class DesiredSkillsPage(ScenarioContext context) : RaaBasePage(context)
{

    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("What skills and personal qualities do applicants need to have?");
    }

    public async Task<QualificationsPage> SelectSkillAndGoToQualificationsPage()
    {
        await SelectSkillsAndContinue();

        return await VerifyPageAsync(() => new QualificationsPage(context));
    }

    public async Task<FutureProspectsPage> SelectSkillsAndGoToFutureProspectsPage()
    {
        await SelectSkillsAndContinue();

        return await VerifyPageAsync(() => new FutureProspectsPage(context));
    }

    private async Task SelectSkillsAndContinue()
    {
        var options = RandomDataGenerator.GetRandom(await page.Locator("label.govuk-checkboxes__label").AllAsync());

        await options.ClickAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();
    }
}

public class QualificationsPage(ScenarioContext context) : RaaBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Qualifications");
    }

    public async Task<AddAQualificationPage> SelectYesToAddQualification()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "Yes" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();

        return await VerifyPageAsync(() => new AddAQualificationPage(context));
    }
}

public class AddAQualificationPage(ScenarioContext context) : RaaBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Add a qualification");
    }

    public async Task<ConfirmQualificationsPage> EnterQualifications()
    {
        await page.GetByRole(AriaRole.Combobox).SelectOptionAsync(new[] { "A Level" });

        await page.GetByRole(AriaRole.Textbox, new() { Name = "Subject" }).FillAsync(rAADataHelper.DesiredQualificationsSubject);
        
        await page.GetByRole(AriaRole.Textbox, new() { Name = "Grade" }).FillAsync(RAADataHelper.DesiredQualificationsGrade);

        await SelectRandomRadioOption();

        await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();

        return await VerifyPageAsync(() => new ConfirmQualificationsPage(context));
    }
}

public class ConfirmQualificationsPage(ScenarioContext context) : RaaBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Qualifications");
    }

    public async Task<FutureProspectsPage> ConfirmQualificationsAndGoToFutureProspectsPage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Save and continue" }).ClickAsync();

        return await VerifyPageAsync(() => new FutureProspectsPage(context));
    }
}

public class FutureProspectsPage(ScenarioContext context) : RaaBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("label")).ToContainTextAsync("What is the expected career progression after this apprenticeship?");
    }

    public async Task<ThingsToConsiderPage> EnterFutureProspect()
    {
        await page.Locator("iframe[title=\"Rich Text Area. Press ALT-F9 for menu. Press ALT-F10 for toolbar. Press ALT-0 for help\"]").ContentFrame.Locator("#tinymce").FillAsync(rAADataHelper.VacancyOutcome);

        await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();

        return await VerifyPageAsync(() => new ThingsToConsiderPage(context));
    }
}
public class ThingsToConsiderPage(ScenarioContext context) : RaaBasePage(context)
{
    public override async Task VerifyPage()
    {
        string PageTitle = isRaaEmployer ? "Other requirements" : "Other requirements";

        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync(PageTitle);
    }

    public async Task<PreviewYourAdvertOrVacancyPage> EnterThingsToConsider()
    {
        await page.GetByRole(AriaRole.Textbox, new() { Name = "What other requirements are" }).FillAsync(rAADataHelper.OptionalMessage);

        await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();

        return await VerifyPageAsync(() => new PreviewYourAdvertOrVacancyPage(context));
    }

    public async Task<CreateAnApprenticeshipAdvertOrVacancyPage> EnterThingsToConsiderAndReturnToCreateAdvert(bool optionalFields)
    {
        if (optionalFields) await page.GetByRole(AriaRole.Textbox, new() { Name = "What other requirements are" }).FillAsync(rAADataHelper.OptionalMessage);

        await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();

        return await VerifyPageAsync(() => new CreateAnApprenticeshipAdvertOrVacancyPage(context));
    }
}

