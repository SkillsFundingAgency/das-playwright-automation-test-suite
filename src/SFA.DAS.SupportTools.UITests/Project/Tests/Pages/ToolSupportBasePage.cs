using System.Linq;

namespace SFA.DAS.SupportTools.UITests.Project.Tests.Pages;

public abstract class ToolSupportBasePage(ScenarioContext context) : BasePage(context)
{
    protected static async Task<List<string>> GetStatusColumn(ILocator locator)
    {
        var result = await locator.AllTextContentsAsync();

        return result.Select(x => RegexHelper.ReplaceMultipleSpace(x).Trim()).ToList();
    }
}
