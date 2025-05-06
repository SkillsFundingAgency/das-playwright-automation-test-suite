using System.Linq;

namespace SFA.DAS.SupportTools.UITests.Project.Tests.Pages;

public class ChallengePage(ScenarioContext context) : SupportConsoleBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.GetByRole(AriaRole.Paragraph).Nth(3)).ToContainTextAsync("Enter the following information to verify the caller's identity:");


    #region Helpers and Context
    private char[] _payeschemechars;
    #endregion

    public async Task EnterIncorrectPaye() => await EnterPayeChallenge("2", "2");

    public async Task EnterCorrectPaye()
    {
        string func(char[] chars, int position)
        {
            var digit = chars.ElementAt(position - 1).ToString();

            objectContext.Set($"challenge position {position}", digit);

            return digit;
        }

        _payeschemechars = config.PayeScheme.Replace("/", string.Empty).ToCharArray();

        var text = await page.GetByText("character of a PAYE").TextContentAsync();

        (int x, int y) = RegexHelper.GetPayeChallenge(text);

        await EnterPayeChallenge(func(_payeschemechars, x), func(_payeschemechars, y));
    }

    public async Task EnterCorrectLevybalance() => await page.GetByRole(AriaRole.Textbox, new() { Name = "Current levy balance" }).FillAsync(RegexHelper.GetLevyBalance(config.CurrentLevyBalance));

    public async Task Submit() => await page.GetByRole(AriaRole.Button, new() { Name = "Submit" }).ClickAsync();

    public async Task VerifyChallengeResponseErrorMessage(string errorMessage) => await Assertions.Expect(page.Locator("#main-content")).ToContainTextAsync(errorMessage);

    private async Task EnterPayeChallenge(string char1, string char2)
    {
        await page.GetByRole(AriaRole.Textbox, new() { Name = "character of a PAYE" }).FillAsync(char1);

        await page.Locator("#challenge2").FillAsync(char2);
    }
}
