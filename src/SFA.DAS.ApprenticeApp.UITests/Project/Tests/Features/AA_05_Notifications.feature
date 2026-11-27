Feature: AA_05_Notifications

Notifications are displayed under the Account hub

@apprenticeapp
@regression
Scenario: AA_05_Notifications are listed
	Given the apprentice has logged into the app
	When the apprentice skips the onboarding tour if present
	And the apprentice navigates to notifications via the account tab
	Then the notifications are displayed