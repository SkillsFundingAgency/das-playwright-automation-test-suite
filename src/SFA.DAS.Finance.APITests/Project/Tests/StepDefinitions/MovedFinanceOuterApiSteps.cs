using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using SFA.DAS.Finance.APITests.Project.Helpers;
using SFA.DAS.Finance.APITests.Project.Helpers.SqlHelpers;

namespace SFA.DAS.Finance.APITests.Project.Tests.StepDefinitions
{
    [Binding]
    public class MovedFinanceOuterApiSteps
    {
        private readonly Inner_EmployerFinanceApiRestClient _innerApiRestClient;
        private readonly ScenarioContext _scenarioContext;
        private readonly ObjectContext _objectContext;
        private readonly Outer_EmployerFinanceApiHelper _apiHelper;
        private readonly StepHelper _stepHelper;

        public MovedFinanceOuterApiSteps(ScenarioContext context)
        {
            _scenarioContext = context;
            _innerApiRestClient = context.GetRestClient<Inner_EmployerFinanceApiRestClient>();
            _objectContext = context.Get<ObjectContext>();
            _apiHelper = new Outer_EmployerFinanceApiHelper(context);
            _stepHelper = new StepHelper(_scenarioContext, _objectContext, _apiHelper);
        }

        [Given(@"an employer account (can|cannot) receive notifications")]
        public async Task GivenAnEmployerAccountCanOrCannotReceiveNotifications(string canOrCannot) => await _stepHelper.PopulateExpectedUserNotification(canOrCannot);

        [When(@"endpoint /Accounts/\{accountId\}/users/which-receive-notifications is called")]
        public Task WhenEndpointAccountsAccountIdUsersWhichReceiveNotificationsIsCalled() => _stepHelper.CallGetNotificationRequestSaveResponse();

        [Then(@"the response body should contain valid account details")]
        public Task ThenTheResponseBodyShouldContainValidAccountDetails() => Task.Run(() => _stepHelper.AssertApiMatchesSqlRow());




        [Given(@"an employer account with signed version")]
        public async System.Threading.Tasks.Task GivenAnEmployerAccountWithSignedVersion() => await _stepHelper.PopulateExpectedSignedAgreementVersion();

        [When(@"endpoint /Accounts/\{accountId\}/users/minimum-signed-agreement-version is called")]
        public async Task WhenEndpointAccountsAccountIdUsersMinimumSignedAgreementVersionIsCalled() => await _stepHelper.CallAccountIdUsersMinimumSignedAgreement();

        [Then(@"the response body should contain valid account details signed aggrement details")]
        public void ThenTheResponseBodyShouldContainValidAccountDetailsSignedAggrementDetails() => Task.Run(() => _stepHelper.AssertApiMatchesSqlRow());
        



        [Given(@"get the user with linked accounts")]
        public async System.Threading.Tasks.Task GetUserWithLinkedAccounts()
        {
            await _stepHelper.PopulateExpectedUserWithLinkedAccounts();
        }

        [When(@"endpoint request GET /AccountUsers/\{\{UserRef\}\}/accounts is called")]
        public async System.Threading.Tasks.Task WhenEndpointGetAccountUsersAccountsIsCalled()  => await _stepHelper.CallGetAccountUserAccountsByUserRef();
        

        [Then(@"account details should retrun for given user")]
        public void ThenAccountDetailsShouldRetrunForGivenUser() => Task.Run(() => _stepHelper.AssertApiMatchesSqlRow());

    }
}
