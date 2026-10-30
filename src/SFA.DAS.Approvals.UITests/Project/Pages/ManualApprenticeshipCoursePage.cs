using SFA.DAS.Approvals.UITests.Project.Helpers.DataHelpers.ApprenticeshipModel;

namespace SFA.DAS.Approvals.UITests.Project.Pages;

internal class ManualApprenticeshipCoursePage(ScenarioContext context, bool provider) : ApprovalsBasePage(context)
{
    public override async Task VerifyPage() =>
        await Assertions.Expect(page.GetByRole(AriaRole.Heading)
            .Filter(new() { HasText = provider ? "Select standard" : "What is the apprenticeship course?" })).ToBeVisibleAsync();

    internal async Task<ManualApprenticeDetailsPage> SelectCourse(Training training)
    {
        if (provider)
        {
            await page.Locator("#CourseCode").SelectOptionAsync(training.LarsCode);
        }
        else
        {
            await page.Locator("#CourseCode").FillAsync(training.CourseTitle);
            await page.Locator("#CourseCode__option--0").ClickAsync();
        }

        await page.Locator("#main-content").GetByRole(AriaRole.Button, new() { Name = "Continue", Exact = true }).ClickAsync();
        return await VerifyPageAsync(() => new ManualApprenticeDetailsPage(context, provider));
    }
}
