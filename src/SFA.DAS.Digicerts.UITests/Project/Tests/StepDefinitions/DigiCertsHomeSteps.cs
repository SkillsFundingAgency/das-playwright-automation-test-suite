using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using SFA.DAS.Digicerts.UITests.Project.Tests.Pages;
using System;
using System.Threading.Tasks;
using TechTalk.SpecFlow;


[Binding]
public class CertificateSteps
{
    private readonly ScenarioContext _context;
    private readonly CertificatePage _certificatePage;

    public CertificateSteps(ScenarioContext context)
    {
        _context = context;
        _certificatePage = new CertificatePage(_context);
    }

    [Given(@"the user navigates to the start page")]
    public async Task GivenTheUserNavigatesToTheStartPage()
    {
        await _certificatePage.Navigate();
    }

    [When(@"the user clicks the Start button")]
    public async Task WhenTheUserClicksTheStartButton()
    {
        await _certificatePage.ClickStart();
    }

    [When(@"the user enters authentication details")]
    public async Task WhenTheUserEntersAuthenticationDetails()
    {
        await _certificatePage.EnterAuthenticationDetails();
    }

    [When(@"the user uploads the authentication file")]
    public async Task WhenTheUserUploadsTheAuthenticationFile()
    {
        await _certificatePage.UploadJson();
    }

    [When(@"the user authenticates the session")]
    public async Task WhenTheUserAuthenticatesTheSession()
    {
        await _certificatePage.Authenticate();
    }

    [When(@"the user continues to the verification page")]
    public async Task WhenTheUserContinuesToTheVerificationPage()
    {
        await _certificatePage.Continue();
        await _certificatePage.ClickVerify();
    }

    [Then(@"the user views available certificate categories")]
    public async Task ThenTheUserViewsAvailableCertificateCategories()
    {
        await _certificatePage.OpenCertificate("Installation electrician and");
        await _certificatePage.ClickBack();

        await _certificatePage.OpenCertificate("Management");
        await _certificatePage.ClickBack();

        await _certificatePage.OpenCertificate("Hairdressing");
        await _certificatePage.ClickBack();
    }
}
