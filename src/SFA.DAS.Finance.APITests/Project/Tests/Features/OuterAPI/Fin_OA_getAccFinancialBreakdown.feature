Feature: Fin_OA_getAccFinancialBreakdown

@api
@employeraccountsapi
@regression
@innerapi
Scenario: getAccFinancialBreakdown

	Then endpoint /Transfers/{accountId}/financial-breakdown can be accessed