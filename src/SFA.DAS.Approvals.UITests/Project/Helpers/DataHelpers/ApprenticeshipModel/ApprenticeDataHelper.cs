using SFA.DAS.Approvals.UITests.Project.Helpers.SqlHelpers;
using System;


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

            apprenticeships = (apprenticeships == null) ? new List<Apprenticeship>() : apprenticeships;
            var employerDetails = await GetEmployerDetails(EmployerType);

            for (int i = 0; i < NumberOfApprenticeships; i++)
            {
                // Create random apprentice, training, and RPL details
                var apprenticeDetails = await apprenticeFactory.CreateApprenticeAsync();
                var training = await trainingFactory.CreateTrainingAsync(EmployerType);
                var rpl = await rplFactory.CreateRPLAsync();


                // Create apprenticeship object with above generated details
                Apprenticeship apprenticeship = new(apprenticeDetails, training, rpl)
                {
                    EmployerDetails = employerDetails,
                    UKPRN = Ukprn != null ? Convert.ToInt32(Ukprn) : Convert.ToInt32(context.GetProviderConfig<ProviderConfig>().Ukprn),
                };


                // Add to the list
                apprenticeships.Add(apprenticeship);
            }

            return apprenticeships;
        }

        private async Task<Employer> GetEmployerDetails(EmployerType employerType)
        {
            Employer employer = new();

            EasAccountUser employerUser = employerType switch
            {
                EmployerType.NonLevy => context.GetUser<NonLevyUser>(),
                EmployerType.NonLevyUserAtMaxReservationLimit => context.GetUser<NonLevyUserAtMaxReservationLimit>(),
                _ => context.GetUser<LevyUser>()
            };

            employer.EmployerName = employerUser.OrganisationName;

            employer.AgreementId = await context.Get<AccountsDbSqlHelper>().GetAgreementId(employerUser.Username, employer.EmployerName[..3] + "%");

            employer.EmployerType = employerType;

            employer.Email = employerUser.Username;

            return employer;
        }

    }
}
