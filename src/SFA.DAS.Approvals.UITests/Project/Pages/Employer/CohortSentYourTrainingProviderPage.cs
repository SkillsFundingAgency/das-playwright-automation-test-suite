using Azure;
using DnsClient;
using Microsoft.Playwright;
using NUnit.Framework;
using NUnit.Framework.Internal;
using SFA.DAS.Approvals.UITests.Project;
using SFA.DAS.Approvals.UITests.Project.Helpers.DataHelpers.ApprenticeshipModel;
using SFA.DAS.Approvals.UITests.Project.Pages;
using TechTalk.SpecFlow;
using SFA.DAS.Approvals.UITests.Project.Helpers.DataHelpers;
using SFA.DAS.Approvals.UITests.Project.Steps;
using SFA.DAS.Approvals.UITests.Project.Pages.Provider;



    namespace SFA.DAS.Approvals.UITests.Project.Pages.Employer
{
    internal class CohortSentYourTrainingProviderPage(ScenarioContext context) : CohortReferenceBasePage(context)
    {
        //private ILocator messageForTrainingProvider => page.Locator("dt:has-text('Message for training provider') + dd");

        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Add apprentice request sent to training provider");
        }

       

    }


    }