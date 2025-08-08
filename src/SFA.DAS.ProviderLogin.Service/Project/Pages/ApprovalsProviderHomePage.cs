namespace SFA.DAS.ProviderLogin.Service.Project.Pages;

public partial class ProviderHomePage : InterimProviderBasePage
{
    public async Task<ProviderLandingPage> SignsOut()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Sign out" }).ClickAsync();

        return await VerifyPageAsync(() => new ProviderLandingPage(context));
    }

    public async Task<ProviderNotificationSettingsPage> GoToProviderNotificationSettingsPage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Settings" }).ClickAsync();

        await page.GetByRole(AriaRole.Link, new() { Name = "Notification settings" }).ClickAsync();

        return await VerifyPageAsync(() => new ProviderNotificationSettingsPage(context));
    }

    public async Task<ProviderEmployersAndPermissionsPage> GoToProviderEmployersAndPermissionsPagePage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "More" }).ClickAsync();

        await page.Locator("a.das-navigation__link", new PageLocatorOptions { HasTextString = "View employers and manage permissions" }).ClickAsync();

        return await VerifyPageAsync(() => new ProviderEmployersAndPermissionsPage(context));
    }

    public async Task<ManageApprenticeshipsServiceHelpPage> GoToManageApprenticeshipsServiceHelpPage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Help" }).ClickAsync();

        return await VerifyPageAsync(() => new ManageApprenticeshipsServiceHelpPage(context));
    }

    public async Task VerifyProviderFooterFeedbackPage()
    {
        var page2 = await page.RunAndWaitForPopupAsync(async () =>
        {
            await page.GetByRole(AriaRole.Link, new() { Name = "Give feedback" }).ClickAsync();
        });

        await Assertions.Expect(page2.Locator("legend")).ToContainTextAsync("Which of the below describes you?");

        await page2.CloseAsync();
    }

    public async Task<ProviderPrivacyPage> GoToProviderFooterPrivacyPage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Privacy" }).ClickAsync();

        return await VerifyPageAsync(() => new ProviderPrivacyPage(context));
    }

    public async Task<ProviderCookiesPage> GoToProviderFooterCookiesPage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Cookies", Exact = true }).ClickAsync();

        return await VerifyPageAsync(() => new ProviderCookiesPage(context));
    }

    public async Task<ProviderTermsOfUsePage> GoToProviderFooterTermsOfUsePage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Terms of use" }).ClickAsync();

        return await VerifyPageAsync(() => new ProviderTermsOfUsePage(context));
    }

    public async Task<ProviderAddApprenticeDetailsEntryMothod> GotoSelectJourneyPage()
    {
        await AddNewApprentices();

        return await VerifyPageAsync(() => new ProviderAddApprenticeDetailsEntryMothod(context));
    }

    public async Task<ProviderAddEmployerStartPage> GotoAddNewEmployerStartPage()
    {
        await ClickAddAnEmployerLink();

        return await VerifyPageAsync(() => new ProviderAddEmployerStartPage(context));
    }

    public async Task<ProviderReserveFundingForNonLevyEmployersPage> GoToProviderGetFunding()
    {
        await ClickFundingLink();

        return await VerifyPageAsync(() => new ProviderReserveFundingForNonLevyEmployersPage(context));
    }

    public async Task<ProviderApprenticeRequestsPage> GoToApprenticeRequestsPage()
    {
        await page.Locator("#navigation").GetByRole(AriaRole.Link, new() { Name = "Apprentice requests" }).ClickAsync();

        return await VerifyPageAsync(() => new ProviderApprenticeRequestsPage(context));
    }

    public async Task<ProviderFundingForNonLevyEmployersPage> GoToManageYourFunding()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Manage your funding reserved" }).ClickAsync();

        return await VerifyPageAsync(() => new ProviderFundingForNonLevyEmployersPage(context));
    }

    public async Task<ProviderManageYourApprenticesPage>  GoToProviderManageYourApprenticePage()
    {
        await page.GetByRole(AriaRole.Heading, new() { Name = "Manage your apprentices" }).GetByRole(AriaRole.Link).ClickAsync();

        return await VerifyPageAsync(() => new ProviderManageYourApprenticesPage(context));
    }

    public async Task<ProviderRecruitApprenticesHomePage> GoToProviderRecruitApprenticesHomePage()
    {
        await page.GetByRole(AriaRole.Heading, new() { Name = "Recruit apprentices" }).GetByRole(AriaRole.Link).ClickAsync();

        return await VerifyPageAsync(() => new ProviderRecruitApprenticesHomePage(context));
    }

    public async Task<ProviderYourStandardsAndTrainingVenuesPage> NavigateToYourStandardsAndTrainingVenuesPage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Your standards and training" }).ClickAsync();

        return await VerifyPageAsync(() => new ProviderYourStandardsAndTrainingVenuesPage(context));
    }

    public async Task<ProviderInformationNotFoundPage> NavigateToShutterPage_EmployerTypeProviderPage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Your standards and training" }).ClickAsync();

        return await VerifyPageAsync(() => new ProviderInformationNotFoundPage(context));
    }

    public async Task<ProviderAPIListPage>  NavigateToDeveloperAPIsPage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Developer APIs" }).ClickAsync();

        return await VerifyPageAsync(() => new ProviderAPIListPage(context));
    }

    public async Task<ProviderYourFeebackPage>  NavigateToYourFeedback()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Your feedback" }).ClickAsync();

        return await VerifyPageAsync(() => new ProviderYourFeebackPage(context));
    }

    public async Task<ProviderViewEmployerRequestsForTrainingPage> NavigateToViewEmployerRequestsForTrainingPage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "View employer requests for" }).ClickAsync();

        return await VerifyPageAsync(() => new ProviderViewEmployerRequestsForTrainingPage(context));
    }  

    private async Task AddNewApprentices() => await page.GetByRole(AriaRole.Link, new() { Name = "Add new apprentices" }).ClickAsync();
}

