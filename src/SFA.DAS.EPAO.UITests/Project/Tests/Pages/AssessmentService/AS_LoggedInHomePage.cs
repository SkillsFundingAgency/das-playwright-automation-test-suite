﻿using SFA.DAS.EPAO.UITests.Project.Tests.Pages.AssessmentService.ManageUsers;
using SFA.DAS.EPAO.UITests.Project.Tests.Pages.EPAOWithdrawalPages;

namespace SFA.DAS.EPAO.UITests.Project.Tests.Pages.AssessmentService;

public class AS_LoggedInHomePage(ScenarioContext context) : EPAO_BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("#Home")).ToContainTextAsync("Home");

    public async Task<ApprovedStandardsAndVersionsLandingPage> ApprovedStandardAndVersions()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Approved standards and" }).ClickAsync();

        return await VerifyPageAsync(() => new ApprovedStandardsAndVersionsLandingPage(context));
    }

    public async Task<AS_RecordAGradePage> GoToRecordAGradePage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Record a grade", Exact = true }).First.ClickAsync();

        return await VerifyPageAsync(() => new AS_RecordAGradePage(context));
    }

    public async Task<AS_CompletedAssessmentsPage> ClickCompletedAssessmentsLink()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Show completed assessments" }).ClickAsync();

        return await VerifyPageAsync(() => new AS_CompletedAssessmentsPage(context));
    }

    public async Task ClickOrganisationDetailsTopMenuLink()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Organisation details", Exact = true }).ClickAsync();
    }

    public async Task<AS_UsersPage> ClickManageUsersLink()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Manage users" }).ClickAsync();

        return await VerifyPageAsync(() => new AS_UsersPage(context));
    }

    public async Task ClickHomeTopMenuLink()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Home" }).ClickAsync();
    }

    public async Task VerifySignedInUserName(string expectedText) => await Assertions.Expect(page.Locator("body")).ToContainTextAsync(expectedText);

    public async Task<AS_SignedOutPage> ClickSignOutLink()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Sign out" }).ClickAsync();

        return await VerifyPageAsync(() => new AS_SignedOutPage(context));
    }

    public async Task<AS_WithdrawFromAStandardsPage> ClickWithdrawFromAStandardLink()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Withdraw from a standard" }).ClickAsync();

        return await VerifyPageAsync(() => new AS_WithdrawFromAStandardsPage(context));
    }

    public async Task<AS_WithdrawFromAStandardsPage> ClickWithdrawFromTheRegisterLink()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Withdraw from all standards" }).ClickAsync();

        return await VerifyPageAsync(() => new AS_WithdrawFromAStandardsPage(context));
    }
}

public class AS_SignedOutPage(ScenarioContext context) : EPAO_BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("You have successfully signed out.");

    public async Task<StubSignInAssessorPage> ClickSignBackInLink()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Click here to sign back in" }).ClickAsync();

        return await VerifyPageAsync(() => new StubSignInAssessorPage(context));
    }
}


public class ApprovedStandardsAndVersionsLandingPage(ScenarioContext context) : EPAO_BasePage(context)
{

    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Approved standards and versions");

    public async Task<StandardDetailsForAssociateProjectManagerPage>  ClickOnAssociateProjectManagerLink()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Associate project manager" }).ClickAsync();

        return await VerifyPageAsync(() => new StandardDetailsForAssociateProjectManagerPage(context));
    }
}

public class StandardDetailsForAssociateProjectManagerPage(ScenarioContext context) : EPAO_BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Standard details");

    public async Task<ConfirmOptInForAssociateProjectManagerPage> ClickOnAssociateProjectManagerOptInLinkForVersion1_1()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Opt into standard version" }).ClickAsync();

        return await VerifyPageAsync(() => new ConfirmOptInForAssociateProjectManagerPage(context));
    }
}

public class ConfirmOptInForAssociateProjectManagerPage(ScenarioContext context) : EPAO_BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Confirm you want to opt in:");

    public async Task<OptInConfirmationPage> ConfirmOptIn()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Confirm and opt in" }).ClickAsync();

        return await VerifyPageAsync(() => new OptInConfirmationPage(context));
    }
}
public class OptInConfirmationPage(ScenarioContext context) : EPAO_BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("You've opted in to version");
}