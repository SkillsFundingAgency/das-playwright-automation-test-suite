Feature: ApiGetAccountTransactionsHashedAccountIdTransactionsYearMonth

@api
@employeraccountsapi
@regression
@innerapi
Scenario: EMPACC_API_07_ApiGetAccountTransactionsHashedAccountIdTransactionsYearMonth
	Then endpoint api/accounts/{hashedAccountId}/transactions/{year}/{month} can be accessed	
