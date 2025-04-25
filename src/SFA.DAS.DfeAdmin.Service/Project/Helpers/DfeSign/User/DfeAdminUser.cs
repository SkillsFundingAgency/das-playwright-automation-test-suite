using SFA.DAS.Login.Service.Project.Helpers;

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

public class SupportConsoleTier1User : DfeAdminUser
{
    public SupportConsoleTier1User() : base("supportconsoletier1") { }
}

public class SupportConsoleTier2User : DfeAdminUser
{
    public SupportConsoleTier2User() : base("supportconsoletier2") { }
}

public class SupportToolScpUser : DfeAdminUser
{
    public SupportToolScpUser() : base("supporttoolscp") { }
}

public class SupportToolScsUser : DfeAdminUser
{
    public SupportToolScsUser() : base("supporttoolscs") { }
}

public class AodpDfeAdminUser : DfeAdminUser
{
    public AodpDfeAdminUser() : base("aodpdfeuser1") { }

}

public class AodpDfeAdminUser1 : DfeAdminUser
{
    public AodpDfeAdminUser1() : base("aodpdfeuser2") { }

}

public class AodpAOUser : DfeAdminUser
{
    public AodpAOUser() : base("aodpaouser") { }

}

public class AodpAOUser1 : DfeAdminUser
{
    public AodpAOUser1() : base("aodpaouser1") { }

}

public class AodpAOUser2 : DfeAdminUser
{
    public AodpAOUser2() : base("aodpaouser2") { }

}

public class AodpIFATEUser : DfeAdminUser
{
    public AodpIFATEUser() : base("aodpifateuser") { }

}

public class AodpOFQUALUser : DfeAdminUser
{
    public AodpOFQUALUser() : base("aodpofqualuser") { }

}