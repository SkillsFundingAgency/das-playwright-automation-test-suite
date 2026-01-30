using System;

namespace SFA.DAS.EmployerAccounts.APITests.Project.Tests.StepDefinitions
{

    [Binding]
    public class EmployerAccountsOuterAPISteps(ScenarioContext context) : BaseSteps(context)
    {
        [Then(@"the employer accounts outer api is reachable")]
        public async Task ThenTheEmployerAccountsOuterApiIsReachable() => await employerAccountsOuterApiHelper.Ping();

        [Then(@"endpoint /Accounts/\{hashedAccountId}/levy/english-fraction-current can be accessed")]
        public async Task ThenEndpointAccountsHashedAccountIdLevyEnglish_Fraction_CurrentCanBeAccessed()
        {
            var hashedAccountId = objectContext.GetHashedAccountId();
            await employerAccountsOuterApiHelper.GetAccountEnglishFractionCurrent(hashedAccountId);
        }

        [Then(@"endpoint /Accounts/\{hashedAccountId}/levy/english-fraction-history can be accessed")]
        public async Task ThenEndpointAccountsHashedAccountIdLevyEnglish_Fraction_HistoryCanBeAccessed()
        {
            var hashedAccountId = objectContext.GetHashedAccountId();
            await employerAccountsOuterApiHelper.GetAccountEnglishFractionHistory(hashedAccountId);
        }

        [Then(@"endpoint /Accounts/\{accountId}/teams can be accessed")]
        public async Task ThenEndpointAccountsAccountIdTeamsCanBeAccessed()
        {
            var accountId = objectContext.GetAccountId();
            await employerAccountsOuterApiHelper.GetAccountTeams(accountId);
        }

        [Then(@"endpoint /Accounts/\{accountId}/create-task-list can be accessed")]
        public async Task ThenEndpointAccountsAccountIdCreate_Task_ListCanBeAccessed()
        {
            var accountId = objectContext.GetAccountId();
            await employerAccountsOuterApiHelper.GetAccountCreateTaskList(accountId);
        }

        [Then(@"endpoint /Reservation/\{accountId} can be accessed")]
        public async Task ThenEndpointReservationsAccountIdCanBeAccessed()
        {
            var accountId = objectContext.GetAccountId();
            await employerAccountsOuterApiHelper.GetReservation(accountId);
        }

        [Then(@"endpoint /AccountUsers/\{userId}/accounts can be accessed")]
        public async Task ThenEndpointAccountUsersUserIdAccountsCanBeAccessed()
        {
            var userId = objectContext.GetUserRef();
            await employerAccountsOuterApiHelper.GetAccountUsersAccounts(userId);
        }


        [Then(@"endpoint /EmployerAgreementTemplates can be accessed")]
        public async Task ThenEndpointEmployerAgreementTemplatesCanBeAccessed()
        {
            await employerAccountsOuterApiHelper.ExecuteEndpoint("/EmployerAgreementTemplates", HttpStatusCode.OK);
        }


        [Then(@"endpoint /SearchOrganisation/IdentifiableOrganisationTypes can be accessed")]
        public async Task ThenEndpointSearchOrganisationIdentifiableOrganisationTypesCanBeAccessed()
        {
            await employerAccountsOuterApiHelper.ExecuteEndpoint("/SearchOrganisation/IdentifiableOrganisationTypes", HttpStatusCode.OK);
        }


        [Then(@"endpoint /SearchOrganisation/organisations/search/results can be accessed")]
        public async Task ThenEndpointSearchOrganisationOrganisationsSearchResultsCanBeAccessed()
        {
            await employerAccountsOuterApiHelper.ExecuteEndpoint("/SearchOrganisation/organisations/search/results", HttpStatusCode.OK);
        }

        [Then(@"endpoint /SearchOrganisation/review?identifier={identifier}&organisationType={organisationType} can be accessed")]
        public async Task ThenEndpointSearchOrganisationReviewCanBeAccessed()
        {
            await employerAccountsOuterApiHelper.ExecuteEndpoint("/SearchOrganisation/review?identifier={identifier}&organisationType={organisationType}", HttpStatusCode.OK);
        }
    }
}
