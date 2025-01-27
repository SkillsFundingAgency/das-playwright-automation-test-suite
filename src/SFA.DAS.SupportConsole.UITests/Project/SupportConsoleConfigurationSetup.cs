﻿using SFA.DAS.DfeAdmin.Service.Project.Helpers;
using SFA.DAS.DfeAdmin.Service.Project.Helpers.DfeSign.User;
using SFA.DAS.Login.Service.Project;

namespace SFA.DAS.SupportConsole.UITests.Project;

[Binding]
public class SupportConsoleConfigurationSetup(ScenarioContext context)
{
    [BeforeScenario(Order = 2)]
    public async Task SetUpSupportConsoleProjectConfiguration()
    {
        var configSection = context.Get<ConfigSection>();

        context.SetSupportConsoleConfig(configSection.GetConfigSection<SupportConsoleConfig>());

        var dfeAdminUsers = context.Get<FrameworkList<DfeAdminUsers>>();

        context.SetNonEasLoginUser(new List<NonEasAccountUser>
        {
            SetDfeAdminCredsHelper.SetDfeAdminCreds(dfeAdminUsers, new SupportConsoleTier1User()),
            SetDfeAdminCredsHelper.SetDfeAdminCreds(dfeAdminUsers, new SupportConsoleTier2User())
        });

        await Task.CompletedTask;
    }
}