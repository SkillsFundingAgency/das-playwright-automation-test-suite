
Feature: Fin_OA_05_getAccUserNotifications_new

@api
@employerfinanceapi
@regression
@outerapi
Scenario Outline: Fin_OA_05 getAccUserNotifications_new

Given an employer account <receive> receive notifications
When endpoint /Accounts/{accountId}/users/which-receive-notifications is called
Then the response body should contain valid account details

Examples:
	| receive |
	| can     |
	# | cannot  |
