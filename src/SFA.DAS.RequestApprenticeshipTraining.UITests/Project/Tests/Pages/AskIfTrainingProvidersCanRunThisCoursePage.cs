

namespace SFA.DAS.RequestApprenticeshipTraining.UITests.Project.Tests.Pages;

public abstract class RatProjectBasePage(ScenarioContext context) : BasePage(context)
{
    protected readonly RatDataHelper ratDataHelper = context.GetValue<RatDataHelper>();
}

public class RatEmployerHomePage(ScenarioContext context) : HomePage(context)
{
    public async Task<FindApprenticeshipTrainingAndManageRequestsPage> GoToRatHomePage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Find apprenticeship training" }).ClickAsync();

        return await VerifyPageAsync(() => new FindApprenticeshipTrainingAndManageRequestsPage(context));
    }
}

public class AskIfTrainingProvidersCanRunThisCoursePage(ScenarioContext context) : RatProjectBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Ask if training providers can run this course");

    public async Task<HowManyAprenticesWouldDoThisApprenticeshipTrainingPage> ClickStarNow()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Start now" }).ClickAsync();

        return await VerifyPageAsync(() => new HowManyAprenticesWouldDoThisApprenticeshipTrainingPage(context));
    }
}

public class HowManyAprenticesWouldDoThisApprenticeshipTrainingPage(ScenarioContext context) : RatProjectBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("label")).ToContainTextAsync("How many apprentices would do this apprenticeship training?");

    public async Task<AreTheApprenticeshipsInTheSameLocationPage> EnterMoreThan1Apprentices()
    {
        await EnterApprentices(RandomDataGenerator.GenerateRandomNumberBetweenTwoValues(2, 4));

        return await VerifyPageAsync(() => new AreTheApprenticeshipsInTheSameLocationPage(context));

    }

    public async Task<WhereIsTheApprenticeshipLocationPage> Enter1Apprentices()
    {
        await EnterApprentices(1);

        return await VerifyPageAsync(() => new WhereIsTheApprenticeshipLocationPage(context, true));
    }

    private async Task EnterApprentices(int noOfApprentice)
    {
        await page.GetByRole(AriaRole.Textbox, new() { Name = "How many apprentices would do" }).FillAsync($"{noOfApprentice}");

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();
    }
}

public class AreTheApprenticeshipsInTheSameLocationPage(ScenarioContext context) : RatProjectBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Are the apprenticeships in the same location?");

    public async Task<WhereIsTheApprenticeshipLocationPage> ClickYesToChooseSingleLocation() => await GoToLocationPage(true);

    public async Task<WhereIsTheApprenticeshipLocationPage> ClickNoToChooseMultipleLocation() => await GoToLocationPage(false);

    private async Task<WhereIsTheApprenticeshipLocationPage> GoToLocationPage(bool IsSingleLocation)
    {
        if (IsSingleLocation) await page.GetByRole(AriaRole.Radio, new() { Name = "Yes" }).CheckAsync();
        else await page.GetByRole(AriaRole.Radio, new() { Name = "No" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new WhereIsTheApprenticeshipLocationPage(context, IsSingleLocation));
    }
}

public class WhereIsTheApprenticeshipLocationPage(ScenarioContext context, bool IsSingleLocation) : RatProjectBasePage(context)
{
    public override async Task VerifyPage()
    {
        if (IsSingleLocation) await Assertions.Expect(page.Locator("label")).ToContainTextAsync("Where is the apprenticeship location?");
        else await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Where is the apprenticeship location?");
    }

    public async Task<SelectTrainingOptionsPage> GoToTrainingOptionsPage(bool enterLocation)
    {
        if (enterLocation)
        {
            var location = RandomDataGenerator.RandomTown();

            await page.GetByRole(AriaRole.Combobox, new() { Name = "Where is the apprenticeship" }).FillAsync(location);

            await page.GetByRole(AriaRole.Option, new() { Name = location, Exact = false }).First.ClickAsync();
        }

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new SelectTrainingOptionsPage(context));
    }

    public async Task<SelectTrainingOptionsPage> ChooseRegion()
    {
        var regions = await page.Locator(".govuk-checkboxes__label").AllTextContentsAsync();

        var listOfRegions = regions.ToList();

        var firstRegion = RandomDataGenerator.GetRandomElementFromListOfElements(listOfRegions);

        listOfRegions.Remove(firstRegion);

        var secondRegion = RandomDataGenerator.GetRandomElementFromListOfElements(listOfRegions);

        await page.GetByRole(AriaRole.Checkbox, new() { Name = firstRegion, Exact = true }).CheckAsync();

        await page.GetByRole(AriaRole.Checkbox, new() { Name = secondRegion, Exact = true }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new SelectTrainingOptionsPage(context));
    }
}

