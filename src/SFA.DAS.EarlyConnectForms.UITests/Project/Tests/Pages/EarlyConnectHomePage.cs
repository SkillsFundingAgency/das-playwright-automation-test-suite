using Azure;
using SFA.DAS.EarlyConnectForms.UITests.Project.Helpers;
using SFA.DAS.MailosaurAPI.Service.Project.Helpers;

namespace SFA.DAS.EarlyConnectForms.UITests.Project.Tests.Pages;

public abstract class EarlyConnectBasePage(ScenarioContext context) : BasePage(context)
{
    #region Helpers and Context
    protected readonly EarlyConnectDataHelper earlyConnectDataHelper = context.Get<EarlyConnectDataHelper>();

    #endregion

}

public class EarlyConnectHomePage(ScenarioContext context) : BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Get an apprenticeship adviser");

    public async Task AcceptCookieAndAlert()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Accept additional cookies" }).ClickAsync();
    }

    public async Task SelectNorthEast()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Get an adviser in North East" }).ClickAsync();
    }
    public async Task SelectLancashire()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Get an adviser in Lancashire" }).ClickAsync();

    }
    public async Task SelectLondon()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Get an adviser in Greater" }).ClickAsync();
    }

    public async Task<EmailAddressPage> GoToEmailAddressPage()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Get an adviser" }).ClickAsync();

        return await VerifyPageAsync(() => new EmailAddressPage(context));
    }
}

public class EmailAddressPage(ScenarioContext context) : EarlyConnectBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("label")).ToContainTextAsync("What is your email address?");


    public async Task<EmailAuthCodePage> EnterNewEmailAddress()
    {
        await page.Locator("#email").FillAsync("Amelia_10Jun2025_010614_Taylor_37480@7sumovsx.mailosaur.net");

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new EmailAuthCodePage(context));
    }
}

public class EmailAuthCodePage(ScenarioContext context) : EarlyConnectBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.GetByRole(AriaRole.Heading)).ToContainTextAsync("Check your email");

    public async Task<WhatsYourNamePage> EnterValidAuthCode()
    {
        var code = await context.Get<MailosaurApiHelper>().GetCodeInEmail(earlyConnectDataHelper.Email, "Confirm your email address – Department for education", "Your confirmation code is:");

        await page.Locator("#authcode").FillAsync(code);
        
        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new WhatsYourNamePage(context));
    }
}

public class WhatsYourNamePage(ScenarioContext context) : EarlyConnectBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.GetByRole(AriaRole.Heading)).ToContainTextAsync("What's your name?");

    public async Task<DateOfBirthPage> EnterFirstAndLastNames()
    {
        
        await page.Locator("#FirstName").FillAsync(earlyConnectDataHelper.Firstname);
        
        await page.Locator("#LastName").FillAsync(earlyConnectDataHelper.Lastname);

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new DateOfBirthPage(context));
    }
}

public class DateOfBirthPage(ScenarioContext context) : EarlyConnectBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.GetByRole(AriaRole.Heading)).ToContainTextAsync("What is your date of birth?");

    public async Task<PostcodePage> EnterValidDateOfBirth()
    {      
        await page.Locator("#Day").FillAsync($"{earlyConnectDataHelper.DateOfBirthDay}");
        
        await page.Locator("#Month").FillAsync($"{earlyConnectDataHelper.DateOfBirthMonth}");
        
        await page.Locator("#Year").FillAsync($"{earlyConnectDataHelper.DateOfBirthYear}");
        
        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new PostcodePage(context));
    }
}

public class PostcodePage(ScenarioContext context) : EarlyConnectBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("label")).ToContainTextAsync("What is your home postcode?");

    public async Task<TelephonePage> EnterValidPostcode()
    {
        await page.GetByRole(AriaRole.Textbox, new() { Name = "What is your home postcode?" }).FillAsync(earlyConnectDataHelper.PostCode);

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new TelephonePage(context));
    }
}

public class TelephonePage(ScenarioContext context) : EarlyConnectBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("label")).ToContainTextAsync("What's your telephone number? (optional)");

    public async Task<AreasOfWorkInterestPage> EnterValidTelephoneNumber()
    {
        await page.GetByRole(AriaRole.Textbox, new() { Name = "What's your telephone number" }).FillAsync(earlyConnectDataHelper.TelephoneNumber);

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new AreasOfWorkInterestPage(context));
    }
}

public class AreasOfWorkInterestPage(ScenarioContext context) : EarlyConnectBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("What areas of work interest you? (optional)");

    public async Task<SchoolCollegePage> SelectAnyAreaInterestToYou()
    {
        var workInterest = await page.Locator(".govuk-checkboxes__label").AllTextContentsAsync();

        var listOfWorkInterest = workInterest.ToList();

        var firstOption = RandomDataGenerator.GetRandomElementFromListOfElements(listOfWorkInterest);

        listOfWorkInterest.Remove(firstOption);

        var secondOption = RandomDataGenerator.GetRandomElementFromListOfElements(listOfWorkInterest);

        await page.GetByRole(AriaRole.Checkbox, new() { Name = firstOption, Exact = true }).CheckAsync();

        await page.GetByRole(AriaRole.Checkbox, new() { Name = secondOption, Exact = true }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new SchoolCollegePage(context));
    }
}

