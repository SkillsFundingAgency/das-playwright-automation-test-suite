using System.Threading.Tasks;

namespace SFA.DAS.API.Framework.RestClients;

public abstract class OuterJobs_BaseApiRestClient(ObjectContext objectContext, string code) : Outer_BaseApiRestClient(objectContext, code)
{
    protected override string ApiName => "/api";

    protected override async Task AddAuthHeaders() => await Task.CompletedTask;

    protected override void AddParameter() => restRequest.AddParameter("code", ApiAuthKey, ParameterType.QueryString);
}
