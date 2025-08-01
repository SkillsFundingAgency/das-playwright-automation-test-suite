namespace SFA.DAS.ApprenticeAmbassadorNetwork.UITests.Project.Tests.Pages.Apprentice.StubPages;

public class StubYouHaveSignedInApprenticeAccountsPage(ScenarioContext context, string username, string idOrUserRef, bool newUser) : StubYouHaveSignedInBasePage(context, username, idOrUserRef, newUser)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("You've signed in");

        await Assertions.Expect(page.Locator("#estimate-start-transfer")).ToContainTextAsync(username);

        if (!newUser) await Assertions.Expect(page.Locator("#estimate-start-transfer")).ToContainTextAsync(idOrUserRef);
    }

    public async Task Continue() => await page.GetByRole(AriaRole.Link, new() { Name = "Continue" }).ClickAsync();
}
