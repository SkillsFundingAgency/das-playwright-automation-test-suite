

using SFA.DAS.EmployerPortal.UITests.Project.Tests.Pages;
using SFA.DAS.QFAST.UITests.Project.Tests.Pages.Application;
using System.Text.RegularExpressions;

namespace SFA.DAS.QFAST.UITests.Project.Tests.Pages
{
    public class QualificationDetails_Page (ScenarioContext context) : BasePage(context)
    {
        public override async Task VerifyPage() => await Assertions.Expect(page.GetByRole(AriaRole.Heading, new() { Name = "Qualification Details" })).ToBeVisibleAsync();
        public async Task<Application_Details_Page> ClickOnFirstAssociatedApplication()
        {
            var firstApplicationLink = page.GetByRole(AriaRole.Link, new() { NameRegex = new Regex("^View application") }).First;
            await firstApplicationLink.ClickAsync();
            return await VerifyPageAsync(() => new Application_Details_Page(context));
        }
    }
}
