Feature: Fin_IA_AT_02_GetTransactions

@api
@employerfinanceapi
@regression
@innerapi
Scenario: Fin_IA_AT_02 GetTransactions
	Then endpoint api/accounts/{hashedAccountId}/transactions/GetTransactions can be accessed
