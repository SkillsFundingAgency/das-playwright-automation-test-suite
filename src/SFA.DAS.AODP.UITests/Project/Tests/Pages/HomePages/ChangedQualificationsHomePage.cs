

namespace SFA.DAS.AODP.UITests.Project.Tests.Pages.AO
{
    public class ChangedQualificationsHomePage(ScenarioContext context) : AoStartPage(context)
    {

        public ILocator ChangedQualificationsPage => page.Locator("//h2[.=\"Qualifications with changes\"]");

        public ILocator QualificationName => page.Locator("#Filter_QualificationName");
        public ILocator QAN => page.Locator("#Filter_QAN");
        public ILocator Organisation => page.Locator("#Filter_Organisation");
        public ILocator RecordsPerPage => page.Locator("#PaginationViewModel_RecordsPerPage");
        public ILocator SearchBtn => page.Locator("[type=\"submit\"]");

        public ILocator NoQualificationsError => page.Locator("//*[.=\"No Results\"]");

        public override async Task VerifyPage() => await Assertions.Expect(ChangedQualificationsPage).ToBeVisibleAsync();
    }
}
