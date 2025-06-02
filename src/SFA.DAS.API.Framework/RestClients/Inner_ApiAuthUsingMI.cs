﻿using System.Threading.Tasks;

namespace SFA.DAS.API.Framework.RestClients;

public class Inner_ApiAuthUsingMI(Inner_ApiFrameworkConfig config) : IInner_ApiGetAuthToken
{
    private string AccessToken = string.Empty;

    private DateTime AcquiredTime = DateTime.MinValue;

    public async Task<(string tokenType, string accessToken)> GetAuthToken(string appServiceName)
    {
        if (string.IsNullOrEmpty(AccessToken) || (DateTime.Now > AcquiredTime.AddMinutes(50)))
        {
            AccessToken = await AzureTokenService.GetAppServiceAuthToken(config.GetResource(appServiceName));

            AcquiredTime = DateTime.Now;
        }

        return ("Bearer", AccessToken);
    }
}
