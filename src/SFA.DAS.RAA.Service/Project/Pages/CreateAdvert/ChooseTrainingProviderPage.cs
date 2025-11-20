
namespace SFA.DAS.RAA.Service.Project.Pages.CreateAdvert;


public class EnterTheNameOfTheTrainingProviderPage(ScenarioContext context) : ChooseTrainingProviderPage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Who is your training provider?");
    }
}

public class ChooseTrainingProviderPage(ScenarioContext context) : RaaBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Have you found a training provider?");
    }

    public async Task<ConfirmTrainingProviderPage> SelectTrainingProvider()
    {
        await page.GetByRole(AriaRole.Combobox, new() { Name = "Training provider name or" }).ClickAsync();

        await page.GetByRole(AriaRole.Combobox, new() { Name = "Training provider name or" }).FillAsync(RAADataHelper.Provider);

        await page.GetByRole(AriaRole.Option, new() { Name = RAADataHelper.Provider, Exact = false }).ClickAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();

        return await VerifyPageAsync(() => new ConfirmTrainingProviderPage(context));
    }
}

public class ConfirmTrainingProviderPage(ScenarioContext context) : RaaBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Confirm the training provider");
    }

    public async Task<SummaryOfTheApprenticeshipPage> ConfirmProviderAndContinueToSummaryPage()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();

        return await VerifyPageAsync(() => new SummaryOfTheApprenticeshipPage(context));
    }
}

public class SummaryOfTheApprenticeshipPage(ScenarioContext context) : RaaBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Summary of the apprenticeship");
    }

    public async Task<WhatWillTheApprenticeDoAtWorkPage> EnterShortDescription()
    {
        await IFrameFillAsync("ShortDescription_ifr", RAADataHelper.RandomAlphabeticString(60));

        await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();

        return await VerifyPageAsync(() => new WhatWillTheApprenticeDoAtWorkPage(context));
    }
}

public class WhatWillTheApprenticeDoAtWorkPage(ScenarioContext context) : RaaBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("What will the apprentice do at work?");
    }    

    public async Task<DescriptionPage> EnterShortDescriptionOfWhatApprenticeWillDo()
    {
        await IFrameFillAsync("VacancyDescription_ifr", rAADataHelper.VacancyShortDescription);
        
        await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();

        return await VerifyPageAsync(() => new DescriptionPage(context));
    }
}