public class ProviderYourFeebackPage(ScenarioContext context) : InterimProviderBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("#your-feedback")).ToContainTextAsync("Your feedback");
    }
}

public class ProviderViewEmployerRequestsForTrainingPage(ScenarioContext context) : InterimProviderBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Employer requests for apprenticeship training");
    }
}

public class ProviderAPIListPage(ScenarioContext context) : InterimProviderBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("API list");
    }
}

public class ProviderRecruitApprenticesHomePage(ScenarioContext context) : InterimProviderBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Recruitment");
    }
}

public class ProviderYourStandardsAndTrainingVenuesPage(ScenarioContext context) : InterimProviderBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Your standards and training venues");
    }
}

public class ProviderInformationNotFoundPage(ScenarioContext context) : InterimProviderBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Provider information not found on course management");
    }
}


public class ProviderManageYourApprenticesPage(ScenarioContext context) : InterimProviderBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Manage your apprentices");
    }
}

public class ProviderApprenticeRequestsPage(ScenarioContext context) : InterimProviderBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Apprentice requests");
    }

    public async Task SelectCohort(string cohortReference)
    {
        var linkId = $"#details_link_{cohortReference}";
        await page.Locator(linkId).ClickAsync();        
    }
}

public class ProviderFundingForNonLevyEmployersPage(ScenarioContext context) : InterimProviderBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Funding for non-levy employers");
    }
}

public class ProviderReserveFundingForNonLevyEmployersPage(ScenarioContext context) : InterimProviderBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Reserve funding for non-levy employers");
    }

    //private static By ReserveFundingButton => By.LinkText("Reserve funding");

    //protected override By AcceptCookieButton => By.CssSelector(".govuk-button");

    //internal ProviderChooseAnEmployerNonLevyPage StartReservedFunding()
    //{
    //    AcceptCookies();
    //    formCompletionHelper.ClickElement(ReserveFundingButton);
    //    return new ProviderChooseAnEmployerNonLevyPage(context);
    //}
    //public ProviderFundingForNonLevyEmployersPage NavigateBrowserBackToProviderFundingForNonLevyEmployersPage()
    //{
    //    tabHelper.NavigateBrowserBack();
    //    return new ProviderFundingForNonLevyEmployersPage(context);
    //}
}

