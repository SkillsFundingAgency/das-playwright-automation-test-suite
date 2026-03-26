using SFA.DAS.Finance.APITests.Project.Helpers.SqlHelpers;
using System.Globalization;

namespace SFA.DAS.Finance.APITests.Project.Hooks;

[Binding]
public class AfterScenarioHooks(ScenarioContext context)
{
    [AfterScenario(Order = 999)]
    public async Task CleanUpInsertedStagingRecords()
    {
        var accountsHelper = context.Get<AccountsSqlDataHelper>();
        if (accountsHelper == null) return;

        var paymentId = GetContextValue("paymentId");
        if (!string.IsNullOrWhiteSpace(paymentId))
        {
            var escapedPaymentId = paymentId.Replace("'", "''");

            await accountsHelper.ExecuteSql($@"
DELETE FROM [employer_financial].[PaymentMetaDataStaging]
WHERE [PaymentId] = '{escapedPaymentId}';", useFinanceDb: true);

            await accountsHelper.ExecuteSql($@"
DELETE FROM [employer_financial].[PaymentStaging]
WHERE [PaymentId] = '{escapedPaymentId}';", useFinanceDb: true);
        }

        var transferId = GetContextValue("transferId");
        if (!string.IsNullOrWhiteSpace(transferId))
        {
            var escapedTransferId = transferId.Replace("'", "''");

            await accountsHelper.ExecuteSql($@"
DELETE FROM [employer_financial].[TransferStaging]
            WHERE [TransferId] = '{escapedTransferId}';", useFinanceDb: true);
        }

        var empRef = GetContextValue("englishFractionsEmpRef");
        if (!string.IsNullOrWhiteSpace(empRef))
        {
            var escapedEmpRef = empRef.Replace("'", "''");

            await accountsHelper.ExecuteSql($@"
DELETE FROM [employer_financial].[EnglishFraction]
WHERE EmpRef = '{escapedEmpRef}';", useFinanceDb: true);
        }
    }

    private string GetContextValue(string key)
    {
        try
        {
            if (!context.ContainsKey(key)) return string.Empty;

            var value = context[key];
            if (value == null) return string.Empty;

            if (value is System.IFormattable formattable)
            {
                return formattable.ToString(null, CultureInfo.InvariantCulture);
            }

            return value.ToString() ?? string.Empty;
        }
        catch
        {
            return string.Empty;
        }
    }
}
