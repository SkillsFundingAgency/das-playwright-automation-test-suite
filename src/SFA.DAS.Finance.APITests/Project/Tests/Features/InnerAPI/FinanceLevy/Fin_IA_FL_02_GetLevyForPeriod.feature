Feature: Fin_IA_FL_02_GetLevyForPeriod

@api
@employerfinanceapi
@regression
@innerapi
Scenario: Fin_IA_FL_02 GetLevyForPeriod
	Then endpoint api/accounts/{hashedAccountId}/levy/GetLevyForPeriod can be accessed

