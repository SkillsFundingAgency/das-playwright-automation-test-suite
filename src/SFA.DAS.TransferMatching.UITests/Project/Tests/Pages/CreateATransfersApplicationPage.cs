namespace SFA.DAS.TransferMatching.UITests.Project.Tests.Pages;

public class CreateATransfersApplicationPage(ScenarioContext context) : TransferMatchingBasePage(context)
{
    protected override string PageTitle => "Create a transfer application";
    protected override ILocator ContinueButton => page.Locator("#opportunity-apply-submit");

    public async Task<ApplicationSubmittedPage> SubmitApplication()
    {
        await Continue();
        return await VerifyPageAsync(() => new ApplicationSubmittedPage(context));
    }

    public async Task<CreateATransfersApplicationPage> VerifyStatusIsComplete()
    {
        var completedStatus = page.Locator(".govuk-tag.app-task-list__group-header-tag")
            .Filter(new() { HasTextRegex = new System.Text.RegularExpressions.Regex(@"\bcomplete(d)?\b", System.Text.RegularExpressions.RegexOptions.IgnoreCase) });
        await Assertions.Expect(completedStatus.First).ToBeVisibleAsync();
        return this;
    }

    public async Task<ApprenticeshipTrainingPage> GoToApprenticeshipTrainingPage() =>
        await FollowLink(GetRandomLink(["Job role", "Number of learners", "Start date", "Have you found a training provider?"]),
            () => new ApprenticeshipTrainingPage(context));

    public async Task<AddYourBusinessDetailsPage> GoToYourBusinessDetailsPage() =>
        await FollowLink(GetRandomLink(["Sector", "Location"]), () => new AddYourBusinessDetailsPage(context));

    public async Task<AboutYourApprenticeshipPage> GoToAboutYourApprenticeshipPage() =>
        await FollowLink("More detail", () => new AboutYourApprenticeshipPage(context));

    public async Task<AddContactDetailsPage> GoToContactDetailsPage() =>
        await FollowLink(GetRandomLink(["Name", "Email address", "Business website"]), () => new AddContactDetailsPage(context));

    private static string GetRandomLink(string[] links) => links[Random.Shared.Next(links.Length)];
}
