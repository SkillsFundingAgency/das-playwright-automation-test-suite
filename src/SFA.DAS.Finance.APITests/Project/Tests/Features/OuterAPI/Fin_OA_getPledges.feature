Feature: Fin_OA_getPledges

@api
@employeraccountsapi
@regression
@innerapi
Scenario: getPledges

	Then endpoint /Pledges?accountId={accountId} can be accessed