using SFA.DAS.EmployerPortal.UITests.Project;
using SFA.DAS.EmployerPortal.UITests.Project.Pages.StubPages;
using SFA.DAS.ProviderPortal.UITests.Project.Helpers;
using SFA.DAS.ProviderPortal.UITests.Project.Pages;

namespace SFA.DAS.ProviderPortal.UITests.Project.Tests.Steps;

[Binding]
public class ProviderCreateAccountSteps(ScenarioContext context) : ProviderPortalBaseSteps(context)
{
    [Given(@"a provider requests employer to create account with all permission")]
    public async Task AProviderRequestsEmployerToCreateAccountWithAllPermission()
    {
        await GoToProviderAddAnEmployer();

        eprDataHelper.EmployerEmail = objectContext.GetRegisteredEmail();

        var page = await new AddAnEmployerPage(context).StartNowToAddAnEmployer();

        var page1 = await page.EnterNewEmployerEmail();

        var page2 = await page1.SubmitPayeAndContinueToInvite();

        var page3 = await page2.SubmitEmployerName();

        var page4 = await page3.SendInvitation();

        var page5 = await page4.GoToViewEmployersPage();

        await page5.VerifyPendingRequest();

        await SetRequestId(RequestType.CreateAccount);
    }

    [Then(@"the employer declines the create account request")]
    public async Task TheEmployerDeclinesTheCreateAccountRequest()
    {
        var page = await OpenEmpInviteFromProviderAndRegister();

        var page1 = await page.ReadAgreement(eprDataHelper.EmployerOrganisationName);

        var page2 = await page1.ReturnToCreateYourApprenticeshipServiceAccount();

        var page3 = await page2.DoNotCreateAccount();

        await page3.ConfirmDoNotCreateAccount();
    }

    [Then(@"the employer accepts the create account request")]
    public async Task TheEmployerAcceptsTheCreateAccountRequest()
    {
        var page = await OpenEmpInviteFromProviderAndRegister();

        var page1 = await page.ChangeName();

        var page2 = await page1.ChangeName(eprDataHelper.EmployerFirstName, eprDataHelper.EmployerLastName);

        var page3 = await page2.CreateAccount();

        await page3.GoToHomePage();
    }

    private async Task<CreateYourApprenticeshipServiceAccount> OpenEmpInviteFromProviderAndRegister()
    {
        await OpenEmpInviteFromProvider();

        var page = await new StubSignInEmployerPage(context).Register();

        await page.Continue();

        return await VerifyPageHelper.VerifyPageAsync(context, () => new CreateYourApprenticeshipServiceAccount(context));
    }


    [Given("a provider requests employer to create account with updated name and requests only RecruitApprenticeButWithEmployerReview")]
    public async Task GivenAProviderRequestsEmployerToCreateAccountWithUpdatedNameAndRequestsOnlyRecruitApprenticeButWithEmployerReview()
    {
        await GoToProviderAddAnEmployer();

        eprDataHelper.EmployerEmail = objectContext.GetRegisteredEmail();

        var page = await new AddAnEmployerPage(context).StartNowToAddAnEmployer();

        var page1 = await page.EnterNewEmployerEmail();

        var page2 = await page1.SubmitPayeAndContinueToInvite();

        var page3 = await page2.SubmitEmployerName();

        var page4 = await page3.AccessChangeEmployerName();

        var page5 = await page4.SubmitEmployerName();

        var page6 = await page5.AccessChangePermissions();

        var page7 = await page6.ProviderRequestNewPermissions((AddApprenticePermissions.NoToAddApprenticeRecords, RecruitApprenticePermissions.YesRecruitApprenticesButEmployerWillReview));

        var page8 = await page7.SendInvitation();

        var page9 = await page8.GoToViewEmployersPage();

        await page9.VerifyPendingRequest();
    }
}