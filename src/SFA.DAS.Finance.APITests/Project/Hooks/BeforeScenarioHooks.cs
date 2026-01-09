using SFA.DAS.Finance.APITests.Project.Helpers;
using SFA.DAS.Finance.APITests.Project.Helpers.SqlDbHelpers;
using SFA.DAS.Finance.APITests.Project.Helpers.SqlHelpers;

namespace SFA.DAS.Finance.APITests.Project.Hooks;

[Binding]
public class BeforeScenarioHooks(ScenarioContext context)
{

    [BeforeScenario(Order = 45)]
    public async Task SetUpHelpers()
    {
        var dbConfig = context.Get<DbConfig>();

        var objectContext = context.Get<ObjectContext>();

        var accountsHelper = new AccountsSqlDataHelper(objectContext, dbConfig);
        context.Set(accountsHelper);
        context.SetRestClient(new Inner_EmployerFinanceApiRestClient(objectContext, context.Get<Inner_ApiFrameworkConfig>()));

        var employerFinanceSqlDbHelper = new EmployerFinanceSqlHelper(context.Get<ObjectContext>(), dbConfig);

        // Prefer account details from the Accounts helper membership view (more relevant test data)
        var membershipResult = await accountsHelper.GetTestAccountDetailsFromDB();

        if (membershipResult != null && membershipResult.Count >= 4 && !string.IsNullOrWhiteSpace(membershipResult[2]))
        {
            var accountIdFromMembership = membershipResult[2];
            var hashedAccountIdFromMembership = membershipResult[3];

            // Set UserRef and email for scenarios that depend on them
            try
            {
                var userRef = membershipResult[0];
                var email = membershipResult[1];
                if (!string.IsNullOrWhiteSpace(userRef)) context.Set(userRef, "UserRef");
                if (!string.IsNullOrWhiteSpace(email)) context.Set(email, "email");
            }
            catch
            {
                // ignore if membershipResult shape is unexpected
            }

            objectContext.SetAccountId(accountIdFromMembership);
            // Set both ObjectContext and ScenarioContext keys so feature placeholders can use either value
            if (!string.IsNullOrWhiteSpace(hashedAccountIdFromMembership))
            {
                objectContext.SetHashedAccountId(hashedAccountIdFromMembership);
                context.Set(hashedAccountIdFromMembership, "hashedAccountId");
            }
            else
            {
                await employerFinanceSqlDbHelper.SetHashedAccountId(accountIdFromMembership);
                // try to retrieve from objectContext after setting
                var setHash = objectContext.GetHashedAccountId();
                if (!string.IsNullOrWhiteSpace(setHash)) context.Set(setHash, "hashedAccountId");
            }

            // also set numeric account id into ScenarioContext
            context.Set(accountIdFromMembership, "accountId");
        }
        else
        {
            // Fallback to existing behaviour
            var accountid = await employerFinanceSqlDbHelper.SetAccountId();
            await employerFinanceSqlDbHelper.SetHashedAccountId(accountid);

            // populate ScenarioContext keys from fallback values
            context.Set(accountid, "accountId");
            var fallbackHash = objectContext.GetHashedAccountId();
            if (!string.IsNullOrWhiteSpace(fallbackHash)) context.Set(fallbackHash, "hashedAccountId");
        }

        // Ensure EmpRef is set when available
        await employerFinanceSqlDbHelper.SetEmpRef();
    }
}