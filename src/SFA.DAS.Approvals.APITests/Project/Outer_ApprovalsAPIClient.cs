

namespace SFA.DAS.Approvals.APITests.Project;

public class Outer_ApprovalsAPIClient(ObjectContext objectContext, Outer_ApiAuthTokenConfig config) : Outer_BaseApiRestClient(objectContext, config.Apim_SubscriptionKey)
{
    protected override string ApiName => "/learnerdata";

}
