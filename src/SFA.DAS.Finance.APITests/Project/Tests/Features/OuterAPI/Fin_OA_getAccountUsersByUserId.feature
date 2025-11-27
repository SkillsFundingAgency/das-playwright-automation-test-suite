Feature: Fin_OA_getAccountUsersByUserId

@api
@employeraccountsapi
@regression
@innerapi
Scenario: getAccountUsersByUserId

	Then endpoint /Accounts/{accountId}/users/which-receive-notifications can be accessed