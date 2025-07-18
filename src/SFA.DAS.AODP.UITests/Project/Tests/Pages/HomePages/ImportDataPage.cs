

using SFA.DAS.AODP.UITests.Project.Tests.Pages.Common;

namespace SFA.DAS.AODP.UITests.Project.Tests.Pages.AO
{
    public class ImportDataPage(ScenarioContext context) : AodpHomePage(context)
    {

        public ILocator ImportPageTitle => page.Locator("//h1[.=\"Qualifications you need to import\"]");

        public override async Task VerifyPage() => await Assertions.Expect(ImportPageTitle).ToBeVisibleAsync();

    }
}
