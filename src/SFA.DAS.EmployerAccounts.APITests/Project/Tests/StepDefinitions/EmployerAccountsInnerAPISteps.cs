using Newtonsoft.Json;
using SFA.DAS.EmployerAccounts.APITests.Project.Models;
using System;


namespace SFA.DAS.EmployerAccounts.APITests.Project.Tests.StepDefinitions
{

    [Binding]
    public class EmployerAccountsInnerAPISteps(ScenarioContext context) : BaseSteps(context)
    {
        [Then(@"endpoint /api/accountlegalentities can be accessed")]
        public async Task ThenEndpointApiAccountlegalentitiesCanBeAccessed()
        {
            await innerApiRestClient.ExecuteEndpoint("/api/accountlegalentities?query.pageNumber=1&query.pageSize=100", HttpStatusCode.OK);
        }

        [Then(@"endpoint /api/accounts/\{accountId}/payeschemes can be accessed")]
        public async Task ThenEndpointApiAccountIdPayeschemesCanBeAccessed()
        {
            var accountId = objectContext.GetAccountId();
            await innerApiRestClient.ExecuteEndpoint($"/api/accounts/{accountId}/payeschemes", HttpStatusCode.OK);
        }

        [Then(@"endpoint /api/accounts/\{accountId}/payeschemes/\{payeSchemeRef} can be accessed")]
        [Then(@"endpoint /api/accounts/\{accountId}/payeschemes/scheme?ref=\{payeSchemeRef} can be accessed")]
        public async Task ThenEndpointApiAccountsAccountIdPayeschemesPayeSchemeRefCanBeAccessed()
        {
            var accountId = objectContext.GetAccountId();
            var payeschemeRef = objectContext.GetPayeSchemeRefId();
            var encodepayeschemeRef = Uri.EscapeDataString(payeschemeRef);
            await innerApiRestClient.ExecuteEndpoint($"/api/accounts/{accountId}/payeschemes/scheme?payeSchemeRef={encodepayeschemeRef}", HttpStatusCode.OK);
        }

        [Then(@"endpoint /api/accounts can be accessed")]
        public async Task ThenEndpointApiAccountsCanBeAccessed()
        {
            await innerApiRestClient.ExecuteEndpoint($"/api/accounts/", HttpStatusCode.OK);
        }

        [Then(@"endpoint /api/accounts/\{hashedAccountId} can be accessed")]
        public async Task ThenEndpointApiAccountsHashedAccountIdCanBeAccessed()
        {
            var hashedAccountId = objectContext.GetHashedAccountId();
            await innerApiRestClient.ExecuteEndpoint($"/api/accounts/{hashedAccountId}", HttpStatusCode.OK);
        }

        [Then(@"endpoint /accounts/internal/\{accountId} can be accessed")]
        public async Task ThenEndpointAccountsInternalAccountIdCanBeAccessed()
        {
            var accountId = objectContext.GetAccountId();
            await innerApiRestClient.ExecuteEndpoint($"/api/accounts/internal/{accountId}/users", HttpStatusCode.OK);
        }

        [Then(@"endpoint /api/accounts/\{hashedAccountId}/users can be accessed")]
        public async Task ThenEndpointApiAccountsHashedAccountIdUsersCanBeAccessed()
        {
            var hashedAccountId = objectContext.GetHashedAccountId();
            await innerApiRestClient.ExecuteEndpoint($"/api/accounts/{hashedAccountId}/users", HttpStatusCode.OK);
        }


        [Then(@"endpoint /api/accounts/internal/\{accountId}/users can be accessed")]
        public async Task ThenEndpointApiAccountsInternalAccountIdUsersCanBeAccessed()
        {
            var accountId = objectContext.GetAccountId();
            await innerApiRestClient.ExecuteEndpoint($"/api/accounts/internal/{accountId}/users", HttpStatusCode.OK);
        }

        [Then(@"endpoint /api/accounts/\{hashedAccountId}/legalEntities/\{hashedlegalEntityId}/agreements/\{agreementId} can be accessed")]
        public async Task ThenEndpointApiAccountsHashedAccountIdLegalEntitiesHashedlegalEntityIdAgreementsAgreementIdCanBeAccessed()
        {
            var result = await employerAccountsSqlDbHelper.GetAgreementInfo();
            var hashedAgreementId = hashingService.HashValue((long)result[0][1]);
            await innerApiRestClient.ExecuteEndpoint($"/api/accounts/{objectContext.GetHashedAccountId()}/legalentities/{result[0][0]}/agreements/{hashedAgreementId}", HttpStatusCode.OK);
        }

        [Then(@"endpoint /api/user/\{userRef}/accounts can be accessed")]
        public async Task ThenEndpointApiUserUserRefAccountsCanBeAccessed()
        {
            var userRef = await employerAccountsSqlDbHelper.GetUserRef();
            await innerApiRestClient.ExecuteEndpoint($"/api/User/{userRef}/accounts", HttpStatusCode.OK);
        }

