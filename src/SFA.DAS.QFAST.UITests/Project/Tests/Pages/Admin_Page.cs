namespace SFA.DAS.QFAST.UITests.Project.Tests.Pages;

public class Admin_Page(ScenarioContext context) : BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("What do you want to do?");
    public async Task ValidateOptionsAsync()
    {
        var mainOptions = new[]
        {
                "Review funding requests",
                "Review newly regulated qualifications",
                "Review regulated qualifications with changes",
                "Import data",
                "Create a submission form",
                "Create an output file"
        };

        
    for (var i = 0; i < mainOptions.Length; i++)
        {            
            var optionLocator = page.Locator($"label.govuk-radios__label:has-text(\"{mainOptions[i]}\")");
            await Assertions.Expect(optionLocator).ToBeVisibleAsync();          
        }
       
        await Assertions.Expect(page.GetByRole(AriaRole.Button, new() { Name = "Continue" })).ToBeVisibleAsync();
    }
}


