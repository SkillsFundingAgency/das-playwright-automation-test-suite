Feature: Fin_OA_getAccUserNotifications

@api
@employeraccountsapi
@regression
@innerapi
Scenario: getAccUserNotifications

	Then endpoint /Accounts/{accountId}/users/which-receive-notifications can be accessed