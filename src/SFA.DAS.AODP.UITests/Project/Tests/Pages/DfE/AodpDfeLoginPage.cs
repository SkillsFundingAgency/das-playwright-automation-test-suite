
namespace SFA.DAS.AODP.UITests.Project.Tests.Pages.DfE
{
    public class AodpDfeLoginPage(ScenarioContext context) : AodpDfeHomePage(context)
    {

        private ILocator Username => page.GetByLabel("Email address");
        private ILocator Next => page.GetByText("Next");

        public async Task<AodpDfePasswordPage> LoginAsReviewer()
        {
            await Username.FillAsync("aodpTestAdmin1@l38cxwya.mailosaur.net");
            await Next.ClickAsync();

            return new AodpDfePasswordPage(context);
        }
    }
}
