namespace SFA.DAS.EmployerAccounts.APITests.Project
{
    public static class ObjectContextExtension
    {
        private const string AccountIdKey = "accountid";
        private const string AccountHashedIdKey = "accounthashedid";
        private const string EmpRefKey = "empref";
        private const string UserRefKey = "userref";
        private const string LegalEntityIdKey = "legalentityid";
        private const string PayeSchemeRef = "payeschemeref";       
        internal static void SetLegalEntityId(this ObjectContext objectContext, string value) => objectContext.Replace(LegalEntityIdKey, value);
        internal static void SetHashedAccountId(this ObjectContext objectContext, string value) => objectContext.Replace(AccountHashedIdKey, value);
        internal static void SetAccountId(this ObjectContext objectContext, string value) => objectContext.Replace(AccountIdKey, value);
        internal static void SetEmpRef(this ObjectContext objectContext, string value) => objectContext.Replace(EmpRefKey, value);
        internal static void SetUserRef(this ObjectContext objectContext, string value) => objectContext.Replace(UserRefKey, value);

        internal static void SetPayeSchemeRef(this ObjectContext objectContext, string value) => objectContext.Replace(PayeSchemeRef, value);

        internal static string GetHashedAccountId(this ObjectContext objectContext) => objectContext.Get(AccountHashedIdKey);
        internal static string GetAccountId(this ObjectContext objectContext) => objectContext.Get(AccountIdKey);
        internal static string GetEmpRef(this ObjectContext objectContext) => objectContext.Get(EmpRefKey);
        internal static string GetUserRef(this ObjectContext objectContext) => objectContext.Get(UserRefKey);
        internal static string GetPayeSchemeRefId(this ObjectContext objectContext) => objectContext.Get(PayeSchemeRef);
        internal static string GetLegalEntityId(this ObjectContext objectContext) => objectContext.Get(LegalEntityIdKey);
        
    }
}
