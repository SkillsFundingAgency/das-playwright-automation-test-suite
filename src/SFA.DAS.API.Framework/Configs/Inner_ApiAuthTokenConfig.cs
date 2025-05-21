﻿namespace SFA.DAS.API.Framework.Configs;

public class Inner_ApiAuthTokenConfig
{
    public string ClientId { get; set; }

    public string ClientSecrets { get; set; }

    public string Tenant { get; set; }

    public string TenantId { get; set; }

    public string ApprenticeAccountsAppServiceName { get; set; }

    public string CoursesAppServiceName { get; set; }

    public string CommitmentsAppServiceName { get; set; }

    public string EmployerFinanceAppServiceName { get; set; }

    public string EmployerAccountsAppServiceName { get; set; }

    public string EmployerAccountsLegacyAppServiceName { get; set; }
}
