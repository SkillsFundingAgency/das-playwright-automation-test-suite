﻿using SFA.DAS.Framework;

namespace SFA.DAS.EPAO.UITests.Project.Tests.Pages;

public abstract class EPAO_BasePage(ScenarioContext context) : BasePage(context)
{
    protected readonly EPAOApplyDataHelper ePAOApplyDataHelper = context.Get<EPAOApplyDataHelper>();
    protected readonly EPAOAssesmentServiceDataHelper ePAOAssesmentServiceDataHelper = context.Get<EPAOAssesmentServiceDataHelper>();
    protected readonly EPAOApplyStandardDataHelper standardDataHelper = context.Get<EPAOApplyStandardDataHelper>();
    protected readonly EPAOAdminDataHelper ePAOAdminDataHelper = context.Get<EPAOAdminDataHelper>();

    //protected override By PageHeader => By.CssSelector(".govuk-heading-xl, .heading-xlarge, .govuk-heading-l, .govuk-panel__title, .govuk-fieldset__heading, .govuk-label--xl");

    //protected override By ContinueButton => By.CssSelector("#main-content .govuk-button");

    //protected override By AcceptCookieButton => By.CssSelector(".das-cookie-banner__button-accept");

    //private static By ChooseFile => By.ClassName("govuk-file-upload");

    //private static By SummaryRows => By.CssSelector(".govuk-summary-list__row");

    public async Task VerifyGrade(string grade) => await Assertions.Expect(page.Locator("dl")).ToContainTextAsync(grade, new LocatorAssertionsToContainTextOptions { IgnoreCase = true});

    protected void UploadFile()
    {
        string File = AppDomain.CurrentDomain.BaseDirectory + FrameworkConfig.SampleFileName;
        //formCompletionHelper.EnterText(ChooseFile, File);
        //Continue();
    }

    //protected void ClickRandomElement(By locator) => formCompletionHelper.ClickElement(() => RandomDataGenerator.GetRandomElementFromListOfElements(pageInteractionHelper.FindElements(locator)));
}
