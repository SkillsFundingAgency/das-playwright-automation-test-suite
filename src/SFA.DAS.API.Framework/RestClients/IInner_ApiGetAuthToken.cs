
using System.Threading.Tasks;

namespace SFA.DAS.API.Framework.RestClients;

public interface IInner_ApiGetAuthToken
{
    public Task<(string tokenType, string accessToken)> GetAuthToken(string appServiceName);
}
