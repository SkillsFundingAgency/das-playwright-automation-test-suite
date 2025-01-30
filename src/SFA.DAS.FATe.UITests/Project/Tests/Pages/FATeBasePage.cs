namespace SFA.DAS.FATe.UITests.Project.Tests.Pages;

public abstract class FATeBasePage(ScenarioContext context) : BasePage(context)
{
    protected readonly FATeDataHelper fateDataHelper = context.Get<FATeDataHelper>();
}