        [Then(@"endpoint /api/accounts/\{AccountId}/legalentities can be accessed")]
        public async Task ThenEndpointApiAccountsAccountIdLegalentitiesCanBeAccessed()
        {
            var accountId = objectContext.GetAccountId();
            await innerApiRestClient.ExecuteEndpoint($"/api/accounts/{accountId}/legalentities", HttpStatusCode.OK);
        }

        [Then(@"endpoint /api/accounts/\{AccountId}/legalentities/\{legalEntityId} can be accessed")]
        public async Task ThenEndpointApiAccountsAccountIdLegalentitiesLegalEntityIdCanBeAccessed()
        {
            var accountId = objectContext.GetAccountId();
            await innerApiRestClient.ExecuteEndpoint($"/api/accounts/{accountId}/legalentities/{objectContext.GetLegalEntityId()}", HttpStatusCode.OK);
        }


        [Then(@"endpoint /api/statistics can be accessed")]
        public async Task ThenEndpointApiStatisticsCanBeAccessed()
        {
            await innerApiRestClient.ExecuteEndpoint("/api/statistics", HttpStatusCode.OK);
        }

        [Then(@"endpoint /api/accounts/internal/\{accountId}/transfers/connections can be accessed")]
        public async Task ThenEndpointApiAccountsInternalAccountIdTransfersConnectionsCanBeAccessed()
        {
            var accountId = objectContext.GetAccountId();
            await innerApiRestClient.ExecuteEndpoint($"/api/accounts/internal/{accountId}/transfers/connections", HttpStatusCode.OK);
        }

        [Then(@"endpoint /api/user can be accessed")]
        public async Task ThenEndpointApiUserCanBeAccessed()
        {
            var userEmail = await employerAccountsSqlDbHelper.GetUserEmail();
            await innerApiRestClient.ExecuteEndpoint($"/api/User?email={userEmail}", HttpStatusCode.OK);
        }

        [Then(@"endpoint api/accounts/\{hashedAccountId}/transactions  can be accessed")]
        public async Task ThenEndpointAccountsHashedAccountIdTransactionsCanBeAccessed()
        {
            var hashedAccountId = objectContext.GetHashedAccountId();
            await innerApiLegacyRestClient.ExecuteEndpoint($"/api/accounts/{hashedAccountId}/transactions", HttpStatusCode.OK);
        }

        [Then(@"endpoint api/accounts/\{hashedAccountId}/transactions/\{year}/\{month} can be accessed")]
        public async Task ThenEndpointAccountsHashedAccountIdTransactionsYearMonthCanBeAccessed()
        {
            var hashedAccountId = objectContext.GetHashedAccountId();
            var response = await innerApiLegacyRestClient.ExecuteEndpoint($"/api/accounts/{hashedAccountId}/transactions");
            var result = JsonConvert.DeserializeObject<ICollection<TransactionSummary>>(response.Content);
            await innerApiLegacyRestClient.ExecuteEndpoint($"/api/accounts/{hashedAccountId}/transactions/{result.FirstOrDefault().Year}/{result.FirstOrDefault().Month}", HttpStatusCode.OK);
        }

        [Then(@"endpoint api/accounts/\{hashedAccountId}/levy  can be accessed")]
        public async Task ThenEndpointLevyAccountsHashedAccountIdCanBeAccessed()
        {
            var hashedAccountId = objectContext.GetHashedAccountId();
            await innerApiLegacyRestClient.ExecuteEndpoint($"/api/accounts/{hashedAccountId}/levy", HttpStatusCode.OK);
        }

        [Then(@"endpoint api/accounts/\{hashedAccountId}/levy/\{year}/\{month} can be accessed")]
        public async Task ThenEndpointLevyAccountsHashedAccountIdLevyYearMonthCanBeAccessed()
        {
            var hashedAccountId = objectContext.GetHashedAccountId();
            var response = await innerApiLegacyRestClient.ExecuteEndpoint($"/api/accounts/{hashedAccountId}/levy");
            var result = JsonConvert.DeserializeObject<ICollection<LevyDeclaration>>(response.Content);
            await innerApiLegacyRestClient.ExecuteEndpoint($"/api/accounts/{hashedAccountId}/levy/{result.FirstOrDefault().PayrollYear}/{result.FirstOrDefault().PayrollMonth}", HttpStatusCode.OK);
        }

        [Then(@"endpoint api/accounts/\{hashedAccountId}/transfers/connections can be accessed")]
        public async Task ThenEndpointTransferConnectionsHashedAccountIdCanBeAccessed()
        {
            var hashedAccountId = objectContext.GetHashedAccountId();
            await innerApiLegacyRestClient.ExecuteEndpoint($"/api/accounts/{hashedAccountId}/transfers/connections", HttpStatusCode.OK);
        }


        [Then(@"das-employer-accounts-api /ping endpoint can be accessed")]
        public async Task ThenDas_Employer_Accounts_ApiPingEndpointCanBeAccessed()
        {
            await innerApiRestClient.ExecuteEndpoint("/ping", HttpStatusCode.OK);
        }
    }
}
