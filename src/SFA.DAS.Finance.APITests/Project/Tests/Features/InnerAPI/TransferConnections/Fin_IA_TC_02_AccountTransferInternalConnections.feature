Feature: Fin_IA_TC_02_AccountTransferConnections

@api
@employerfinanceapi
@regression
@innerapi
Scenario: Fin_IA_TC_01 AccountTransferConnections
	Then endpoint /api/accounts/internal/{accountId}/transfers/connections can be accessed