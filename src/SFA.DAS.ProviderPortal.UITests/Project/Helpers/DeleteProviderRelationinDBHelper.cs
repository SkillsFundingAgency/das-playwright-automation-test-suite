using SFA.DAS.EmployerPortal.UITests.Project;

namespace SFA.DAS.ProviderPortal.UITests.Project.Helpers;

public class DeleteProviderRelationinDbHelper(ScenarioContext context)
{
    private ObjectContext ObjectContext => context.Get<ObjectContext>();

    private ProviderConfig ProviderConfig => context.GetProviderConfig<ProviderConfig>();

    public async Task DeleteProviderRelation() => await context.Get<RelationshipsSqlDataHelper>().DeleteProviderRelation(ProviderConfig.Ukprn, ObjectContext.GetDBAccountId(), ObjectContext.GetRegisteredEmail());

    public async Task DeleteProviderRequest(List<string> requestId) => await context.Get<RelationshipsSqlDataHelper>().DeleteProviderRequest(requestId);

}
