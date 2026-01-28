namespace SFA.DAS.EmployerAccounts.APITests.Project.Tests.StepDefinitions
{

    [Binding]
    public class EmployerAccountsOuterAPISteps(ScenarioContext context) : BaseSteps(context)
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

    }
}
