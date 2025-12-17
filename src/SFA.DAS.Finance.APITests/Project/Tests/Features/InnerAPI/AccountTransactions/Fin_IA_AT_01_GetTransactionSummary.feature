Feature: Fin_IA_AT_01_GetTransactionSummary

@api
@employerfinanceapi
@regression
@innerapi
Scenario: Fin_IA_AT_01 GetTransactionSummary
	When send an api request GET api/accounts/{hashedAccountId}/transactions
	Then Verify the transactions api response with records fetch from DB
		| query |
		| getTransactions.sql |
