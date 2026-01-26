namespace SFA.DAS.EmployerAccounts.APITests.Project.Helpers;

public class Outer_EmployerAccountsHealthApiRestClient(ObjectContext objectContext) : Outer_HealthApiRestClient(objectContext, UrlConfig.OuterApiUrlConfig.Outer_EmployerFinanceHealthBaseUrl)
{

}
