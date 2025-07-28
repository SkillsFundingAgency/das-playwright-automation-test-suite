namespace SFA.DAS.ApprenticeAmbassadorNetwork.UITests.Project.Tests.Pages.AppEmpCommonPages;

public abstract class AppEmpCommonBasePage(ScenarioContext context) : AanBasePage(context)
{
    protected async Task GoToId(string id)
    {
        var url = page.Url;

        var guid = url.Split('/').ToList().Single(x => x.Count(c => c == '-') == 4);

        var newUrl = url.Replace(guid, id);

        await Navigate(newUrl);
    }
}