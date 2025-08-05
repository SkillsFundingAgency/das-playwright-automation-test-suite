using System;
using SFA.DAS.Approvals.UITests.Project.Helpers.DataHelpers;
using SFA.DAS.Approvals.UITests.Project.Helpers.StepsHelper;
using SFA.DAS.Approvals.UITests.Project.Pages.Employer;
using SFA.DAS.FrameworkHelpers;
using SFA.DAS.FrameworkHelpers;
using TechTalk.SpecFlow;
using SFA.DAS.Approvals.UITests.Project.Helpers.StepsHelper.Provider;
using SFA.DAS.Approvals.UITests.Project.Helpers.DataHelpers.ApprenticeshipModel;
using SFA.DAS.Approvals.UITests.Project.Steps;
using SFA.DAS.Approvals.UITests.Project.Tests.Pages.Employer;




namespace SFA.DAS.Approvals.UITests.Project.Helpers.StepsHelper.Employer
{
    public class EmployerCreateCohortStepsHelper
    {
        private readonly ObjectContext _objectContext;
        protected readonly ScenarioContext context;

        private readonly CohortReferenceHelper _cohortReferenceHelper;
        private readonly ConfirmProviderDetailsHelper _confirmProviderDetailsHelper;


        public EmployerCreateCohortStepsHelper(ScenarioContext context)
        {
            this.context = context;
            _objectContext = context.Get<ObjectContext>();
            _cohortReferenceHelper = new CohortReferenceHelper(context);
            _confirmProviderDetailsHelper = new ConfirmProviderDetailsHelper(this.context);
        }

        public void EmployerCreateCohortViaLevyFundsAndSendsToProvider()
        {
            var cohortSentYourTrainingProviderPage = EmployerCreateCohortViaLevyFunds(false);

            var cohortReference = cohortSentYourTrainingProviderPage.CohortReference();

            _cohortReferenceHelper.SetCohortReference(cohortReference);
        }

        private CohortSentYourTrainingProviderPage EmployerCreateCohortViaLevyFunds(bool isTransferReceiverEmployer)
        {
            return _confirmProviderDetailsHelper
               .ConfirmProviderDetailsAreCorrect(isTransferReceiverEmployer, AddTrainingProviderDetailsFuncWithoutSelectFundingOption())
               .EmployerSendsToProviderToAddApprentices()
               .VerifyMessageForTrainingProvider(context.GetValue<ApprenticeDataHelper>().MessageToProvider);
        }



        protected virtual Func<AddAnApprenitcePage, AddTrainingProviderDetailsPage> AddTrainingProviderDetailsFunc() => AddTrainingProviderStepsHelper.AddTrainingProviderDetailsViaCurrentLevyFundsFunc();

        protected virtual Func<AddAnApprenitcePage, AddTrainingProviderDetailsPage> AddTrainingProviderDetailsFuncWithoutSelectFundingOption() => AddTrainingProviderStepsHelper.AddTrainingProviderDetailsFunc();

        
    }
}
