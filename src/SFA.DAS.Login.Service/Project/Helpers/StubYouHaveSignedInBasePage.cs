﻿

namespace SFA.DAS.Login.Service.Project.Helpers;

public abstract class StubYouHaveSignedInBasePage(ScenarioContext context, string username, string idOrUserRef, bool newUser) : BasePage(context)
{
    protected readonly string username = username;

    protected string idOrUserRef = idOrUserRef;

    protected readonly bool newUser = newUser;

    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("You've signed in");

        await Assertions.Expect(page.Locator("#main-content")).ToContainTextAsync(username);

        if (!newUser) await Assertions.Expect(page.Locator("#main-content")).ToContainTextAsync(idOrUserRef);
    }
}