public class SchoolCollegePage(ScenarioContext context) : EarlyConnectBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("label")).ToContainTextAsync("What is the name of your school or college?");


    public async Task<ApprenticeshipsLevelPage> SearchValidSchoolOrCollegeName()
    {
        var name = earlyConnectDataHelper.SchoolOrCollegeName;

        await page.Locator("#schoolsearchterm").FillAsync(name);

        await page.GetByRole(AriaRole.Option, new() { Name = name, Exact =false }).First.ClickAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new ApprenticeshipsLevelPage(context));
    }

    public async Task<ApprenticeshipsLevelPage> EnterInvalidSchoolOrCollegeName()
    {
        await page.Locator("#schoolsearchterm").FillAsync("invalid selection");

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        await Assertions.Expect(page.Locator("#error-message-SchoolSearchTerm")).ToContainTextAsync("Enter the name of your school or college, or select 'I cannot find my school - enter manually'");

        await page.GetByRole(AriaRole.Link, new() { Name = "I cannot find my school - enter manually", Exact = true }).ClickAsync();

        await Assertions.Expect(page.GetByRole(AriaRole.Heading)).ToContainTextAsync("Enter the name of your school or college manually");

        await page.Locator("#schoolname").FillAsync($"{earlyConnectDataHelper.SchoolOrCollegeName} school or college manual entry");
        
        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new ApprenticeshipsLevelPage(context));
    }
}

public class ApprenticeshipsLevelPage(ScenarioContext context) : EarlyConnectBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.GetByRole(AriaRole.Heading)).ToContainTextAsync("What level of apprenticeship are you interested in?");

    public async Task<HaveYouAppliedPage> SelectAnyApprenticeshipLevelInterestToYou()
    {
        await page.GetByRole(AriaRole.Checkbox, new() { Name = "Intermediate apprenticeships" }).CheckAsync();

        await page.GetByRole(AriaRole.Checkbox, new() { Name = "Advanced apprenticeships (" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new HaveYouAppliedPage(context));
    }

    public async Task<HaveYouAppliedPage> SelectNotSure()
    {
        await page.GetByRole(AriaRole.Checkbox, new() { Name = "Not sure" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new HaveYouAppliedPage(context));
    }
}


public class HaveYouAppliedPage(ScenarioContext context) : EarlyConnectBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.GetByRole(AriaRole.Heading)).ToContainTextAsync("Have you applied for any of the following?");

    public async Task<AreaOfEnglandPage> SelectAnyPastApplications()
    {
        await page.GetByRole(AriaRole.Checkbox, new() { Name = "Apprenticeship" }).CheckAsync();

        await page.GetByRole(AriaRole.Checkbox, new() { Name = "University" }).CheckAsync();
        
        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new AreaOfEnglandPage(context));
    }

    public async Task<AreaOfEnglandPage> SelectNoneAbove()
    {
        await page.GetByRole(AriaRole.Checkbox, new() { Name = "None of the above" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new AreaOfEnglandPage(context));
    }
}

public class AreaOfEnglandPage(ScenarioContext context) : EarlyConnectBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.GetByRole(AriaRole.Heading)).ToContainTextAsync("Would you move to another area of England for an apprenticeship?");

    public async Task<SupportPage> SelectYesForTheRightApprenticeship()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "Yes, for the right" }).CheckAsync();
        
        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new SupportPage(context));
    }

    public async Task<SupportPage> SelectNo()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "No" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new SupportPage(context));
    }
}

public class SupportPage(ScenarioContext context) : EarlyConnectBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.GetByRole(AriaRole.Heading)).ToContainTextAsync("What would you like support with?");

    public async Task<CheckYourAnswerPage> SelectAnySupportThatAppliesToYou()
    {
        await page.GetByRole(AriaRole.Checkbox, new() { Name = "Understanding apprenticeships" }).CheckAsync();

        await page.GetByRole(AriaRole.Checkbox, new() { Name = "Finding apprenticeship" }).CheckAsync();

        await page.GetByRole(AriaRole.Checkbox, new() { Name = "Applying for an apprenticeship" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new CheckYourAnswerPage(context));
    }
}

public class CheckYourAnswerPage(ScenarioContext context) : EarlyConnectBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Check your details before they’re sent to DfE");

    public async Task<ApplicantSurveySummitedPage> AcceptAndSubmitForm()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Accept and submit" }).ClickAsync();

        return await VerifyPageAsync(() => new ApplicantSurveySummitedPage(context));
    }
}

public class ApplicantSurveySummitedPage(ScenarioContext context) : EarlyConnectBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Details sent to our team");

        await Assertions.Expect(page.Locator("body")).ToContainTextAsync("You’ll hear from an adviser soon");
    }
}