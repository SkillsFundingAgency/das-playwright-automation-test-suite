using Polly;
using SFA.DAS.QFAST.UITests.Project.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SFA.DAS.QFAST.UITests.Project.Tests.Steps
{
    [Binding]
    public class LoginSteps
    {
        private readonly ScenarioContext _context;
        private readonly QfastHelpers _qfastHelpers;
        public LoginSteps(ScenarioContext context)
        {
            _context = context;
            _qfastHelpers = new QfastHelpers(context);
        }

        [Given("the admin user log in to the portal")]
        public async Task GivenTheAdminUserLogInToThePortal()
        {
            await _qfastHelpers.NavigateToQFASTPortal();
        }
    }
}
