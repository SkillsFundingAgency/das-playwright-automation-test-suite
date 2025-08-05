
using SFA.DAS.Approvals.UITests.Project.Tests.Pages.Employer;
using TechTalk.SpecFlow;
using SFA.DAS.Approvals.UITests.Project.Pages;
using NUnit.Framework.Constraints;
using SFA.DAS.Approvals.UITests.Project.Pages.Employer;
using Azure;



namespace SFA.DAS.Approvals.UITests.Project.Tests.Pages.Employer
{
    public abstract class AddAnApprenitcePage(ScenarioContext context) : ApprovalsBasePage(context)
    {

        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Add an apprentice");
        }

        internal async Task<SelectFundingPage> StartNowToSelectFundingk()
        {
            await page.GetByRole(AriaRole.Link, new() { Name = "Start now" }).ClickAsync();
            return await VerifyPageAsync(() => new SelectFundingPage(context));
        }


    }
}


