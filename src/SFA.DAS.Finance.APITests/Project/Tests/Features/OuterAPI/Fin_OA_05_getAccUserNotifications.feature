Feature: Fin_OA_05_getAccUserNotifications

@api
@employerfinanceapi
@regression
@outerapi
Scenario: Fin_OA_05 getAccUserNotifications

	Then endpoint /Accounts/{accountId}/users/which-receive-notifications can be accessed