﻿//namespace SFA.DAS.EPAO.UITests.Project.Tests.Pages.AssessmentService.OrganisationDetails;

//public class AS_ChangePhoneNumberPage : EPAO_BasePage
//{
//    protected override string PageTitle => "Change phone number";

//    #region Locators
//    private static By PhoneNumberTextBox => By.CssSelector(".govuk-input");
//    #endregion

//    public AS_ChangePhoneNumberPage(ScenarioContext context) : base(context) => VerifyPage();

//    public AS_ConfirmPhoneNumberPage EnterRandomPhoneNumberAndClickUpdate()
//    {
//        formCompletionHelper.EnterText(PhoneNumberTextBox, Helpers.DataHelpers.EPAODataHelper.GetRandomNumber(10));
//        Continue();
//        return new(context);
//    }
//}

//public class AS_ConfirmPhoneNumberPage : EPAO_BasePage
//{
//    protected override string PageTitle => "Confirm phone number";

//    public AS_ConfirmPhoneNumberPage(ScenarioContext context) : base(context) => VerifyPage();

//    public AS_ContactPhoneNumberUpdatedPage ClickConfirmButtonInConfirmPhoneNumberPage()
//    {
//        Continue();
//        return new(context);
//    }
//}

//public class AS_ContactPhoneNumberUpdatedPage : AS_ChangeOrgDetailsBasePage
//{
//    protected override string PageTitle => "Contact phone number updated";

//    public AS_ContactPhoneNumberUpdatedPage(ScenarioContext context) : base(context) => VerifyPage();
//}
