namespace SFA.DAS.Campaigns.UITests.Project.Tests.Pages.Apprentices;

public class ApprenticeHowDoTheyWorkPage(ScenarioContext context) : ApprenticeBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("How do they work?");

    public async Task<ApprenticeHowDoTheyWorkPage> VerifyHowDoTheyWorkPageSubHeadings() => await VerifyFiuCards(() => NavigateToHowDoTheyWorkPage());
}
