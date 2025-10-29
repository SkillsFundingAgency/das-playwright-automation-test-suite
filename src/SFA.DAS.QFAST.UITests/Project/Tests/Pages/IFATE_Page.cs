using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.QFAST.UITests.Project.Tests.Pages;

public class IFATE_Page(ScenarioContext context) : BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.GetByRole(AriaRole.Heading, new() { Name = "Requests for funding" })).ToBeVisibleAsync();
}
