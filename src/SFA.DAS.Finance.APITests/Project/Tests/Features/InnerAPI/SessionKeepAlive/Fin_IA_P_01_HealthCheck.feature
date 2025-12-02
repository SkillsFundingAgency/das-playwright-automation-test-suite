Feature: Fin_IA_P_01_HealthCheck

@api
@employerfinanceapi
@regression
@innerapi
Scenario: Fin_IA_P_01 ApiHealthCheck
	Then endpoint das-employer-finance-api /ping can be accessed
