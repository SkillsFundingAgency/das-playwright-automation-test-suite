﻿using SFA.DAS.Framework;

namespace SFA.DAS.Registration.UITests.Project.Pages;

public abstract class RegistrationBasePage(ScenarioContext context) : BasePage(context)
{
    #region Helpers and Context
    protected readonly RegistrationDataHelper registrationDataHelper = context.GetValue<RegistrationDataHelper>();
    #endregion

    #region Locators

    private static By SignOutLink => By.LinkText("Sign out");

    #endregion

    public HomePage GoToHomePage() => new(context, true);

    public HomePage ClickBackLink()
    {
        NavigateBack();
        return new HomePage(context);
    }

    public YouveLoggedOutPage SignOut()
    {
        formCompletionHelper.Click(SignOutLink);
        return new YouveLoggedOutPage(context);
    }
}
