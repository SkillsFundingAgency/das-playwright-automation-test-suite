Feature: AA_02_YourApprenticeshipDetails

Your apprenticeship details are displayed

@apprenticeapp
@regression
Scenario: AA_02_Your apprenticeship details are displayed
	Given the apprentice has logged into the app
	When the apprentice skips the onboarding tour if present
	And the apprentice clicks on the account tab
	And the apprentice clicks on your apprenticeship details
	Then the apprenticeship details page is displayed