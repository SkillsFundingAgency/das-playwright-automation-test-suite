﻿using SFA.DAS.Login.Service.Project.Helpers;

namespace SFA.DAS.ProviderLogin.Service.Project;

public class ProviderConfig : NonEasAccountUser
{
    public string Ukprn { get; set; }

    public string Name { get; set; }

    public override string ToString() => $"{base.ToString()}, Ukprn:'{Ukprn}', ProviderName: '{Name}'";
}

public class DeleteCohortProviderConfig : ProviderConfig
{
    public string NoOfCohortToDelete { get; set; }

    public int DfeTimeOut { get; set; }
}

public class RplProviderConfig : ProviderConfig { }

public class ChangeOfPartyConfig : ProviderConfig
{
    public string NewProviderName { get; set; }
}

public class ProviderPermissionsConfig : ProviderConfig
{

}

public class PortableFlexiJobProviderConfig : ProviderConfig { }

public class PerfTestProviderPermissionsConfig : ProviderConfig { }

public class ProviderLoginUser : ProviderConfig { }

public class ProviderViewOnlyUser : ProviderConfig { }

public class ProviderContributorUser : ProviderConfig { }

public class ProviderContributorWithApprovalUser : ProviderConfig { }

public class ProviderAccountOwnerUser : ProviderConfig { }

public class EmployerTypeProviderAccount : ProviderConfig { }