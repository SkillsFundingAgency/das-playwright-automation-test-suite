namespace SFA.DAS.QFAST.UITests.Project.Tests.Pages.Application;

public class DfeFundigReview_Page(ScenarioContext context) : BasePage(context)
{
    public override async Task VerifyPage()
    {
        var reviewHeading = page.GetByRole(AriaRole.Heading, new() { Name = "DfE funding review" });
        var summaryHeading = page.GetByRole(AriaRole.Heading, new() { Name = "Offers funding summary" });
        if (await reviewHeading.IsVisibleAsync())
            return;
        if (await summaryHeading.IsVisibleAsync())
            return;
        throw new Exception("Neither the DfE funding review page nor the Offers funding summary page is displayed.");
    }
    public async Task<DfeFundigReview_Page> ApproveTheApplcaiton()
    {
        await page.Locator("#Approved").ClickAsync();
        await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();
        return await VerifyPageAsync(() => new DfeFundigReview_Page(context));
    }
    public async Task ApproveTheQualification()
    {
        var approvedRadio = page.Locator("#Approved");
        if (!await approvedRadio.IsVisibleAsync())
        {          
            await page.GetByRole(AriaRole.Link, new() { Name = "Change" }).First.ClickAsync(); 
            await approvedRadio.WaitForAsync();
        }
        await approvedRadio.ClickAsync();
        await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();        
    }
    public async Task<DfeFundigReview_Page>RejectTheApplcaiton()
    {
        await page.Locator("#Approved-2").ClickAsync();
        await page.GetByRole(AriaRole.Button, new() { Name = "Confirm" }).ClickAsync();
        return await VerifyPageAsync(() => new DfeFundigReview_Page(context));
    }
    public async Task<DfeFundigReview_Page> SelectFundingStream()
    {
        await page.Locator("#SelectedOfferIds").ClickAsync();
        await page.Locator("#SelectedOfferIds-2").ClickAsync();
        await page.Locator("#SelectedOfferIds-3").ClickAsync();
        await page.Locator("#SelectedOfferIds-4").ClickAsync();
        await page.Locator("#SelectedOfferIds-5").ClickAsync();
        await page.Locator("#SelectedOfferIds-6").ClickAsync();
        await page.Locator("#SelectedOfferIds-7").ClickAsync();
        await page.Locator("#SelectedOfferIds-8").ClickAsync();
        await page.Locator("#SelectedOfferIds-9").ClickAsync();
        await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();
        return await VerifyPageAsync(() => new DfeFundigReview_Page(context));
    }
    public async Task<DfeFundigReview_Page> SelectFundingStreamForQualification()
    {
        var checkboxIds = new[]
    {
        "#SelectedOfferIds",
        "#SelectedOfferIds-2",
        "#SelectedOfferIds-3",
        "#SelectedOfferIds-4",
        "#SelectedOfferIds-5",
        "#SelectedOfferIds-6",
        "#SelectedOfferIds-7",
        "#SelectedOfferIds-8",
        "#SelectedOfferIds-9"
    };
        foreach (var id in checkboxIds)
        {
            var checkbox = page.Locator(id);
            if (!await checkbox.IsCheckedAsync())
            {
                await checkbox.CheckAsync();
            }
        }
        await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();
        return await VerifyPageAsync(() => new DfeFundigReview_Page(context));
    }
    public async Task SetFundingStreamsAndApprovedTheApplication()
    {
        var now = DateTime.Now;
        int presentYear = now.Year;
        int nextYear = now.Year + 1;

        // LifelongLearningEntitlement
        await page.Locator("[name='Details[0][StartDate.Day]']").FillAsync("01");
        await page.Locator("[name='Details[0][StartDate.Month]']").FillAsync("08");
        await page.Locator("[name='Details[0][StartDate.Year]']").FillAsync(presentYear.ToString());
        
        await page.Locator("[name='Details[0][EndDate.Day]']").FillAsync("31");
        await page.Locator("[name='Details[0][EndDate.Month]']").FillAsync("07");
        await page.Locator("[name='Details[0][EndDate.Year]']").FillAsync(nextYear.ToString());

        //LegalEntitlementEnglishandMaths
        await page.Locator("[name='Details[1][StartDate.Day]']").FillAsync("01");
        await page.Locator("[name='Details[1][StartDate.Month]']").FillAsync("08");
        await page.Locator("[name='Details[1][StartDate.Year]']").FillAsync(presentYear.ToString());

        await page.Locator("[name='Details[1][EndDate.Day]']").FillAsync("31");
        await page.Locator("[name='Details[1][EndDate.Month]']").FillAsync("07");
        await page.Locator("[name='Details[1][EndDate.Year]']").FillAsync(nextYear.ToString());

        //LocalFlexibilities
        await page.Locator("[name='Details[2][StartDate.Day]']").FillAsync("01");
        await page.Locator("[name='Details[2][StartDate.Month]']").FillAsync("08");
        await page.Locator("[name='Details[2][StartDate.Year]']").FillAsync(presentYear.ToString());

        await page.Locator("[name='Details[2][EndDate.Day]']").FillAsync("31");
        await page.Locator("[name='Details[2][EndDate.Month]']").FillAsync("07");
        await page.Locator("[name='Details[2][EndDate.Year]']").FillAsync(nextYear.ToString());

        //AdvancedLearnerLoans
        await page.Locator("[name='Details[3][StartDate.Day]']").FillAsync("01");
        await page.Locator("[name='Details[3][StartDate.Month]']").FillAsync("08");
        await page.Locator("[name='Details[3][StartDate.Year]']").FillAsync(presentYear.ToString());

        await page.Locator("[name='Details[3][EndDate.Day]']").FillAsync("31");
        await page.Locator("[name='Details[3][EndDate.Month]']").FillAsync("07");
        await page.Locator("[name='Details[3][EndDate.Year]']").FillAsync(nextYear.ToString());

        //L3FreeCoursesForJobs
        await page.Locator("[name='Details[4][StartDate.Day]']").FillAsync("01");
        await page.Locator("[name='Details[4][StartDate.Month]']").FillAsync("08");
        await page.Locator("[name='Details[4][StartDate.Year]']").FillAsync(presentYear.ToString());

        await page.Locator("[name='Details[4][EndDate.Day]']").FillAsync("31");
        await page.Locator("[name='Details[4][EndDate.Month]']").FillAsync("07");
        await page.Locator("[name='Details[4][EndDate.Year]']").FillAsync(nextYear.ToString());

        //Age1416
        await page.Locator("[name='Details[5][StartDate.Day]']").FillAsync("01");
        await page.Locator("[name='Details[5][StartDate.Month]']").FillAsync("08");
        await page.Locator("[name='Details[5][StartDate.Year]']").FillAsync(presentYear.ToString());

        await page.Locator("[name='Details[5][EndDate.Day]']").FillAsync("31");
        await page.Locator("[name='Details[5][EndDate.Month]']").FillAsync("07");
        await page.Locator("[name='Details[5][EndDate.Year]']").FillAsync(nextYear.ToString());

        //Age1619
        await page.Locator("[name='Details[6][StartDate.Day]']").FillAsync("01");
        await page.Locator("[name='Details[6][StartDate.Month]']").FillAsync("08");
        await page.Locator("[name='Details[6][StartDate.Year]']").FillAsync(presentYear.ToString());

        await page.Locator("[name='Details[6][EndDate.Day]']").FillAsync("31");
        await page.Locator("[name='Details[6][EndDate.Month]']").FillAsync("07");
        await page.Locator("[name='Details[6][EndDate.Year]']").FillAsync(nextYear.ToString());

        //DigitalEntitlement
        await page.Locator("[name='Details[7][StartDate.Day]']").FillAsync("01");
        await page.Locator("[name='Details[7][StartDate.Month]']").FillAsync("08");
        await page.Locator("[name='Details[7][StartDate.Year]']").FillAsync(presentYear.ToString());

        await page.Locator("[name='Details[7][EndDate.Day]']").FillAsync("31");
        await page.Locator("[name='Details[7][EndDate.Month]']").FillAsync("07");
        await page.Locator("[name='Details[7][EndDate.Year]']").FillAsync(nextYear.ToString());

        //LegalEntitlementL2L3
        await page.Locator("[name='Details[8][StartDate.Day]']").FillAsync("01");
        await page.Locator("[name='Details[8][StartDate.Month]']").FillAsync("08");
        await page.Locator("[name='Details[8][StartDate.Year]']").FillAsync(presentYear.ToString());

        await page.Locator("[name='Details[8][EndDate.Day]']").FillAsync("31");
        await page.Locator("[name='Details[8][EndDate.Month]']").FillAsync("07");
        await page.Locator("[name='Details[8][EndDate.Year]']").FillAsync(nextYear.ToString());

        await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();
        await page.GetByRole(AriaRole.Link, new() { Name = "Confirm" }).ClickAsync();
        await page.GetByRole(AriaRole.Link, new() { Name = "Application details" }).ClickAsync();
        await page.GetByRole(AriaRole.Link, new() { Name = "Application funding decision" }).ClickAsync();
        await page.GetByRole(AriaRole.Button, new() { Name = "Inform AO of decision" }).ClickAsync();
    }
    public async Task SetFundingStreamsAndApprovedTheQualification()
    {
        var now = DateTime.Now;
        int presentYear = now.Year;
        int nextYear = now.Year + 1;

        // LifelongLearningEntitlement
        await page.Locator("[name='Details[0][StartDate.Day]']").FillAsync("01");
        await page.Locator("[name='Details[0][StartDate.Month]']").FillAsync("08");
        await page.Locator("[name='Details[0][StartDate.Year]']").FillAsync(presentYear.ToString());

        await page.Locator("[name='Details[0][EndDate.Day]']").FillAsync("31");
        await page.Locator("[name='Details[0][EndDate.Month]']").FillAsync("07");
        await page.Locator("[name='Details[0][EndDate.Year]']").FillAsync(nextYear.ToString());

        //LegalEntitlementEnglishandMaths
        await page.Locator("[name='Details[1][StartDate.Day]']").FillAsync("01");
        await page.Locator("[name='Details[1][StartDate.Month]']").FillAsync("08");
        await page.Locator("[name='Details[1][StartDate.Year]']").FillAsync(presentYear.ToString());

        await page.Locator("[name='Details[1][EndDate.Day]']").FillAsync("31");
        await page.Locator("[name='Details[1][EndDate.Month]']").FillAsync("07");
        await page.Locator("[name='Details[1][EndDate.Year]']").FillAsync(nextYear.ToString());

        //LocalFlexibilities
        await page.Locator("[name='Details[2][StartDate.Day]']").FillAsync("01");
        await page.Locator("[name='Details[2][StartDate.Month]']").FillAsync("08");
        await page.Locator("[name='Details[2][StartDate.Year]']").FillAsync(presentYear.ToString());

        await page.Locator("[name='Details[2][EndDate.Day]']").FillAsync("31");
        await page.Locator("[name='Details[2][EndDate.Month]']").FillAsync("07");
        await page.Locator("[name='Details[2][EndDate.Year]']").FillAsync(nextYear.ToString());

        //AdvancedLearnerLoans
        await page.Locator("[name='Details[3][StartDate.Day]']").FillAsync("01");
        await page.Locator("[name='Details[3][StartDate.Month]']").FillAsync("08");
        await page.Locator("[name='Details[3][StartDate.Year]']").FillAsync(presentYear.ToString());

        await page.Locator("[name='Details[3][EndDate.Day]']").FillAsync("31");
        await page.Locator("[name='Details[3][EndDate.Month]']").FillAsync("07");
        await page.Locator("[name='Details[3][EndDate.Year]']").FillAsync(nextYear.ToString());

        //L3FreeCoursesForJobs
        await page.Locator("[name='Details[4][StartDate.Day]']").FillAsync("01");
        await page.Locator("[name='Details[4][StartDate.Month]']").FillAsync("08");
        await page.Locator("[name='Details[4][StartDate.Year]']").FillAsync(presentYear.ToString());

        await page.Locator("[name='Details[4][EndDate.Day]']").FillAsync("31");
        await page.Locator("[name='Details[4][EndDate.Month]']").FillAsync("07");
        await page.Locator("[name='Details[4][EndDate.Year]']").FillAsync(nextYear.ToString());

        //Age1416
        await page.Locator("[name='Details[5][StartDate.Day]']").FillAsync("01");
        await page.Locator("[name='Details[5][StartDate.Month]']").FillAsync("08");
        await page.Locator("[name='Details[5][StartDate.Year]']").FillAsync(presentYear.ToString());

        await page.Locator("[name='Details[5][EndDate.Day]']").FillAsync("31");
        await page.Locator("[name='Details[5][EndDate.Month]']").FillAsync("07");
        await page.Locator("[name='Details[5][EndDate.Year]']").FillAsync(nextYear.ToString());

        //Age1619
        await page.Locator("[name='Details[6][StartDate.Day]']").FillAsync("01");
        await page.Locator("[name='Details[6][StartDate.Month]']").FillAsync("08");
        await page.Locator("[name='Details[6][StartDate.Year]']").FillAsync(presentYear.ToString());

        await page.Locator("[name='Details[6][EndDate.Day]']").FillAsync("31");
        await page.Locator("[name='Details[6][EndDate.Month]']").FillAsync("07");
        await page.Locator("[name='Details[6][EndDate.Year]']").FillAsync(nextYear.ToString());

        //DigitalEntitlement
        await page.Locator("[name='Details[7][StartDate.Day]']").FillAsync("01");
        await page.Locator("[name='Details[7][StartDate.Month]']").FillAsync("08");
        await page.Locator("[name='Details[7][StartDate.Year]']").FillAsync(presentYear.ToString());

        await page.Locator("[name='Details[7][EndDate.Day]']").FillAsync("31");
        await page.Locator("[name='Details[7][EndDate.Month]']").FillAsync("07");
        await page.Locator("[name='Details[7][EndDate.Year]']").FillAsync(nextYear.ToString());

        //LegalEntitlementL2L3
        await page.Locator("[name='Details[8][StartDate.Day]']").FillAsync("01");
        await page.Locator("[name='Details[8][StartDate.Month]']").FillAsync("08");
        await page.Locator("[name='Details[8][StartDate.Year]']").FillAsync(presentYear.ToString());

        await page.Locator("[name='Details[8][EndDate.Day]']").FillAsync("31");
        await page.Locator("[name='Details[8][EndDate.Month]']").FillAsync("07");
        await page.Locator("[name='Details[8][EndDate.Year]']").FillAsync(nextYear.ToString());

        await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();
        await page.GetByRole(AriaRole.Button, new() { Name = "Confirm" }).ClickAsync();
        await page.GetByRole(AriaRole.Link, new() { Name = "Qualification details" }).ClickAsync();        
    }
}