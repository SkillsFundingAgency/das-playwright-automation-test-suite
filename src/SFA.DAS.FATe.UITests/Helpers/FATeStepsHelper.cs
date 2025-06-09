using SFA.DAS.FATe.UITests.Project.Tests.Pages;

namespace SFA.DAS.FATe.UITests.Helpers
{
    public class FATeStepsHelper(ScenarioContext context)
    {
        public async Task<FATeHomePage> AcceptCookiesAndGoToFATeHomePage() => await new FATeHomePage(context).AcceptCookieAndAlert();

        public async Task<FATeHomePage> ReturnToStartPage() => await new FATeHomePage(context).ReturnToStartPage();

        public async Task SearchForTrainingCourse(string course)
        {
            var page = await new FATeHomePage(context).ClickStartNow();

            var page1 = await page.BrowseAllCourses();

            await page1.SearchApprenticeshipInApprenticeshipTrainingCoursesPage(course);


        }
    }
}
