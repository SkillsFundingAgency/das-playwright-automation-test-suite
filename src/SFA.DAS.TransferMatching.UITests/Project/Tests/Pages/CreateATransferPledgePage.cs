namespace SFA.DAS.TransferMatching.UITests.Project.Tests.Pages;

public class CreateATransferPledgePage(ScenarioContext context) : TransferMatchingBasePage(context)
{
    protected override string PageTitle => "Create a transfer pledge";
    protected override ILocator ContinueButton => page.Locator("#pledge-create-submit");

    public static string LocationLink => "Location";
    public static string SectorLink => "Sector";
    public static string TypeOfJobRoleLink => "Type of job role";
    public static string LevelLink => "Level";

    public async Task<PledgeAmountPage> GoToPledgeAmountPage() =>
        await FollowLink("Amount you want to pledge", () => new PledgeAmountPage(context));

    public async Task<PledgeOrganisationNameOptionPage> GoToPledgeOrganisationNamePageOptionPage() =>
        await FollowLink("Show or hide your organisation's name", () => new PledgeOrganisationNameOptionPage(context));

    public async Task<AddtheLocationPage> GoToAddtheLocationPage() =>
        await FollowLink(LocationLink, () => new AddtheLocationPage(context));

    public async Task<ChoosetheSectorsPage> GoToChoosetheSectorPage() =>
        await FollowLink(SectorLink, () => new ChoosetheSectorsPage(context));

    public async Task<ChooseTheTypesOfJobPage> GoToChooseTheTypesOfJobPage() =>
        await FollowLink(TypeOfJobRoleLink, () => new ChooseTheTypesOfJobPage(context));

    public async Task<ChooseTheLevelPage> GoToChooseTheLevelPage() =>
        await FollowLink(LevelLink, () => new ChooseTheLevelPage(context));

    public async Task<Pledge100PercentMatchPage> GoToPledge100PercentMatchPage() =>
        await FollowLink("Review or approve immediately", () => new Pledge100PercentMatchPage(context));

    public async Task<PledgeVerificationPage> ContinueToPledgeVerificationPage()
    {
        objectContext.SetPledgeCreatedOn(DateTime.UtcNow);
        await Continue();
        return await VerifyPageAsync(() => new PledgeVerificationPage(context));
    }

    public async Task<string> GetAmount(string key) => await GetValue(key, 0);
    public async Task<string> GetCriteriaValue(string key) => await GetValue(key, 2);

    private async Task<string> GetValue(string key, int tablePosition) =>
        await page.Locator(".das-task-list__items").Nth(tablePosition)
            .Locator(".das-task-list__item").Filter(new() { HasText = key })
            .Locator(".das-task-list__task-tag").InnerTextAsync();
}
