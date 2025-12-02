Feature: Fin_IA_AT_01_GetTransactionSummary

@api
@employerfinanceapi
@regression
@innerapi
Scenario: Fin_IA_AT_01 GetTransactionSummary
	Then endpoint api/accounts/{accountId}/transactions can be accessed
