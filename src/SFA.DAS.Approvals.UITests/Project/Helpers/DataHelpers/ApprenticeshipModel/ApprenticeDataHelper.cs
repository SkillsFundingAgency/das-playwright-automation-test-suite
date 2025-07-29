using DnsClient;
using Dynamitey;
using MongoDB.Driver.Linq;
using Polly;
using SFA.DAS.Approvals.UITests.Project.Helpers.SqlHelpers;
using SFA.DAS.FrameworkHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace SFA.DAS.Approvals.UITests.Project.Helpers.DataHelpers.ApprenticeshipModel
{
    internal class ApprenticeDataHelper(ScenarioContext context)
    {
        public async Task<List<Apprenticeship>> CreateApprenticeshipAsync(            
            EmployerType EmployerType, 
            int NumberOfApprenticeships, 
            string Ukprn = null,
            List<Apprenticeship>? apprenticeships = null,
            IApprenticeFactory? apprenticeFactory = null,
            ITrainingFactory? trainingFactory = null,
            IRPLFactory? rplFactory = null)

        {
            apprenticeFactory ??= new ApprenticeFactory();
            trainingFactory ??= new TrainingFactory();
            rplFactory ??= new RPLFactory();

            apprenticeships = (apprenticeships == null) ? new List<Apprenticeship>(): apprenticeships;
            var employerDetails = await GetEmployerDetails(EmployerType);
            var ukprn = Ukprn != null ? Convert.ToInt32(Ukprn) : Convert.ToInt32(context.GetProviderConfig<ProviderConfig>().Ukprn);

            for (int i = 0; i < NumberOfApprenticeships; i++)
            {
                // Create random apprentice, training, and RPL details
                var apprenticeDetails = await apprenticeFactory.CreateApprenticeAsync();
                var training = await trainingFactory.CreateTrainingAsync(EmployerType);
                var rpl = await rplFactory.CreateRPLAsync();


                // Create apprenticeship object with above generated details
                Apprenticeship apprenticeship = new Apprenticeship()
                {                    
                    UKPRN = ukprn,       //Ukprn != null ? Convert.ToInt32(Ukprn) : Convert.ToInt32(context.GetProviderConfig<ProviderConfig>().Ukprn),
                    EmployerDetails = employerDetails,
                    ApprenticeDetails = apprenticeDetails,
                    TrainingDetails = training,
                    RPLDetails = rpl,
                };
                

                // Add to the list
                apprenticeships.Add(apprenticeship);
            }

            return apprenticeships;
        }

        internal async Task<Apprenticeship> CreateEmptyCohortAsync(EmployerType EmployerType, string Ukprn = null)
        {
            var employerDetails = await GetEmployerDetails(EmployerType);
            var ukprn = Ukprn != null ? Convert.ToInt32(Ukprn) : Convert.ToInt32(context.GetProviderConfig<ProviderConfig>().Ukprn);

            Apprenticeship apprenticeship = new Apprenticeship()
            {
                UKPRN = ukprn,
                EmployerDetails = employerDetails,
            };

            return apprenticeship;
        }

        private async Task<Employer> GetEmployerDetails(EmployerType employerType)
        {
            var accountsDbSqlHelper = context.Get<AccountsDbSqlHelper>();
            Employer employer = new Employer();

            EasAccountUser employerUser = employerType switch
            {
                EmployerType.NonLevy => context.GetUser<NonLevyUser>(),
                EmployerType.NonLevyUserAtMaxReservationLimit => context.GetUser<NonLevyUserAtMaxReservationLimit>(),
                _ => context.GetUser<LevyUser>()
            };                      

            employer.EmployerName = employerUser.OrganisationName;

            employer.EmployerType = employerType;

            employer.Email = employerUser.Username;

            employer.AgreementId = await accountsDbSqlHelper.GetAgreementId(employerUser.Username, employer.EmployerName[..3] + "%");

            var aleId = await accountsDbSqlHelper.GetAccountLegalEntityId(employerUser.Username, employer.EmployerName[..3] + "%");
            employer.AccountLegalEntityId = Convert.ToInt32(aleId);               //Convert.ToInt32(accountsDbSqlHelper.GetAccountLegalEntityId(employerUser.Username, employer.EmployerName[..3] + "%"));

            return employer;
        }

    }
}
