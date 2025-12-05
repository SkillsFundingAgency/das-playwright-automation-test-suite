using SFA.DAS.AparAdmin.UITests.Project.Tests.Pages;
using SFA.DAS.AparAdmin.UITests.Project.Tests.Pages.SearchAndUpdate;
using System;

namespace SFA.DAS.AparAdmin.UITests.Project.Tests.Steps;


[Binding, Scope(Tag = "apar")]
public class AddTrainingProviderShortCoursesSteps
{
    private readonly ScenarioContext _context;

    public AddTrainingProviderShortCoursesSteps(ScenarioContext context)
    {
        _context = context;
    }

    [Given(@"the user verifies links available in Manage Training Provider page")]
    public async Task GivenTheUserVerifiesLinksAvailableInManageTrainingProviderPage()
    {
        var manageTrainingProviderPage = await OpenManageTrainingProviderPage();
        var searchPage = await manageTrainingProviderPage.ClickSearchForATrainingProvider();
        var returnedManagePage = await searchPage.GoBackToManageTrainingProvider();
        //var page3 = await page2.ClickAddUkprnToAllowList();
        //var page4 = await page3.GoBackToManageTrainingProvider();
        //var page5 = await page3.ClickAddNewTrainingProvider();
        //await page5.GoBackToManageTrainingProvider();
    }

    [Given("the user navigates to training providers page")]
    public async Task GivenTheUserNavigatesToTrainingProvidersPage()
    {
        const string ukprn = "10056801";
        const string providerSearchText = "METRO BANK PLC UKPRN: 10056801";

        var manageTrainingProviderPage = await OpenManageTrainingProviderPage();
        var searchPage = await manageTrainingProviderPage.ClickSearchForATrainingProvider();
        var providerDetailsPage = await searchPage.EnterProviderDetailAndSearch(ukprn, providerSearchText);
    }


    [Given(@"the user updated the training provider route status to\s*""?(.*)""?")]
    public async Task GivenTheUserUpdatedTheTrainingProviderRouteStatusTo(string status)
    {
        const string providerName = "METRO BANK PLC";

        var providerDetailsPage = new ProviderDetailsPage(_context);
        var statusChangePage = await providerDetailsPage.ClickChangeStatusOfProvider();

        var resultPage = await statusChangePage.ChangeOrganisationStatus(status);

        var expectedStatus = (status ?? string.Empty).ToLowerInvariant();

        if (expectedStatus == "removed")
        {
            var removedPage = resultPage as RemovedReasonsPage
                ?? throw new Exception("Expected RemovedReasonsPage after selecting 'removed'");

            var successPage = await removedPage.ConfirmRemovalReasonAndContinue("14");

            await successPage.VerifyOrganisationStatus(providerName, expectedStatus);
            var providerPage = await successPage.GoBackToProviderDetailsPage();
            await providerPage.VerifyProviderStatus(expectedStatus);
        }
        else
        {
            var successPage = resultPage as SuccessPage
                ?? throw new Exception("Expected SuccessPage after status change");

            await successPage.VerifyOrganisationStatus(providerName, expectedStatus);
            var providerPage = await successPage.GoBackToProviderDetailsPage();
            await providerPage.VerifyProviderStatus(expectedStatus);
        }
    }

    [Given(@"the user updated the training provider type to\s*""?(.*)""?")]
    public async Task GivenTheUserUpdatedTheTrainingProviderTypeTo(string providerType)
    {
        var providerDetailsPage = new ProviderDetailsPage(_context);

        var providerRoutePage = await providerDetailsPage.ClickChangeTypeOfProvider();
        await providerRoutePage.SelectProviderType(providerType);

        // Optional: handle extra pages if provider type changed from Supporting → Main/Employer
        var h1Text = await providerRoutePage.GetHeadingTextAsync();
        if (h1Text.Contains("Do they offer apprenticeships?", StringComparison.OrdinalIgnoreCase))
        {
            var offerPage = new OfferApprenticeshipsPage(_context);
            var unitsPage = await offerPage.YesOfferApprenticeships();
            await unitsPage.YesOfferApprenticeshipsUnits();
        }
        await providerDetailsPage.VerifyProviderRouteType(providerType);
    }

    [Given(@"the user updated the training provider Organisation type to (.*)")]
    public async Task GivenTheUserUpdatedTheTrainingProviderOrganisationTypeTo(string orgType)
    {
        var providerDetailsPage = new ProviderDetailsPage(_context);

        var typeOfOrgPage = await providerDetailsPage.ClickChangeTypeOfOrganisation();
        providerDetailsPage = await typeOfOrgPage.YesChangeTypeofOrganisation(orgType);
        await providerDetailsPage.VerifyOrganisationType(orgType);
    }

    [Given(@"the user updated the training provider apprenticeship units to (Yes|No)")]
    public async Task GivenTheUserUpdatedTheTrainingProviderApprenticeshipUnitsTo(string answer)
    {
        var providerDetailsPage = new ProviderDetailsPage(_context);
        var apprenticeshipUnitsPage = await providerDetailsPage.ClickOffersApprenticeshipUnits();

        if (answer.Equals("Yes", StringComparison.OrdinalIgnoreCase))
        {
            providerDetailsPage = await apprenticeshipUnitsPage.YesOfferApprenticeshipsUnits();
        }
        else
        {
            providerDetailsPage = await apprenticeshipUnitsPage.NoDoNotOfferApprenticeshipsUnits();
        }
        await providerDetailsPage.VerifyApprenticeshipUnits(answer);
    }

    [Given("the user cannot update the training provider type to (.*) without offering apprenticehsips or apprenticeship units")]
    public async Task GivenTheUserCannotUpdateTheTrainingProviderTypeToMainProviderWithoutOfferingApprenticehsipsOrApprenticeshipUnits(string providerType)
    {
        var providerDetailsPage = new ProviderDetailsPage(_context);

        var providerRoutePage = await providerDetailsPage.ClickChangeTypeOfProvider();
        await providerRoutePage.SelectProviderType(providerType);

        var offerPage = new OfferApprenticeshipsPage(_context);
        var unitsPage = await offerPage.NoDoNotOfferApprenticeships();
        await unitsPage.TrainingProviderMustOfferEitherApprenticeshipsOrApprenticeshipUnits();
        await unitsPage.YesOfferApprenticeshipsUnits();

    }

    private async Task<ManageTrainingProviderInformationPage> OpenManageTrainingProviderPage()
    {
        var home = new AparAdminHomePage(_context);
        await home.ClickAddOrSearchForProvider();
        return await new ManageTrainingProviderInformationPage(_context)
            .VerifyPageAsync(() => new ManageTrainingProviderInformationPage(_context));
    }
}