public class SelectTrainingOptionsPage(ScenarioContext context) : RatProjectBasePage(context)
{

    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Select training options");

    public async Task<EmpCheckYourAnswersPage> SelectTrainingOptions()
    {
        var selector = RandomDataGenerator.GetRandomElementFromListOfElements(["At apprentice's workplace", "Day release", "Block release"]);

        await page.GetByRole(AriaRole.Checkbox, new() { Name = selector }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new EmpCheckYourAnswersPage(context));
    }
}


public class EmpCheckYourAnswersPage(ScenarioContext context) : RatProjectBasePage(context)
{

    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Check your answers");

    public async Task<WeHaveSharedThisWithTrainingProvidersPage> SubmitAnswers()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Submit" }).ClickAsync();

        return await VerifyPageAsync(() => new WeHaveSharedThisWithTrainingProvidersPage(context));
    }

    public async Task<HowManyAprenticesWouldDoThisApprenticeshipTrainingPage> ChangeHowManyApprentices()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Change   how many apprentices" }).ClickAsync();

        return await VerifyPageAsync(() => new HowManyAprenticesWouldDoThisApprenticeshipTrainingPage(context));

    }

    public async Task<AreTheApprenticeshipsInTheSameLocationPage> ChangeOneApprenticeshipLocation()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Change   one apprenticeship" }).ClickAsync();

        return await VerifyPageAsync(() => new AreTheApprenticeshipsInTheSameLocationPage(context));

    }

    public async Task<WhereIsTheApprenticeshipLocationPage> ChangeApprenticeshipLocations()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Change   apprenticeship" }).ClickAsync();

        return await VerifyPageAsync(() => new WhereIsTheApprenticeshipLocationPage(context, true));
    }

    public async Task<SelectTrainingOptionsPage> ChangeTrainingOptions()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Change   training options" }).ClickAsync();

        return await VerifyPageAsync(() => new SelectTrainingOptionsPage(context));

    }
}

public class WeHaveSharedThisWithTrainingProvidersPage(ScenarioContext context) : RatProjectBasePage(context)
{

    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("We've shared this with training providers");

    public async Task<FindApprenticeshipTrainingAndManageRequestsPage> ReturnToRequestPage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Manage your training requests" }).ClickAsync();

        return await VerifyPageAsync(() => new FindApprenticeshipTrainingAndManageRequestsPage(context));
    }
}

public class FindApprenticeshipTrainingAndManageRequestsPage(ScenarioContext context) : RatProjectBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Find apprenticeship training and manage requests");


    public async Task<TrainingRequestDetailPage> SelectActiveRequest()
    {
        string trainingCourseName = objectContext.GetTrainingCourseName();

        string href = await page.GetByRole(AriaRole.Link, new() { Name = trainingCourseName, Exact = true }).GetAttributeAsync("href");

        objectContext.SetDebugInformation($"Active request href - {href}");

        var requestId = GenerateGuidRegexHelper.FindGuidRegex().Match(href).Value;

        objectContext.SetDebugInformation($"Found request id - {requestId}");

        await page.GetByRole(AriaRole.Link, new() { Name = trainingCourseName, Exact = true }).ClickAsync();

        ratDataHelper.TrainingCourse = trainingCourseName;

        ratDataHelper.RequestId = requestId;

        return await VerifyPageAsync(() => new TrainingRequestDetailPage(context, trainingCourseName));
    }

    public async Task<ApprenticeshipTrainingCoursesPage> GoToApprenticeshipTrainingCourses()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Find apprenticeship training" }).ClickAsync();

        return await VerifyPageAsync(() => new ApprenticeshipTrainingCoursesPage(context));
    }
}

public class TrainingRequestDetailPage(ScenarioContext context, string requestName) : RatProjectBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync(requestName);

    public async Task<CancelYourRequestPage> CancelYourRequest()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "cancel your request" }).ClickAsync();

        return await VerifyPageAsync(() => new CancelYourRequestPage(context));
    }

    public async Task VerifyProviderResponse()
    {
        await Assertions.Expect(page.Locator("#main-content")).ToContainTextAsync(ratDataHelper.ProviderEmail);

        await Assertions.Expect(page.Locator("h3")).ToContainTextAsync(ratDataHelper.ProviderName);
    }
}

public class CancelYourRequestPage(ScenarioContext context) : RatProjectBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Cancel your request");

    public async Task<WeCancelledYourRequestPage> SubmitCancelRequest()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Cancel request" }).ClickAsync();

        return await VerifyPageAsync(() => new WeCancelledYourRequestPage(context));
    }
}

public class WeCancelledYourRequestPage(ScenarioContext context) : RatProjectBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("We've cancelled your request");

}