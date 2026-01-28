using Newtonsoft.Json;
using SFA.DAS.EmployerAccounts.APITests.Project.Models;

namespace SFA.DAS.EmployerAccounts.APITests.Project.Tests.StepDefinitions
{
    [Binding]
    public class EmployerAccountsLegacyAPISteps(ScenarioContext context) : BaseSteps(context)
    {
        [Then(@"endpoint api/accounts/\{hashedAccountId} from legacy accounts api can be accessed")]
        public async Task ThenEndpointApiAccountsHashedAccountIdCanBeAccessed()
        {
            var hashedAccountId = _objectContext.GetHashedAccountId();
            await _innerApiLegacyRestClient.ExecuteEndpoint($"/api/accounts/{hashedAccountId}", HttpStatusCode.OK);
        }

        [Then(@"endpoint /api/accounts/internal/\{accountId} from legacy accounts api can be accessed")]
        public async Task ThenEndpointApiAccountsInternalAccountIdCanBeAccessed()
        {
            var accountId = _objectContext.GetAccountId();
            await _innerApiLegacyRestClient.ExecuteEndpoint($"/api/accounts/internal/{accountId}", HttpStatusCode.OK);
        }

        [Then(@"endpoint /api/accounts/\{hashedAccountId}/users from legacy accounts api can be accessed")]
        public async Task ThenEndpointApiAccountsHashedAccountIdUsersFromLegacyAccountsApiCanBeAccessed()
        {
            var hashedAccountId = _objectContext.GetHashedAccountId();
            await _innerApiLegacyRestClient.ExecuteEndpoint($"/api/accounts/{hashedAccountId}/users", HttpStatusCode.OK);
        }

        [Then(@"endpoint /api/accounts/internal/\{accountId}/users from legacy accounts api can be accessed")]
        public async Task ThenEndpointApiAccountsAccountIdUsersCanBeAccessed()
        {
            var accountId = _objectContext.GetAccountId();
            await _innerApiLegacyRestClient.ExecuteEndpoint($"/api/accounts/internal/{accountId}/users", HttpStatusCode.OK);
        }

        [Then(@"endpoint api/accounts/\{hashedAccountId}/legalEntities/\{publicHashedLegalEntityId}/agreements/\{hashedAgreementId}/agreement from legacy accounts api can be accessed")]
        public async Task ThenEndpointApiAccountsAccountIdLegalEntitiesLegalEntityIdAgreementsAgreementIdAgreementCanBeAccessed()
        {
            var hashedAccountId = _objectContext.GetHashedAccountId();
            var agreementInfo = await _employerAccountsSqlDbHelper.GetAgreementInfo();
            var accountLegalEntityPublicHashedId = agreementInfo[0][0];
            var hashedAgreementId = _hashingService.HashValue((long)agreementInfo[0][1]);
            await _innerApiLegacyRestClient.ExecuteEndpoint($"/api/accounts/{hashedAccountId}/legalentities/{accountLegalEntityPublicHashedId}/agreements/{hashedAgreementId}", HttpStatusCode.OK);
        }

        [Then(@"endpoint api/accounts/\{hashedAccountId}/legalentities from legacy accounts api can be accessed")]
        public async Task ThenEndpointApiAccountsAccountIdLegalentitiesCanBeAccessed()
        {
            var hashedAccountId = _objectContext.GetHashedAccountId();
            await _innerApiLegacyRestClient.ExecuteEndpoint($"/api/accounts/{hashedAccountId}/legalentities", HttpStatusCode.OK);
        }

        [Then(@"endpoint api/accounts/\{hashedAccountId}/legalentities\?includeDetails=true from legacy accounts api can be accessed")]
        public async Task ThenEndpointApiAccountsAccountIdLegalentitiesIncludeDetailsTruecanBeAccessed()
        {
            var hashedAccountId = _objectContext.GetHashedAccountId();
            await _innerApiLegacyRestClient.ExecuteEndpoint($"/api/accounts/{hashedAccountId}/legalentities?includeDetails=true", HttpStatusCode.OK);
        }

