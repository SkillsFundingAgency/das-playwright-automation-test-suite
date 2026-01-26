using Newtonsoft.Json;
using SFA.DAS.EmployerAccounts.APITests.Project.Models;
using System;


namespace SFA.DAS.EmployerAccounts.APITests.Project.Tests.StepDefinitions
{

    [Binding]
    public class EmployerAccountsAPISteps(ScenarioContext context) : BaseSteps(context)
    {
        [Then(@"the employer accounts outer api is reachable")]
       public async Task ThenTheEmployerAccountsOuterApiIsReachable() => await _employerAccountsOuterApiHelper.Ping();

        [Then(@"endpoint /Accounts/\{hashedAccountId}/levy/english-fraction-current can be accessed")]
        public async Task ThenEndpointAccountsHashedAccountIdLevyEnglish_Fraction_CurrentCanBeAccessed()
        {
            var hashedAccountId = _objectContext.GetHashedAccountId();
            await _employerAccountsOuterApiHelper.GetAccountEnglishFractionCurrent(hashedAccountId);
        }

        [Then(@"endpoint /Accounts/\{hashedAccountId}/levy/english-fraction-history can be accessed")]
        public async Task ThenEndpointAccountsHashedAccountIdLevyEnglish_Fraction_HistoryCanBeAccessed()
        {
            var hashedAccountId = _objectContext.GetHashedAccountId();
            await  _employerAccountsOuterApiHelper.GetAccountEnglishFractionHistory(hashedAccountId);
        }

        [Then(@"endpoint /api/accountlegalentities can be accessed")]
        public async Task ThenEndpointApiAccountlegalentitiesCanBeAccessed()
        {
            await _innerApiRestClient.ExecuteEndpoint("/api/accountlegalentities?query.pageNumber=1&query.pageSize=100", HttpStatusCode.OK);
        }

        [Then(@"endpoint /api/accounts/\{accountId}/payeschemes can be accessed")]
        public async Task ThenEndpointApiAccountIdPayeschemesCanBeAccessed()
        {
            var accountId = _objectContext.GetAccountId();
            await _innerApiRestClient.ExecuteEndpoint($"/api/accounts/{accountId}/payeschemes", HttpStatusCode.OK);
        }

        [Then(@"endpoint /api/accounts/\{accountId}/payeschemes/\{payeSchemeRef} can be accessed")]
        [Then(@"endpoint /api/accounts/\{accountId}/payeschemes/scheme?ref=\{payeSchemeRef} can be accessed")]
        public async Task ThenEndpointApiAccountsAccountIdPayeschemesPayeSchemeRefCanBeAccessed()
        {
            var accountId = _objectContext.GetAccountId();
            var payeschemeRef = _objectContext.GetPayeSchemeRefId();
            var encodepayeschemeRef = Uri.EscapeDataString(payeschemeRef);
            await _innerApiRestClient.ExecuteEndpoint($"/api/accounts/{accountId}/payeschemes/scheme?payeSchemeRef={encodepayeschemeRef}", HttpStatusCode.OK);
        }

        [Then(@"endpoint /api/accounts can be accessed")]
        public async Task ThenEndpointApiAccountsCanBeAccessed()
        {
            await _innerApiRestClient.ExecuteEndpoint($"/api/accounts/", HttpStatusCode.OK);
        }

        [Then(@"endpoint /api/accounts/\{hashedAccountId} can be accessed")]
        public async Task ThenEndpointApiAccountsHashedAccountIdCanBeAccessed()
        {
            var hashedAccountId = _objectContext.GetHashedAccountId();
            await _innerApiRestClient.ExecuteEndpoint($"/api/accounts/{hashedAccountId}", HttpStatusCode.OK);
        }

        [Then(@"endpoint /accounts/internal/\{accountId} can be accessed")]
        public async Task ThenEndpointAccountsInternalAccountIdCanBeAccessed()
        {
            var accountId = _objectContext.GetAccountId();
            await _innerApiRestClient.ExecuteEndpoint($"/api/accounts/internal/{accountId}/users", HttpStatusCode.OK);
        }

        [Then(@"endpoint /api/accounts/\{hashedAccountId}/users can be accessed")]
        public async Task ThenEndpointApiAccountsHashedAccountIdUsersCanBeAccessed()
        {
            var hashedAccountId = _objectContext.GetHashedAccountId();
            await _innerApiRestClient.ExecuteEndpoint($"/api/accounts/{hashedAccountId}/users", HttpStatusCode.OK);
        }


        [Then(@"endpoint /api/accounts/internal/\{accountId}/users can be accessed")]
        public async Task ThenEndpointApiAccountsInternalAccountIdUsersCanBeAccessed()
        {
            var accountId = _objectContext.GetAccountId();
            await _innerApiRestClient.ExecuteEndpoint($"/api/accounts/internal/{accountId}/users", HttpStatusCode.OK);
        }

