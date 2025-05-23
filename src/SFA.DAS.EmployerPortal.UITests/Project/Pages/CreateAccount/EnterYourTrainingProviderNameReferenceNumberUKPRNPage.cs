﻿namespace SFA.DAS.EmployerPortal.UITests.Project.Pages.CreateAccount;

public class EnterYourTrainingProviderNameReferenceNumberUKPRNPage(ScenarioContext context) : EmployerPortalBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("name or reference number (UKPRN)");
    }

    public async Task<AddPermissionsForTrainingProviderPage> SearchForATrainingProvider(ProviderConfig providerConfig)
    {
        await EnterATrainingProvider(providerConfig);

        return await VerifyPageAsync(() => new AddPermissionsForTrainingProviderPage(context, providerConfig));
    }

    public async Task<AlreadyLinkedToTrainingProviderPage> SearchForAnExistingTrainingProvider(ProviderConfig providerConfig)
    {
        await EnterATrainingProvider(providerConfig);

        return await VerifyPageAsync(() => new AlreadyLinkedToTrainingProviderPage(context));
    }

    private async Task EnterATrainingProvider(ProviderConfig providerConfig)
    {
        await page.Locator("#SearchTerm").PressSequentiallyAsync(providerConfig.Ukprn, new() { Delay = 1000 });

        await Task.Delay(1000);

        await page.GetByRole(AriaRole.Option, new() { Name = providerConfig.Ukprn }).ClickAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        //new TrainingProviderAutoCompleteHelper(context).SelectFromAutoCompleteList(providerConfig.Ukprn);

    }
}

public class AlreadyLinkedToTrainingProviderPage(ScenarioContext context) : PermissionBasePageForEmployerPortalPage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("You're already linked to this training provider");
    }

    public async Task CannotAddExistingTrainingProvider()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Return to your training" }).ClickAsync();
    }
}
