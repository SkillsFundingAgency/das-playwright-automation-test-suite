namespace SFA.DAS.ApprenticeAmbassadorNetwork.UITests.Project.Tests.Pages.Admin;

public abstract class AanAdminBasePage(ScenarioContext context) : AanBasePage(context)
{
    protected readonly AanAdminCreateEventDatahelper aanAdminCreateEventDatahelper = context.GetValue<AanAdminCreateEventDatahelper>();

    protected readonly AanAdminUpdateEventDatahelper aanAdminUpdateEventDatahelper = context.GetValue<AanAdminUpdateEventDatahelper>();

    protected async Task EnterYesOrNoRadioOption(string x)
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = x }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();
    }

    protected async Task SelectAutoDropDown(string text)
    {
        await page.GetByRole(AriaRole.Combobox).ClickAsync();

        await page.GetByRole(AriaRole.Combobox).FillAsync(text);

        await page.GetByRole(AriaRole.Option, new() { Name = text, Exact = false }).First.ClickAsync();
    }
}
