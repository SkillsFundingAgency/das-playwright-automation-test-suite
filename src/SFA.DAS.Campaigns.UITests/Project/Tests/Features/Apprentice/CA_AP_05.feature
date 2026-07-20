Feature: CA_AP_05_CheckCreateAccountToSearchAndApplyPage

Check navigation and links on the "Create an account to search and apply for apprenticeships" page.

@campaigns
@apprentice
@regression
Scenario: CA_AP_05_CheckCreateAccountToSearchAndApplyPage
	Given the user navigates to Create An Account To Search And Apply Page
	Then the links are not broken