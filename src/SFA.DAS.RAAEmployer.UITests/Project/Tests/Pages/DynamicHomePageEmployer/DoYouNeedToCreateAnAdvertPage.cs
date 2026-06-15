using SFA.DAS.RAAEmployer.UITests.Project.Tests.Pages;

namespace SFA.DAS.RAAEmployer.UITests.Project.Tests.Pages.DynamicHomePageEmployer
{
    public abstract class DoYouNeedToCreateAnAdvertBasePage : RaaBasePage
    {
        protected virtual string PageTitleText => "Do you need to create an advert for this apprenticeship?";
        protected virtual string PageHeaderId => "heading-continue-setup-create-advert";
        protected virtual string ContinueButtonId => "accept";
        protected virtual string NoRadioButtonId => "choice2-no";
        protected virtual string YesRadioButtonId => "choice1-yes";

        public DoYouNeedToCreateAnAdvertBasePage(ScenarioContext context) : base(context)
        {
        }

        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator($"#{PageHeaderId}")).ToBeVisibleAsync();
        }

        protected async Task ClickYesRadioButton()
        {
            await page.Locator($"#{YesRadioButtonId}").ClickAsync();
        }

        protected async Task ClickNoRadioButton()
        {
            await page.Locator($"#{NoRadioButtonId}").ClickAsync();
        }

        protected async Task ClickContinue()
        {
            await page.Locator($"#{ContinueButtonId}").ClickAsync();
        }
    }

    public class DoYouNeedToCreateAnAdvertPage(ScenarioContext context) : DoYouNeedToCreateAnAdvertBasePage(context)
    {
        public async Task<CreateAnAdvertHomePage> ClickYesRadioButtonTakesToRecruitment()
        {
            await ClickYesRadioButton();
            await ClickContinue();
            return await VerifyPageAsync(() => new CreateAnAdvertHomePage(context));
        }
    }
}
