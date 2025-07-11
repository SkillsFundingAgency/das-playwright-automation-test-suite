using SFA.DAS.Framework;

namespace SFA.DAS.EPAO.UITests.Project.Tests.Pages;

public abstract class EPAO_BasePage(ScenarioContext context) : BasePage(context)
{
    protected readonly EPAOApplyDataHelper ePAOApplyDataHelper = context.Get<EPAOApplyDataHelper>();
    protected readonly EPAOAssesmentServiceDataHelper ePAOAssesmentServiceDataHelper = context.Get<EPAOAssesmentServiceDataHelper>();
    protected readonly EPAOApplyStandardDataHelper standardDataHelper = context.Get<EPAOApplyStandardDataHelper>();
    protected readonly EPAOAdminDataHelper ePAOAdminDataHelper = context.Get<EPAOAdminDataHelper>();

    public async Task VerifyGrade(string grade) => await Assertions.Expect(page.Locator("dl")).ToContainTextAsync(grade, new LocatorAssertionsToContainTextOptions { IgnoreCase = true});
}
