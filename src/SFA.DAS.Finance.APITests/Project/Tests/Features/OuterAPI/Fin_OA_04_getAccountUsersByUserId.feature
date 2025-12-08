Feature: Fin_OA_04_getAccountUsersByUserId

@api
@employerfinanceapi
@regression
@outerapi
Scenario: Fin_OA_04 getAccountUsersByUserId

	Then endpoint /Accounts/{accountId}/users/which-receive-notifications can be accessed