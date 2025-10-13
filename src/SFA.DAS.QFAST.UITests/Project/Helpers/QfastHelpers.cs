using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.QFAST.UITests.Project.Helpers
{
    public class QfastHelpers(ScenarioContext context){
        public async Task NavigateToQFASTPortal()
        {
            var driver = context.Get<Driver>();

            var url = UrlConfig.QFAST_BaseUrl;

            context.Get<ObjectContext>().SetDebugInformation(url);

            await driver.Page.GotoAsync(url);
        }

    }
}
