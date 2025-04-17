

using SFA.DAS.AODP.UITests.Project.Tests.Pages.Common;

namespace SFA.DAS.AODP.UITests.Project.Tests.Pages.AO
{
    public class OutputFilePage(ScenarioContext context) : AodpHomePage(context)
    {

        public ILocator OutputPage => page.GetByText("Output file");

        public override async Task VerifyPage() => await Assertions.Expect(OutputPage).ToBeVisibleAsync();

    }
}
