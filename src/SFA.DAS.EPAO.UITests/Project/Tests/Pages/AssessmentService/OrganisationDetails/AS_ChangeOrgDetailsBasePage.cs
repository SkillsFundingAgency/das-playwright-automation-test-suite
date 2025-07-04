

namespace SFA.DAS.EPAO.UITests.Project.Tests.Pages.AssessmentService.OrganisationDetails;

public abstract class AS_ChangeOrgDetailsBasePage(ScenarioContext context) : EPAO_BasePage(context)
{
    public async Task<AS_OrganisationDetailsPage> ClickViewOrganisationDetailsLink()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "View organisation details" }).ClickAsync();

        return await VerifyPageAsync(() => new AS_OrganisationDetailsPage(context));
    }
}