        [Then(@"endpoint api/accounts/\{hashedAccountId}/legalentities/\{legalEntityId} from legacy accounts api can be accessed")]
        public async Task ThenEndpointApiAccountsAccountIdLegalentitiesLegalEntityIdCanBeAccessed()
        {
            var hashedAccountId = _objectContext.GetHashedAccountId();
            var legalEntityId = _objectContext.GetLegalEntityId();
            await _innerApiLegacyRestClient.ExecuteEndpoint($"/api/accounts/{hashedAccountId}/legalentities/{legalEntityId}", HttpStatusCode.OK);
        }

        [Then(@"endpoint api/accounts/\{hashedAccountId}/levy from legacy accounts api can be accessed")]
        public async Task ThenEndpointApiAccountsAccountIdLevyCanBeAccessed()
        {
            var hashedAccountId = _objectContext.GetHashedAccountId();
            await _innerApiLegacyRestClient.ExecuteEndpoint($"/api/accounts/{hashedAccountId}/levy", HttpStatusCode.OK);
        }

        [Then(@"endpoint api/accountlegalentities\?pageNumber=(.*)&pageSize=(.*) from legacy accounts api can be accessed")]
        public async Task ThenEndpointApiAccountlegalentitiesPageNumberPageSizeCanBeAccessed(int _, int __)
        {
            await _innerApiLegacyRestClient.ExecuteEndpoint("/api/accountlegalentities?pageNumber=1&pageSize=100", HttpStatusCode.OK);
        }

        [Then(@"endpoint /api/accounts/\{hashedAccountId}/payeschemes from legacy accounts api can be accessed")]
        public async Task ThenEndpointApiAccountsAccountIdPayeschemesCanBeAccessed()
        {
            var hashedAccountId = _objectContext.GetHashedAccountId();
            await _innerApiLegacyRestClient.ExecuteEndpoint($"/api/accounts/{hashedAccountId}/payeschemes", HttpStatusCode.OK);
        }

        [Then(@"endpoint /api/statistics from legacy accounts api can be accessed")]
        public async Task ThenEndpointApiStatisticsFromLegacyAccountsCanBeAccessed()
        {
            await _innerApiLegacyRestClient.ExecuteEndpoint($"/api/statistics", HttpStatusCode.OK);
        }

        [Then(@"endpoint api/accounts/\{hashedAccountId}/transactions from legacy accounts api can be accessed")]
        public async Task ThenEndpointApiAccountsAccountIdTransactionsCanBeAccessed()
        {
            var hashedAccountId = _objectContext.GetHashedAccountId();
            await _innerApiLegacyRestClient.ExecuteEndpoint($"/api/accounts/{hashedAccountId}/transactions", HttpStatusCode.OK);
        }

        [Then(@"endpoint api/accounts/\{hashedAccountId}/transactions/year/month from legacy accounts api can be accessed")]
        public async Task ThenEndpointApiAccountsAccountIdTransactionsYearMonthCanBeAccessed()
        {
            var hashedAccountId = _objectContext.GetHashedAccountId();
            var response = await _innerApiLegacyRestClient.ExecuteEndpoint($"/api/accounts/{hashedAccountId}/transactions");
            var result = JsonConvert.DeserializeObject<ICollection<TransactionSummary>>(response.Content);
            await _innerApiLegacyRestClient.ExecuteEndpoint($"/api/accounts/{hashedAccountId}/transactions/{result.FirstOrDefault().Year}/{result.FirstOrDefault().Month}", HttpStatusCode.OK);
        }

        [Then(@"endpoint api/user/\{userRef}/accounts from legacy accounts api can be accessed")]
        public async Task ThenEndpointApiUserUserRefAccountsCanBeAccessed()
        {
            var userRef = await _employerAccountsSqlDbHelper.GetUserRef();
            await _innerApiLegacyRestClient.ExecuteEndpoint($"/api/user/{userRef}/accounts", HttpStatusCode.OK);
        }
    }
}
