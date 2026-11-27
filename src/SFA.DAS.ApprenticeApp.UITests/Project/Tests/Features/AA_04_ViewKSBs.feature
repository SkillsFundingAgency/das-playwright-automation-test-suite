Feature: AA_04_View KSBs

KSBs are listed and categorized!

@apprenticeapp
@regression
Scenario: AA_04_KSBs are listed and sub-tabs are verified
	Given the apprentice has logged into the app
	When the apprentice skips the onboarding tour if present
	And the apprentice clicks on the KSBs tab
	Then the apprentice is taken to the KSBs tab
	When the apprentice views the "Knowledge", "Skills", and "Behaviours" KSB sub-tabs
	Then each KSB sub-tab displays its respective items correctly