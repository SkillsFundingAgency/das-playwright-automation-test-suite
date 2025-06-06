﻿
namespace SFA.DAS.EmployerPortal.UITests.Project;

public static class ObjectContextExtension
{
    #region Constants
    private static string UserCredsKey(int index) => $"usercreds_{index}";
    private const string LoggedInUserObject = "loggedinuserobject";
    private const string OrganisationNameKey = "organisationname";
    private const string TransferSenderOrganisationNameKey = "transfersenderorganisationname";
    private const string TransferReceiverOrganisationNameKey = "transferreceiverorganisationname";
    private const string RegisteredEmailAddress = "registeredemailaddress";
    private const string RecentlyAddedOrganisationName = "recentlyaddedorganisationname";
    private static string AdditionalOrganisation(int index) => $"additionalorganisationkey_{index}";

    #endregion

    internal static void SetLoginCredentials(this ObjectContext objectContext, string loginusername, string idOrUserRef, string organisationName)
    {
        objectContext.SetRegisteredEmail(loginusername);
        objectContext.SetOrganisationName(organisationName);
        objectContext.Replace(LoggedInUserObject, new LoggedInAccountUser { Username = loginusername, IdOrUserRef = idOrUserRef, OrganisationName = organisationName });
    }

    internal static void UpdateLoginIdOrUserRef(this ObjectContext objectContext, string loginusername, string idOrUserRef)
    {
        var loggedInAccountUser = objectContext.GetLoginCredentials();

        if (loggedInAccountUser.Username == loginusername)
        {
            loggedInAccountUser.IdOrUserRef = idOrUserRef;
        }
    }

    public static void SetOrganisationName(this ObjectContext objectContext, string organisationName) => objectContext.Replace(OrganisationNameKey, organisationName);
    public static void ReplaceTransferSenderOrganisationName(this ObjectContext objectContext, string organisationName) => objectContext.Replace(TransferSenderOrganisationNameKey, organisationName);
    public static void ReplaceTransferReceiverOrganisationName(this ObjectContext objectContext, string organisationName) => objectContext.Replace(TransferReceiverOrganisationNameKey, organisationName);

    public static void SetRecentlyAddedOrganisationName(this ObjectContext objectContext, string organisationName) => objectContext.Replace(RecentlyAddedOrganisationName, organisationName);
    public static void SetAdditionalOrganisationName(this ObjectContext objectContext, string secondAccountOrganisationName, int index) => objectContext.Set(AdditionalOrganisation(index), secondAccountOrganisationName);
    internal static void SetRegisteredEmail(this ObjectContext objectContext, string value) => objectContext.Replace(RegisteredEmailAddress, value);

    internal static List<UserCreds> SetOrUpdateUserCreds(this ObjectContext objectContext, string emailaddress, string password, List<AccountDetails> accDetails)
    {
        var usercreds = objectContext.GetAllUserCreds();

        if (GetUserCreds(usercreds, emailaddress) is null) objectContext.SetUserCreds(emailaddress, password, usercreds.Count);

        objectContext.UpdateUserCreds(emailaddress, accDetails);

        return objectContext.GetAllUserCreds();
    }
    public static string GetRegisteredEmail(this ObjectContext objectContext) => objectContext.Get(RegisteredEmailAddress);
    public static string GetHashedAccountId(this ObjectContext objectContext) => objectContext.GetAllUserCreds()[0].AccountDetails[0].HashedId;
    public static string GetDBAccountId(this ObjectContext objectContext) => objectContext.GetAllUserCreds()[0].AccountDetails[0].AccountId;
    public static string GetAleAgreementId(this ObjectContext objectContext) => objectContext.GetAllUserCreds()[0].AccountDetails[0].AleAgreementid;
    public static string GetOrganisationName(this ObjectContext objectContext) => objectContext.Get(OrganisationNameKey);
    public static string GetTransferSenderOrganisationName(this ObjectContext objectContext) => objectContext.Get(TransferSenderOrganisationNameKey);
    public static string GetTransferReceiverOrganisationName(this ObjectContext objectContext) => objectContext.Get(TransferReceiverOrganisationNameKey);
    public static string GetRecentlyAddedOrganisationName(this ObjectContext objectContext) => objectContext.Get(RecentlyAddedOrganisationName);
    public static string GetAdditionalOrganisationName(this ObjectContext objectContext, int index) => objectContext.Get(AdditionalOrganisation(index));
    internal static LoggedInAccountUser GetLoginCredentials(this ObjectContext objectContext) => objectContext.Get<LoggedInAccountUser>(LoggedInUserObject);

    private static void SetUserCreds(this ObjectContext objectContext, string emailaddress, string password, int index) =>
        objectContext.Replace(UserCredsKey(index), new UserCreds(emailaddress, password));

    private static List<UserCreds> GetAllUserCreds(this ObjectContext objectContext) => objectContext.GetAll<UserCreds>().ToList();

    private static UserCreds GetUserCreds(List<UserCreds> userCreds, string emailaddress) => userCreds.SingleOrDefault(x => x.EmailAddress == emailaddress);

    private static void UpdateUserCreds(this ObjectContext objectContext, string emailaddress, List<AccountDetails> accDetails)
    {
        var usercreds = GetUserCreds(objectContext.GetAllUserCreds(), emailaddress);

        foreach (var accdetail in accDetails)
        {
            var userAccountDetails = usercreds.AccountDetails;

            var index = userAccountDetails.Count;

            if (userAccountDetails.Any(x => x.AccountId == accdetail.AccountId)) continue;

            userAccountDetails.Add(accdetail);
        }
    }
}
