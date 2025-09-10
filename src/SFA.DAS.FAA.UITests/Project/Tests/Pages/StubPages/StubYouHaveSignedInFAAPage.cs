namespace SFA.DAS.FAA.UITests.Project.Tests.Pages.StubPages;

public class StubYouHaveSignedInFAAPage(ScenarioContext context, string username, string idOrUserRef, bool newUser) : StubYouHaveSignedInBasePage(context, username, idOrUserRef, newUser)
{
    public async Task Continue() => await page.GetByRole(AriaRole.Link, new() { Name = "Continue" }).ClickAsync();
}
