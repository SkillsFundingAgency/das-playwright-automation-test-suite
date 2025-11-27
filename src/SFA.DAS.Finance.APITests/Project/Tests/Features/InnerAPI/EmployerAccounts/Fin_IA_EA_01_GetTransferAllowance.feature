Feature: Fin_IA_EA_01_GetTransferAllowance

@api
@employerfinanceapi
@regression
@innerapi
Scenario: GetTransferAllowance
	Then endpoint api/accounts/{hashedAccountId}/transferAllowance can be accessed
