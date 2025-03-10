﻿namespace SFA.DAS.EmployerPortal.UITests.Project.Helpers;

public class LoginCredentialsHelper(ObjectContext objectContext)
{
    public bool IsLevy { get; private set; }

    internal void SetLoginCredentials(string username, string password, string organisationName, bool isLevy = false)
    {
        objectContext.SetLoginCredentials(username, password, organisationName);

        IsLevy = isLevy;
    }

    public void SetIsLevy() => IsLevy = true;

    public LoggedInAccountUser GetLoginCredentials() => objectContext.GetLoginCredentials();
}
