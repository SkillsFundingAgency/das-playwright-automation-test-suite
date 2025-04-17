
using System;
using System.Runtime.ConstrainedExecution;
using SFA.DAS.DfeAdmin.Service.Project.Helpers.DfeSign.User;
using SFA.DAS.Login.Service.Project;
using SFA.DAS.Login.Service.Project.Helpers;

namespace SFA.DAS.AODP.UITests.Project.Tests.Pages.DfE
{
    public class AodpLoginPage(ScenarioContext context) : DfeAdminHomePage(context)
    {

        private ILocator Username => page.GetByLabel("Email address");

        private ILocator Password => page.GetByLabel("Password");
        private ILocator SignIn => page.GetByText("Sign in");
        private ILocator Next => page.GetByText("Next");

        public override async Task VerifyPage() => await Assertions.Expect(page.GetByRole(AriaRole.Heading)).ToHaveTextAsync("Access the DfE Sign-in service");

        public async Task LoginAsDfeUser(string role)
        {
            // AodpPortalDfeUserUser user = context.GetUser<AodpPortalDfeUserUser>();
            // Console.WriteLine(">>>>", user.Username);
            string userName = await GetUser(role);
            await Username.FillAsync(userName);
            await Next.ClickAsync();
            await Password.Nth(0).FillAsync(await GetPassword());
            await SignIn.Nth(1).ClickAsync();
        }
    }
}
