Feature: Fin_IA_EA_01_GetTransferAllowance

@api
@employerfinanceapi
@regression
@innerapi
Scenario: Fin_IA_EA_01 GetTransferAllowance
	Then endpoint api/accounts/{hashedAccountId}/transferAllowance can be accessed
