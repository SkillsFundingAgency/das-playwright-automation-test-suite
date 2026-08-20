using Reqnroll;
using Reqnroll.EnvironmentAccess;
using SFA.DAS.Digicerts.UITests.Project.Tests.Pages;
using SFA.DAS.Digicerts.UITests.Project.Tests.Pages.Dashboard;
using System.Threading.Tasks;


[Binding]
public class CertificatesharingSteps(ScenarioContext context)
{

    [Then(@"^the user clicks the sharing link and verifies its details$")]
    public async Task ThenTheUserClicksTheSharingLinkAndVerifiesItsDetails()
    {
        var sharingLink = await new DigiCertsFrameworkDetailsPage(context).clickCreateLink();

        await sharingLink.verifyCreateSharingLinkPage();
    }

    [Then(@"^the user shares the certificate via email successfully$")]
    public async Task ThenTheUserSharesTheCertificateViaEmailSuccessfully()
    {
        await new DigiCertsCreateNewSharingLinkPage(context).sentEmail();
    }

    [Then(@"^the user opens the sharing link in a private browser and verifies the (.*) certificate details$")]
    public async Task ThenTheUserOpensTheSharingLinkInAPrivateBrowserAndVerifiesTheFrameworkCertificateDetails(string type)
    {
        var createNewSharingLink = await new DigiCertsCreateSharingLinkPage(context).clickCreateNewSharingLink();

        await createNewSharingLink.verifyCreateNewSharingLinkPage();

        await createNewSharingLink.viewSharingLinkPrivateBrowserandVerify(type);
    }


}