

namespace SFA.DAS.AODP.UITests.Project.Tests.Pages.AO
{
    public class ChangedQualificationsHomePage(ScenarioContext context) : AoStartPage(context)
    {

        public ILocator NewQualificationsPage => page.GetByText("Changed Qualifications");

        public ILocator QualificationName => page.Locator("#Filter_QualificationName");
        public ILocator QAN => page.Locator("#Filter_QAN");
        public ILocator Organisation => page.Locator("#Filter_Organisation");
        public ILocator RecordsPerPage => page.Locator("#PaginationViewModel_RecordsPerPage");
        public ILocator SearchBtn => page.Locator("[type=\"submit\"]");

        public ILocator NoQualificationsError => page.Locator("//*[.=\"No qualifications match your search criteria\"]");

        public override async Task VerifyPage() => await Assertions.Expect(NewQualificationsPage).ToBeVisibleAsync();
    }
}
