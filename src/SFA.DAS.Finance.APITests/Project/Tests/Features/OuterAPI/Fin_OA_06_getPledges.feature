Feature: Fin_OA_06_getPledges

@api
@employerfinanceapi
@regression
@outerapi
Scenario: Fin_OA_06 getPledges

	Then endpoint /Pledges?accountId={accountId} can be accessed