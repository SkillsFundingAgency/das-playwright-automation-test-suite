using Azure;
using SFA.DAS.FATe.UITests.Project.Tests.Pages;

public class Specific_TrainingProviderPage : FATeBasePage
{
    private readonly string _providerName;

    public Specific_TrainingProviderPage(ScenarioContext context, string providerName) : base(context)
    {
        _providerName = providerName;
    }

    public override async Task VerifyPage()
    {
        await VerifyPage(_providerName);
    }

    public async Task VerifyPage(string providerName)
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync(providerName);
        //This test is failing due to bug in html where h1 is missing. We will get this fixed
    }
}