public class ProviderAddEmployerStartPage(ScenarioContext context) : InterimProviderBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Add an employer");
    }
}

public class ApimAccessDeniedPage(ScenarioContext context) : InterimProviderBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("To continue, you'll need to get your role for this service changed.");

    }
    public async Task<ProviderHomePage> GoBackToTheServiceHomePage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "homepage of this service." }).ClickAsync();

        return await VerifyPageAsync(() => new ProviderHomePage(context));
    }
}


public class ProviderAccessDeniedPage(ScenarioContext context) : InterimProviderBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("You need a different service role to view this page");
    }

    public async Task<ProviderHomePage> GoBackToTheServiceHomePage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "homepage of this service." }).ClickAsync();

        return await VerifyPageAsync(() => new ProviderHomePage(context));
    }

    //public async Task<ProviderFundingForNonLevyEmployersPage>  NavigateBrowserBackToProviderFundingForNonLevyEmployersPage()
    //{
    //    await NavigateBrowserBack();

    //    return await VerifyPageAsync(() => new ProviderFundingForNonLevyEmployersPage(context));
    //}
}

public class ProviderAddApprenticeDetailsEntryMothod(ScenarioContext context) : InterimProviderBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Add apprentice details");
    }

    //internal async Task<ProviderAddApprenticeDetailsViaSelectJourneyPage>  SelectAddManually()
    //{
    //    await page.GetByRole(AriaRole.Radio, new() { Name = "Upload a CSV file" }).CheckAsync();

    //    await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

    //    return await VerifyPageAsync(() => new ProviderAddApprenticeDetailsViaSelectJourneyPage(context));
    //}

    //internal async Task<ProviderBeforeYouStartBulkUploadPage> SelectBulkUpload()
    //{
    //    await page.GetByRole(AriaRole.Radio, new() { Name = "Manually" }).CheckAsync();

    //    await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

    //    return await VerifyPageAsync(() => new ProviderBeforeYouStartBulkUploadPage(context));
    //}
}

public class ProviderTermsOfUsePage(ScenarioContext context) : InterimProviderBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Terms of use");
    }
}

public class ProviderCookiesPage(ScenarioContext context) : InterimProviderBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Cookies");
    }
}


public class ProviderPrivacyPage(ScenarioContext context) : InterimProviderBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Privacy");
    }
}

public class ManageApprenticeshipsServiceHelpPage(ScenarioContext context) : InterimProviderBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Manage apprenticeships service help");
    }
}

public class ProviderEmployersAndPermissionsPage(ScenarioContext context) : InterimProviderBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("View employers and manage permissions");
    }

}

public class ProviderNotificationSettingsPage(ScenarioContext context) : InterimProviderBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Notification settings");


    //private static By NotificationOptions => By.CssSelector(".selection-button-radio");

    //private static By Alert => By.CssSelector(".green-box-alert");

    //public ProviderNotificationSettingsPage ChooseToReceiveEmails() => SelectReceiveEmailsOptions("NotificationSettings-true-0");

    //public ProviderNotificationSettingsPage ChooseNotToReceiveEmails() => SelectReceiveEmailsOptions("NotificationSettings-false-0");

    //public bool IsSettingsUpdated() => pageInteractionHelper.IsElementDisplayed(Alert);

    //private ProviderNotificationSettingsPage SelectReceiveEmailsOptions(string option)
    //{
    //    formCompletionHelper.SelectRadioOptionByForAttribute(NotificationOptions, option);
    //    Continue();
    //    return this;
    //}

    public async Task<ProviderHomePage> ClickCancel()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Cancel" }).ClickAsync();

        return await VerifyPageAsync(() => new ProviderHomePage(context));
    } 

}