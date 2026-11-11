using SFA.DAS.TransferMatching.UITests.Project.Tests.Pages;
using SFA.DAS.EmployerPortal.UITests.Project.Pages;

namespace SFA.DAS.TransferMatching.UITests.Project.Helpers;

public static class SubmitApplicationHelper
{
    public static async Task<HomePage> SubmitApplication(CreateATransfersApplicationPage page, string pledgeId = "")
    {
        var training = await page.GoToApprenticeshipTrainingPage();
        page = await training.EnterAppTrainingDetailsAndContinue();
        await page.VerifyStatusIsComplete();
        var business = await page.GoToYourBusinessDetailsPage();
        page = await business.EnterBusinessDetailsAndContinue();
        await page.VerifyStatusIsComplete();
        var details = await page.GoToAboutYourApprenticeshipPage();
        page = await details.EnterMoreDetailsAndContinue(pledgeId);
        await page.VerifyStatusIsComplete();
        var contact = await page.GoToContactDetailsPage();
        page = await contact.EnterContactDetailsAndContinue();
        await page.VerifyStatusIsComplete();
        return await (await page.SubmitApplication()).ContinueToMyAccount();
    }

    public static async Task<ApprenticeshipTrainingPage> GoToApprenticeshipTrainingPage(CreateATransfersApplicationPage page) =>
        await page.GoToApprenticeshipTrainingPage();
}
