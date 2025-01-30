namespace SFA.DAS.SupportTools.UITests.Project.Tests.Pages;

public abstract class ToolSupportBasePage(ScenarioContext context) : BasePage(context)
{
    protected async Task ClickSelectAllCheckBox() => await page.GetByRole(AriaRole.Row, new() { Name = "Id Uln Cohort Ref First Name" }).GetByLabel("").CheckAsync();

}
