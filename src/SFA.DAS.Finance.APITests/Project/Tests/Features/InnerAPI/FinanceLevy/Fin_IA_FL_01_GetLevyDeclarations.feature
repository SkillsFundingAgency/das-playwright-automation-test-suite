Feature: Fin_IA_FL_01_GetLevyDeclarations

@api
@employerfinanceapi
@regression
@innerapi
Scenario: Fin_IA_FL_01 GetLevyDeclarations
	Then endpoint api/accounts/{hashedAccountId}/levy can be accessed
