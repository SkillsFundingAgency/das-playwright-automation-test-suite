Feature: Fin_IA_TC_01_AccountTransferConnections

@api
@employerfinanceapi
@regression
@innerapi
Scenario: Fin_IA_TC_01 AccountTransferConnections
	Then endpoint /api/accounts/{hashedAccountId}/transfers/connections can be accessed