        [Then(@"endpoint /api/accounts/\{hashedAccountId}/legalEntities/\{hashedlegalEntityId}/agreements/\{agreementId} can be accessed")]
        public async Task ThenEndpointApiAccountsHashedAccountIdLegalEntitiesHashedlegalEntityIdAgreementsAgreementIdCanBeAccessed()
        {
            var result = await _employerAccountsSqlDbHelper.GetAgreementInfo();
            var hashedAgreementId = _hashingService.HashValue((long)result[0][1]);
            await _innerApiRestClient.ExecuteEndpoint($"/api/accounts/{_objectContext.GetHashedAccountId()}/legalentities/{result[0][0]}/agreements/{hashedAgreementId}", HttpStatusCode.OK);
        }

        [Then(@"endpoint /api/user/\{userRef}/accounts can be accessed")]
        public async Task ThenEndpointApiUserUserRefAccountsCanBeAccessed()
        {
            var userRef = await _employerAccountsSqlDbHelper.GetUserRef();
            await _innerApiRestClient.ExecuteEndpoint($"/api/User/{userRef}/accounts", HttpStatusCode.OK);
        }

        [Then(@"endpoint /api/accounts/\{AccountId}/legalentities can be accessed")]
        public async Task ThenEndpointApiAccountsAccountIdLegalentitiesCanBeAccessed()
        {
            var accountId = _objectContext.GetAccountId();
            await _innerApiRestClient.ExecuteEndpoint($"/api/accounts/{accountId}/legalentities", HttpStatusCode.OK);
        }

        [Then(@"endpoint /api/accounts/\{AccountId}/legalentities/\{legalEntityId} can be accessed")]
        public async Task ThenEndpointApiAccountsAccountIdLegalentitiesLegalEntityIdCanBeAccessed()
        {
            var accountId = _objectContext.GetAccountId();
            await _innerApiRestClient.ExecuteEndpoint($"/api/accounts/{accountId}/legalentities/{_objectContext.GetLegalEntityId()}", HttpStatusCode.OK);
        }


        [Then(@"endpoint /api/statistics can be accessed")]
        public async Task ThenEndpointApiStatisticsCanBeAccessed()
        {
            await _innerApiRestClient.ExecuteEndpoint("/api/statistics", HttpStatusCode.OK);
        }

        [Then(@"endpoint /api/accounts/internal/\{accountId}/transfers/connections can be accessed")]
        public async Task ThenEndpointApiAccountsInternalAccountIdTransfersConnectionsCanBeAccessed()
        {
            var accountId = _objectContext.GetAccountId();
            await _innerApiRestClient.ExecuteEndpoint($"/api/accounts/internal/{accountId}/transfers/connections", HttpStatusCode.OK);
        }

        [Then(@"endpoint /api/user can be accessed")]
        public async Task ThenEndpointApiUserCanBeAccessed()
        {
            var userEmail = await _employerAccountsSqlDbHelper.GetUserEmail();
            await _innerApiRestClient.ExecuteEndpoint($"/api/User?email={userEmail}", HttpStatusCode.OK);
        }

        [Then(@"endpoint /accounts/\{hashedAccountId}/transactions  can be accessed")]
        public async Task ThenEndpointAccountsHashedAccountIdTransactionsCanBeAccessed()
        {
            var hashedAccountId = _objectContext.GetHashedAccountId();
            await _innerApiLegacyRestClient.ExecuteEndpoint($"/api/accounts/{hashedAccountId}/transactions", HttpStatusCode.OK);
        }

        [Then(@"endpoint /accounts/\{hashedAccountId}/transactions/\{year}/\{month} can be accessed")]
        public async Task ThenEndpointAccountsHashedAccountIdTransactionsYearMonthCanBeAccessed()
        {
            var hashedAccountId = _objectContext.GetHashedAccountId();
            var response = await _innerApiLegacyRestClient.ExecuteEndpoint($"/api/accounts/{hashedAccountId}/transactions");
            var result = JsonConvert.DeserializeObject<ICollection<TransactionSummary>>(response.Content);
            await _innerApiLegacyRestClient.ExecuteEndpoint($"/api/accounts/{hashedAccountId}/transactions/{result.FirstOrDefault().Year}/{result.FirstOrDefault().Month}", HttpStatusCode.OK);
        }

        [Then(@"das-employer-accounts-api /ping endpoint can be accessed")]
        public async Task ThenDas_Employer_Accounts_ApiPingEndpointCanBeAccessed()
        {
            await _innerApiRestClient.ExecuteEndpoint("/ping", HttpStatusCode.OK);
        }
    }
}
