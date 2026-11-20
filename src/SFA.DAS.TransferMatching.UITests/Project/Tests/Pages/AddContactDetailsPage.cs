namespace SFA.DAS.TransferMatching.UITests.Project.Tests.Pages;

public class AddContactDetailsPage(ScenarioContext context) : TransferMatchingBasePage(context)
{
    protected override string PageTitle => "Add contact details";
    protected override ILocator ContinueButton => page.Locator("#application-contact-details");

    public async Task<CreateATransfersApplicationPage> EnterContactDetailsAndContinue()
    {
        var data = context.Get<EmployerPortalDataHelper>();
        await page.Locator("#FirstName").FillAsync(data.FirstName);
        await page.Locator("#LastName").FillAsync(data.LastName);
        await page.Locator("#EmailAddress").FillAsync(data.RandomEmail);
        await page.Locator("#BusinessWebsite").FillAsync("www.example.com");
        await Continue();
        return await VerifyPageAsync(() => new CreateATransfersApplicationPage(context));
    }
}
