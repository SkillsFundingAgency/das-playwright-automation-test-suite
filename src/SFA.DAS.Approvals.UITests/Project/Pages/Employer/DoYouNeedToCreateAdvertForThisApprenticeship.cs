
namespace SFA.DAS.Approvals.UITests.Project.Pages.Employer
{
    internal class DoYouNeedToCreateAdvertForThisApprenticeship(ScenarioContext context) : ApprovalsBasePage(context)
    {
        public override async Task VerifyPage() => await Assertions.Expect(page.Locator(".govuk-heading-l").First).ToContainTextAsync("Do you need to create an advert for this apprenticeship?");


        public async Task<DoYouNeedToCreateAdvertForThisApprenticeship> Yes()
        {
            // [value='No']
            await page.Locator("[value='True']").ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();
            return await VerifyPageAsync(() => new DoYouNeedToCreateAdvertForThisApprenticeship(context));
        }


        public async Task<AddApprenticePage> No()
        {
            await page.Locator("[value='False']").ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();
            return await VerifyPageAsync(() => new AddApprenticePage(context));
        }
    }
}