namespace SFA.DAS.FAA.UITests.Project.Tests.Pages;

public class FAA_CreateAccountPage(ScenarioContext context) : FAABasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Create an account for Find an apprenticeship");

    public async Task<WhatIsYourNamePage> CreateAnAccount()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new WhatIsYourNamePage(context));
    }
}

public class WhatIsYourNamePage(ScenarioContext context) : FAABasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("What is your name?");

    public async Task<DateOfBirthPage> SubmitApprenticeName()
    {
        await page.GetByRole(AriaRole.Textbox, new() { Name = "First name" }).FillAsync(fAAUserNameDataHelper.FirstName);

        await page.GetByRole(AriaRole.Textbox, new() { Name = "Last name" }).FillAsync(fAAUserNameDataHelper.LastName);

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new DateOfBirthPage(context));
    }
}

public class DateOfBirthPage(ScenarioContext context) : FAABasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Date of birth");

    public async Task<WhatIsYourAddressPage> SubmitApprenticeDateOfBirth()
    {
        var dob = fAAUserNameDataHelper.FaaNewUserDob;

        await page.GetByRole(AriaRole.Textbox, new() { Name = "Day" }).FillAsync($"{dob.Day}");

        await page.GetByRole(AriaRole.Textbox, new() { Name = "Month" }).FillAsync($"{dob.Month}");

        await page.GetByRole(AriaRole.Textbox, new() { Name = "Year" }).FillAsync($"{dob.Year}");

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new WhatIsYourAddressPage(context));
    }
}

public class WhatIsYourAddressPage(ScenarioContext context) : FAABasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("What is your address?");

    private static string Address => ("#SelectedAddress");

    public async Task<WhatIsYourTelephoneNumberPage> SubmitApprenticePostCode()
    {
        await page.GetByRole(AriaRole.Textbox, new() { Name = "UK postcode" }).FillAsync(fAAUserNameDataHelper.FaaNewUserPostCode);

        await page.GetByRole(AriaRole.Button, new() { Name = "Find my address" }).ClickAsync();

        var options = await page.Locator(Address).AllTextContentsAsync();

        var allOptions = options.ToList().Where(x => x != "Select address").ToList();

        var option = RandomDataGenerator.GetRandomElementFromListOfElements(allOptions);

        await page.GetByLabel("Select an address from the").SelectOptionAsync(option);

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new WhatIsYourTelephoneNumberPage(context));
    }
}

public class WhatIsYourTelephoneNumberPage(ScenarioContext context) : FAABasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("What is your telephone number?");

    public async Task<GetRemindersAboutYourUnfinishedApplicationsPage> SubmitApprenticeTelephoneNumber()
    {
        await page.GetByRole(AriaRole.Textbox, new() { Name = "Telephone number" }).FillAsync($"{fAAUserNameDataHelper.FaaNewUserMobilePhone}");

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new GetRemindersAboutYourUnfinishedApplicationsPage(context));
    }
}

public class GetRemindersAboutYourUnfinishedApplicationsPage(ScenarioContext context) : FAABasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("#main-content")).ToContainTextAsync("Get reminders about your unfinished applications");

    public async Task<CheckYourAccountDetailsPage> SelectRemindersNotification()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "No" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new CheckYourAccountDetailsPage(context));
    }
}

public class CheckYourAccountDetailsPage(ScenarioContext context) : FAABasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Check your account details");

    public async Task<FAASearchApprenticeLandingPage> ClickCreateYourAccountConfirmation()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Create your account" }).ClickAsync();

        return await VerifyPageAsync(() => new FAASearchApprenticeLandingPage(context));
    }
}