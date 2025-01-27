namespace SFA.DAS.SupportConsole.UITests.Project.Tests.Pages;

public class UlnDetailsPage(ScenarioContext context, CohortDetails cohortDetails) : SupportConsoleBasePage(context)
{
    private readonly CohortDetails cohortDetails = cohortDetails;

    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("#content")).ToContainTextAsync(cohortDetails.UlnName);

    public async Task VerifyUlnDetailsPageHeaders()
    {
        await Assertions.Expect(page.Locator("#content")).ToContainTextAsync(cohortDetails.Uln);

        await Assertions.Expect(page.Locator("#tab-summary")).ToContainTextAsync(cohortDetails.CohortRef);
    }

    //protected void ClickTab(By by) => formCompletionHelper.ClickElement(() => pageInteractionHelper.FindElement(by));

    //protected void IsTabDisplayed(By by) => Assert.IsTrue(pageInteractionHelper.FindElements(by).Count > 0);
}