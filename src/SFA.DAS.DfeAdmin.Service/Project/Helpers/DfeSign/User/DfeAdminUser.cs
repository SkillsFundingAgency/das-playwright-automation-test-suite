﻿using SFA.DAS.Login.Service.Project.Helpers;

namespace SFA.DAS.DfeAdmin.Service.Project.Helpers.DfeSign.User;

public abstract class DfeAdminUser(string adminServiceName) : NonEasAccountUser
{
    public string AdminServiceName { get; init; } = adminServiceName;

    public override string ToString() => $"{base.ToString()}, ServiceName:'{AdminServiceName}'";
}

public class AsAssessor1User : DfeAdminUser
{
    public AsAssessor1User() : base("asassessor1") { }
}

public class AsAssessor2User : DfeAdminUser
{
    public AsAssessor2User() : base("asassessor2") { }
}

public class VacancyQaUser : DfeAdminUser
{
    public VacancyQaUser() : base("vacancyqa") { }
}

public class AsAdminUser : DfeAdminUser
{
    public AsAdminUser() : base("asadmin") { }
}

public class AanAdminUser : DfeAdminUser
{
    public AanAdminUser() : base("aanadmin") { }
}

public class AanSuperAdminUser : DfeAdminUser
{
    public AanSuperAdminUser() : base("aansuperadmin") { }
}

public class SupportToolTier1User : DfeAdminUser
{
    public SupportToolTier1User() : base("supporttooltier1") { }
}

public class SupportToolTier2User : DfeAdminUser
{
    public SupportToolTier2User() : base("supporttooltier2") { }
}

public class SupportToolScpUser : DfeAdminUser
{
    public SupportToolScpUser() : base("supporttoolscp") { }
}

public class SupportToolScsUser : DfeAdminUser
{
    public SupportToolScsUser() : base("supporttoolscs") { }
}
