namespace SFA.DAS.ApprenticeAmbassadorNetwork.UITests.Project.Tests.Pages.AppEmpCommonPages;

public class ApprenticeMessagePage : AppEmpCommonBasePage
{
    public override async Task VerifyPage()
    {
        if (IsRegionalChair)
        {
            await Assertions.Expect(page.GetByRole(AriaRole.Strong)).ToContainTextAsync("Regional chair");
        }
        else
        {
            await Assertions.Expect(page.GetByRole(AriaRole.Strong)).ToContainTextAsync("Apprentice");
        }

        if (!string.IsNullOrEmpty(ApprenticeName))
        {
            await Assertions.Expect(page.Locator("#main-content")).ToContainTextAsync(ApprenticeName);
        }       
    }

    private readonly string ApprenticeName;

    private readonly bool IsRegionalChair;

    //private static By CodeOfConduct => By.CssSelector("input[id='hasAgreedToCodeOfConduct']");

    //private static By SendMessageButton => By.CssSelector("[id='sendMessage']");

    public ApprenticeMessagePage(ScenarioContext context, bool isRegionalChair) : base(context)
    {
        IsRegionalChair = isRegionalChair;
    }

    public ApprenticeMessagePage(ScenarioContext context, string apprenticeName) : base(context)
    {
        ApprenticeName = apprenticeName;
    }

    public async Task<ApprenticeMessagePage> GoToApprenticeMessagePage((string id, string fullname) apprentice)
    {
        await GoToId(apprentice.id);

        return await VerifyPageAsync(() => new ApprenticeMessagePage(context, apprentice.fullname));
    }

    public async Task<SucessfullySentMessagePage> SendMessage(string message)
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = message }).CheckAsync();

        await page.GetByRole(AriaRole.Checkbox, new() { Name = "my details including my name" }).CheckAsync();

        await page.GetByRole(AriaRole.Checkbox, new() { Name = "adhere to the code of conduct" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Send the message" }).ClickAsync();

        return await VerifyPageAsync(() => new SucessfullySentMessagePage(context));
    }
}

public class SucessfullySentMessagePage(ScenarioContext context) : AanBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Your message has been sent successfully");
    }

    public async Task<NetworkDirectoryPage> AccessNetworkDirectory()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Network Directory" }).ClickAsync();

        return await VerifyPageAsync(() => new NetworkDirectoryPage(context));
    }
}