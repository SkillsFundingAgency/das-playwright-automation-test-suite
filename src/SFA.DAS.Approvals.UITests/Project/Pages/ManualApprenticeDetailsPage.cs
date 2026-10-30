using SFA.DAS.Approvals.UITests.Project.Helpers.DataHelpers.ApprenticeshipModel;
using SFA.DAS.Approvals.UITests.Project.Pages.Employer;
using SFA.DAS.Approvals.UITests.Project.Pages.Provider;
using SFA.DAS.TestDataCleanup.Project.Helpers;
using SFA.DAS.Approvals.UITests.Project.Helpers.TestDataHelpers;
using System.Globalization;
using System.Text.RegularExpressions;

namespace SFA.DAS.Approvals.UITests.Project.Pages;

internal class ManualApprenticeDetailsPage(ScenarioContext context, bool provider) : ApprovalsBasePage(context)
{
    public override async Task VerifyPage() =>
        await Assertions.Expect(page.GetByRole(AriaRole.Heading).Filter(new() { HasText = "Add apprentice details" })).ToBeVisibleAsync();

    internal async Task FillDetails(Apprenticeship apprenticeship)
    {
        var apprentice = apprenticeship.ApprenticeDetails;
        var training = apprenticeship.TrainingDetails;
        if (provider)
            await page.Locator("[name='Uln']").FillAsync(apprentice.ULN);
        await page.Locator("[name='FirstName']").FillAsync(apprentice.FirstName);
        await page.Locator("[name='LastName']").FillAsync(apprentice.LastName);
        await page.Locator("[name='Email']").FillAsync(apprentice.Email);
        objectContext.AddDbNameToTearDown(CleanUpDbName.EasAppAccTestDataCleanUp, apprentice.Email);
        await page.Locator("[name='BirthDay']").FillAsync(apprentice.DateOfBirth.Day.ToString());
        await page.Locator("[name='BirthMonth']").FillAsync(apprentice.DateOfBirth.Month.ToString());
        await page.Locator("[name='BirthYear']").FillAsync(apprentice.DateOfBirth.Year.ToString());
        await page.Locator("[name='StartMonth']").FillAsync(training.StartDate.Month.ToString());
        await page.Locator("[name='StartYear']").FillAsync(training.StartDate.Year.ToString());
        await page.Locator("[name='EndMonth']").FillAsync(training.EndDate.Month.ToString());
        await page.Locator("[name='EndYear']").FillAsync(training.EndDate.Year.ToString());
        await page.Locator("[name='Cost']").FillAsync(training.TotalPrice.ToString());
        await page.Locator("#Reference").FillAsync(training.ConsumerReference);
        if (!provider)
        {
            var course = await new CoursesDataHelper().GetCourse(training.LarsCode);
            var funding = page.Locator("#addApprenticeship dl").Filter(new() { HasText = training.CourseTitle });
            await Assertions.Expect(funding.Locator("dd").Nth(0)).ToContainTextAsync(training.CourseTitle);
            await Assertions.Expect(funding.Locator("dd").Nth(1)).ToHaveTextAsync(
                new Regex($@"^\s*\u00a3{Regex.Escape(course.MaxFunding.ToString("N0", CultureInfo.GetCultureInfo("en-GB")))}\s*$"));
        }
    }

    internal async Task<EmployerApproveLearnerDetailsPage> SaveEmployerDetails()
    {
        await page.Locator("#main-content .govuk-button").ClickAsync();
        var review = new EmployerApproveLearnerDetailsPage(context);
        await review.VerifyManualEntryPage();
        return review;
    }

    internal async Task<ApproveApprenticeDetailsPage> SaveProviderDetails()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Add", Exact = true }).ClickAsync();
        var rpl = await VerifyPageAsync(() => new RecognitionOfPriorLearningPage(context));
        return await rpl.SelectNoForManualEntryRPL();
    }
}
