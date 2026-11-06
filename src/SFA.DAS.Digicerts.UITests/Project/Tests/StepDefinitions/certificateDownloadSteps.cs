
using Reqnroll;
using SFA.DAS.Digicerts.UITests.Project.Tests.Pages.Dashboard;
using System.Threading.Tasks;


[Binding]
public class CertificateDownloadSteps(ScenarioContext context)
{

    [Then(@"^User is able to Download Standard Certificate in PDF format$")]
    public async Task ThenUserIsAbleToStandardDownloadCertificateInPDFFormat()
    {
        await new DigiCertsStandardDetailsPage(context).verifyStandardCertificatePDF();
    }

    [Then(@"^User is able to Download Framework Certificate in PDF format$")]
    public async Task ThenUserIsAbleToFrameworkDownloadCertificateInPDFFormat()
    {
        await new DigiCertsFrameworkDetailsPage(context).verifyFrameworkCertificatePDF();
    }

}
