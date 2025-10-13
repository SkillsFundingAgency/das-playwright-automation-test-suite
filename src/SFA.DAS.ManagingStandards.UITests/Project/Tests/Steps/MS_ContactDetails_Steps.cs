using SFA.DAS.ManagingStandards.UITests.Project.Tests.Pages;

namespace SFA.DAS.ManagingStandards.UITests.Project.Tests.Steps;

[Binding]
public class MS_ContactDetails_Steps(ScenarioContext context)
{
    [Then("the provider is able to update contact details")]
    public async Task ThenTheProviderIsAbleToUpdateContactDetails()
    {
        var page = new ManagingStandardsProviderHomePage(context);

        var page1 = await page.NavigateToYourStandardsAndTrainingVenuesPage();

        var page2 = await page1.AccessContactDetails();

        var page3 = await page2.ChangeContactDetails();

        var page4 = await page3.ChangeEmailAndPhonenumberContactDetails();

        var page5 = await page4.NoDontUpdateExistingStandards();

        var page6 = await page5.ConfirmUpdateBothEmailAndPhonenumberContactDetailsAndContinue();

        await page6.ReturnToManagingStandardsDashboard();
    }
}