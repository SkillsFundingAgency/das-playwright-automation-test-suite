
namespace SFA.DAS.AODP.UITests.Project.Tests.Pages.AO
{
    public class AodpAoLoginPage(ScenarioContext context) : AodpAoHomePage(context)
    {

        private ILocator Username => page.GetByLabel("Email address");
        private ILocator Next => page.GetByText("Next");


        public async Task<AodpAoPasswordPage> LoginAsReviewer()
        {
            await Username.FillAsync("aodpTestAdmin1@l38cxwya.mailosaur.net");
            await Next.ClickAsync();

            return await new AodpAoPasswordPage(context).LoginAsReviewer();
        }
    }
}
