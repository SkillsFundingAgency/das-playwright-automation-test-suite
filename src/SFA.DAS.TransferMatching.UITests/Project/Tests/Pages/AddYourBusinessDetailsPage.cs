namespace SFA.DAS.TransferMatching.UITests.Project.Tests.Pages;

public class AddYourBusinessDetailsPage(ScenarioContext context) : TransferMatchingBasePage(context)
{
    protected override string PageTitle => "Add your business details";
    protected override ILocator ContinueButton => page.Locator("#opportunity-criteria-continue");

    public async Task<CreateATransfersApplicationPage> EnterBusinessDetailsAndContinue()
    {
        await SelectRandomCheckbox();
        var location = TMDataHelper.GetRandomLocation();
        var additionalLocation = page.Locator("#AdditionalLocation");
        if (await additionalLocation.CountAsync() > 0)
        {
            await additionalLocation.CheckAsync();
            await page.Locator("#AdditionalLocationText").FillAsync(location);
        }
        else
        {
            await page.Locator("#SpecificLocation").FillAsync(location);
        }
        await Continue();
        return await VerifyPageAsync(() => new CreateATransfersApplicationPage(context));
    }
}
