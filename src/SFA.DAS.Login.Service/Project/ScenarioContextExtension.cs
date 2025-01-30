﻿using SFA.DAS.Framework;
using SFA.DAS.Login.Service.Project.Helpers;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace SFA.DAS.Login.Service.Project
{
    public static class ScenarioContextExtension
    {
        public static void SetNonEasLoginUser<T>(this ScenarioContext context, T value) => SetUser(context, value);

        public static void SetNonEasLoginUser<T>(this ScenarioContext context, List<T> value)
        {
            foreach (T item in value) SetUser(context, item);
        }

        public static async Task SetFAAPortaluser(this ScenarioContext context, List<FAAPortalUser> users)
        {
            var signInIds = await new CandidateAccountStubLoginSqlDataHelper(context.Get<ObjectContext>(), context.Get<DbConfig>()).GetSignInIds(users.Select(x => x.Username).ToList());

            for (int i = 0; i < users.Count; i++)
            {
                users[i].IdOrUserRef = signInIds[i].signInId;

                users[i].FirstName = signInIds[i].firstName;

                users[i].LastName = signInIds[i].lastName;

                users[i].MobilePhone = signInIds[i].mobilePhone;

                SetUser(context, users[i]);
            }
        }

        public static async Task SetEPAOAssessorPortalUser(this ScenarioContext context, List<EPAOAssessorPortalUser> users)
        {
            var signInIds = await new AssessorStubLoginSqlDataHelper(context.Get<ObjectContext>(), context.Get<DbConfig>()).GetSignInIds(users.Select(x => x.Username).ToList());

            for (int i = 0; i < users.Count; i++)
            {
                users[i].IdOrUserRef = signInIds[i].signInId;

                users[i].FullName = signInIds[i].displayName;

                SetUser(context, users[i]);
            }
        }

        public static async Task SetApprenticeAccountsPortalUser(this ScenarioContext context, List<ApprenticeUser> users)
        {
            var signInIds = await new ApprenticeAccountsStubLoginSqlDataHelper(context.Get<ObjectContext>(), context.Get<DbConfig>()).GetSignInIds(users.Select(x => x.Username).ToList());

            for (int i = 0; i < users.Count; i++)
            {
                users[i].IdOrUserRef = signInIds[i].signInId;

                users[i].FirstName = signInIds[i].firstName;

                users[i].LastName = signInIds[i].lastName;

                users[i].Id = signInIds[i].id;

                SetUser(context, users[i]);
            }
        }

        public static async Task SetEasLoginUser(this ScenarioContext context, List<EasAccountUser> users)
        {
            var notNullUsers = users.Where(x => x != null).ToList();

            if (notNullUsers.Count == 0) return;

            var legalentities = await context.GetAccountLegalEntities(notNullUsers.Select(x => x.Username).ToList());

            for (int i = 0; i < notNullUsers.Count; i++)
            {
                notNullUsers[i].IdOrUserRef = legalentities[i].idOrUserRef;

                notNullUsers[i].LegalEntities = legalentities[i].listoflegalEntities;

                SetUser(context, notNullUsers[i]);
            }
        }

        public static T GetUser<T>(this ScenarioContext context) => context.Get<T>(Key<T>());

        public static async Task<List<(List<string> listoflegalEntities, string idOrUserRef)>> GetAccountLegalEntities(this ScenarioContext context, List<string> username)
        {
            var accountDetails = await new EasAccountsSqlDataHelper(context.Get<ObjectContext>(), context.Get<DbConfig>()).GetAccountDetails(username);

            return accountDetails.Select(x => (x.listoflegalEntities.Select(y => RegexHelper.ReplaceMultipleSpace(y)).ToList(), x.idOrUserRef)).ToList();
        }

        private static void SetUser<T>(ScenarioContext context, T data) => context.Set(data, data == null ? Key<T>() : Key(data.GetType()));

        private static string Key<T>() => Key(typeof(T));

        private static string Key(Type t) => t.FullName;
    }
}
