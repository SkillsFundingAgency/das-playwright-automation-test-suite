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

    public async Task TriggerLastNameValidationError()
    {
        await page.GetByRole(AriaRole.Textbox, new() { Name = "Last name" }).FillAsync(string.Empty);

        await page.GetByRole(AriaRole.Button, new() { Name = "Save" }).ClickAsync();

        await CheckValidationErrors("Enter your last name");
    }

}

public class ChangeYourNamePage(ScenarioContext context) : FAABasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Change your name");

    public async Task TriggerLastNameValidationError()
    {
        await page.GetByRole(AriaRole.Textbox, new() { Name = "Last name" }).FillAsync(string.Empty);

        await page.GetByRole(AriaRole.Button, new() { Name = "Save" }).ClickAsync();

        await CheckValidationErrors("Enter your last name");
    }

    public async Task<SettingPage> SaveNameChange()
    {
        await page.GetByRole(AriaRole.Textbox, new() { Name = "First name" }).FillAsync(fAAUserNameDataHelper.FirstName);

        await page.GetByRole(AriaRole.Textbox, new() { Name = "Last name" }).FillAsync(fAAUserNameDataHelper.LastName);

        await page.GetByRole(AriaRole.Button, new() { Name = "Save" }).ClickAsync();

        return await VerifyPageAsync(() => new SettingPage(context));
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

    public class ChangeDateOfBirthPage(ScenarioContext context) : FAABasePage(context)
    {
        public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Change your date of birth");

        public async Task TriggerDateOfBirthValidationError()
        {
            await page.GetByRole(AriaRole.Textbox, new() { Name = "Day" }).FillAsync(string.Empty);

            await page.GetByRole(AriaRole.Button, new() { Name = "Save" }).ClickAsync();

            await CheckValidationErrors("Date of birth must include a day, month and year");
        }
    public async Task<SettingPage> SaveDateOfBirthChange()
        {
            var dob = fAAUserNameDataHelper.FaaNewUserDob;

            await page.GetByRole(AriaRole.Textbox, new() { Name = "Day" }).FillAsync($"{dob.Day}");

            await page.GetByRole(AriaRole.Textbox, new() { Name = "Month" }).FillAsync($"{dob.Month}");

            await page.GetByRole(AriaRole.Textbox, new() { Name = "Year" }).FillAsync($"{dob.Year}");

            await page.GetByRole(AriaRole.Button, new() { Name = "Save" }).ClickAsync();

            return await VerifyPageAsync(() => new SettingPage(context));
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

        var alloptionstexts = await AllTextAsync($"{Address} option");

        objectContext.SetDebugInformation($"Available address - {alloptionstexts.ToString(",")}");

        alloptionstexts = [.. alloptionstexts.ToList().Select(x => x.RemoveMultipleSpace())];

        objectContext.SetDebugInformation($"Available address - {alloptionstexts.ToString(",")}");

        var allOptions = alloptionstexts.Where(x => x != "Select address").ToList();

        var option = RandomDataGenerator.GetRandomElementFromListOfElements(allOptions);

        objectContext.SetDebugInformation($"Selection address - {option}");

        await page.GetByLabel("Select an address from the").SelectOptionAsync(option);

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new WhatIsYourTelephoneNumberPage(context));
    }

    public async Task<SettingPage> SaveAddressChange()
    {
        await page.GetByRole(AriaRole.Textbox, new() { Name = "UK postcode" }).FillAsync(fAAUserNameDataHelper.FaaNewUserPostCode);

        await page.GetByRole(AriaRole.Button, new() { Name = "Find my address" }).ClickAsync();

        var alloptionstexts = await AllTextAsync($"{Address} option");

        alloptionstexts = [.. alloptionstexts.ToList().Select(x => x.RemoveMultipleSpace())];

        var allOptions = alloptionstexts.Where(x => x != "Select address").ToList();

        var option = RandomDataGenerator.GetRandomElementFromListOfElements(allOptions);

        await page.GetByLabel("Select an address from the").SelectOptionAsync(option);

        await page.GetByRole(AriaRole.Button, new() { Name = "Save" }).ClickAsync();

        return await VerifyPageAsync(() => new SettingPage(context));
    }
}

public class ChangeYourAddressPage(ScenarioContext context) : FAABasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Change your address?");

    private static string Address => ("#SelectedAddress");
    public async Task<SettingPage> SaveAddressChange()
    {
        await page.GetByRole(AriaRole.Textbox, new() { Name = "UK postcode" }).FillAsync(fAAUserNameDataHelper.FaaNewUserPostCode);

        await page.GetByRole(AriaRole.Button, new() { Name = "Find my address" }).ClickAsync();

        var alloptionstexts = await AllTextAsync($"{Address} option");

        alloptionstexts = [.. alloptionstexts.ToList().Select(x => x.RemoveMultipleSpace())];

        var allOptions = alloptionstexts.Where(x => x != "Select address").ToList();

        var option = RandomDataGenerator.GetRandomElementFromListOfElements(allOptions);

        await page.GetByLabel("Select an address from the").SelectOptionAsync(option);

        await page.GetByRole(AriaRole.Button, new() { Name = "Save" }).ClickAsync();

        return await VerifyPageAsync(() => new SettingPage(context));
    }
}

    public class WhatIsYourTelephoneNumberPage(ScenarioContext context) : FAABasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("What is your telephone number?");

    public async Task<GetRemindersAboutYourUnfinishedApplicationsPage> SubmitApprenticeTelephoneNumber()
    {
        var number = fAAUserNameDataHelper.FaaNewUserMobilePhone;

        await page.GetByRole(AriaRole.Textbox, new() { Name = "Telephone number" }).FillAsync($"{number}");

        objectContext.SetDebugInformation($"Telephone number - '{number}'");

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new GetRemindersAboutYourUnfinishedApplicationsPage(context));
    }

}

public class ChangeYourTelephoneNumberPage(ScenarioContext context) : FAABasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Change your telephone number");
    public async Task TriggerTelephoneNumberValidationError()
    {
        await page.GetByRole(AriaRole.Textbox, new() { Name = "Telephone number" }).FillAsync(string.Empty);

        await page.GetByRole(AriaRole.Button, new() { Name = "Save" }).ClickAsync();

        await CheckValidationErrors("Enter your telephone number");
    }
    public async Task<SettingPage> SaveTelephoneNumberChange()
    {
        var number = fAAUserNameDataHelper.FaaNewUserMobilePhone;

        await page.GetByRole(AriaRole.Textbox, new() { Name = "Telephone number" }).FillAsync($"{number}");

        await page.GetByRole(AriaRole.Button, new() { Name = "Save" }).ClickAsync();

        return await VerifyPageAsync(() => new SettingPage(context));
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