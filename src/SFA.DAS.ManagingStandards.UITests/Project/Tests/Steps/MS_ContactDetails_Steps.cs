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

        var page6 = await page5.ConfirmUpdateContactDetailsAndContinue();

        await page6.ReturnToManagingStandardsDashboard();
    }

    [Then("the provider is able to update phone number only")]
    public async Task ThenTheProviderIsAbleToUpdatePhoneNumberOnly()
    {
        var page = new ManagingStandardsProviderHomePage(context);

        var page1 = await page.NavigateToYourStandardsAndTrainingVenuesPage();

        var page2 = await page1.AccessContactDetails();

        var page3 = await page2.ChangeContactDetails();

        var page4 = await page3.ChangePhonenumberOnly();

        var page5 = await page4.NoDontUpdateExistingStandards();

        var page6 = await page5.ConfirmUpdateContactDetailsAndContinue();

        await page6.ReturnToManagingStandardsDashboard();

    }

    [Then("the provider is able to update email only")]
    public async Task ThenTheProviderIsAbleToUpdateEmailOnly()
    {
        var page = new ManagingStandardsProviderHomePage(context);

        var page1 = await page.NavigateToYourStandardsAndTrainingVenuesPage();

        var page2 = await page1.AccessContactDetails();

        var page3 = await page2.ChangeContactDetails();

        var page4 = await page3.ChangeEmailOnly();

        var page5 = await page4.NoDontUpdateExistingStandards();

        var page6 = await page5.ConfirmUpdateContactDetailsAndContinue();

        await page6.ReturnToManagingStandardsDashboard();

    }

    [Then("the provider is able to update contact details to all the standards")]
    public void ThenTheProviderIsAbleToUpdateContactDetailsToAllTheStandards()
    {
        throw new PendingStepException();
    }


}