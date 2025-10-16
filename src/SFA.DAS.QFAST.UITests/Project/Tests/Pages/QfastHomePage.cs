using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.QFAST.UITests.Project.Tests.Pages
{
    public class QfastHomePage(ScenarioContext context) : BasePage(context)
    {
        public override async Task VerifyPage() => await Assertions.Expect(page.GetByRole(AriaRole.Heading)).ToContainTextAsync("What do you want to do?", new() { Timeout = LandingPageTimeout });
       
    }


}
