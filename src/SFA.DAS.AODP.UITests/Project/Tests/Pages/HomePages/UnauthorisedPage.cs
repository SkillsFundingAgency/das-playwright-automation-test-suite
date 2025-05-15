

namespace SFA.DAS.AODP.UITests.Project.Tests.Pages.AO
{
    public class UnauthorisedPage(ScenarioContext context) : AoStartPage(context)
    {

        public ILocator MainPage => page.Locator("//h1[.=\"Not Authorised\"]");

        public override async Task VerifyPage() => await Assertions.Expect(MainPage).ToBeVisibleAsync();
    }
}
