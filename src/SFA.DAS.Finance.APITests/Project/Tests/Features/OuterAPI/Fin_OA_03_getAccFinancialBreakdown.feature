Feature: Fin_OA_03_getAccFinancialBreakdown

@api
@employerfinanceapi
@regression
@outerapi
Scenario: Fin_OA_03 getAccFinancialBreakdown

	Then endpoint /Transfers/{accountId}/financial-breakdown can be accessed