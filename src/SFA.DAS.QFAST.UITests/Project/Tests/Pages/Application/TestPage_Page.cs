using SFA.DAS.QFAST.UITests.Project.Helpers;
namespace SFA.DAS.QFAST.UITests.Project.Tests.Pages.Application
{
    public class TestPage_Page(ScenarioContext context) : BasePage(context)
    {
        public override async Task VerifyPage() => await Assertions.Expect(page.GetByRole(AriaRole.Heading, new() { Name = "Test Page" })).ToBeVisibleAsync();
        protected readonly QfastDataHelpers qfastDataHelpers = context.Get<QfastDataHelpers>();
        public async Task<TestSection_Page> SubmitTheAnswer()
        {
            await page.Locator("input[name='Questions[0][Answer][TextValue]']").FillAsync(qfastDataHelpers.AwardingOrganisation);
            await page.Locator("textarea[name='Questions[1][Answer][TextValue]']").FillAsync(qfastDataHelpers.QualificationTitle);
            await page.Locator("input[name='Questions[2][Answer][NumberValue]']").FillAsync(qfastDataHelpers.PhoneNumber);
            await page.Locator("input[name='Questions[3][Answer][DateValue.Day]']").FillAsync(qfastDataHelpers.Day);
            await page.Locator("input[name='Questions[3][Answer][DateValue.Month]']").FillAsync(qfastDataHelpers.Month);
            await page.Locator("input[name='Questions[3][Answer][DateValue.Year]']").FillAsync(qfastDataHelpers.Year);
            await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();
            return await VerifyPageAsync(() => new TestSection_Page(context));
        }
    }
}
