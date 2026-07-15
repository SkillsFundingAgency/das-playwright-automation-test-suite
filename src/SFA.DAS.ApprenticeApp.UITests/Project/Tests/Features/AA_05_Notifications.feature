Feature: AA_05_Notifications

Notifications are displayed

@apprenticeapp
@regression
Scenario: AA_05_Notifications are listed
	Given the apprentice has logged into the app
	When the apprentice skips the onboarding tour if present
	And the apprentice clicks on the notifications tab
	Then the notifications are displayed