Feature: AA_04_View KSBs

KSBs are listed!

@ApprenticeApp
@regression
Scenario: AA_04_KSBs are listed
	Given the apprentice has logged into the app
	When the apprentice skips the onboarding tour if present
	And the apprentice clicks on the KSBs tab
	Then the apprentice is taken to the KSBs tab