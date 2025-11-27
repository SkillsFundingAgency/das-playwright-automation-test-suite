Feature: Fin_IA_AT_02_GetTransactions

@api
@employerfinanceapi
@regression
@innerapi
Scenario: GetTransactions
	Then endpoint api/accounts/{hashedAccountId}/levy/GetLevyForPeriod can be accessed
