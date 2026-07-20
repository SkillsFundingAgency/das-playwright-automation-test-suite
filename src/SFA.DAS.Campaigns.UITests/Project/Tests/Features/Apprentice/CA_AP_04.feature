Feature: CA_AP_04_CheckPreparingForAnApprenticeshipPage

Check navigation and links on the "Preparing for an apprenticeship" page.

@campaigns
@apprentice
@regression
Scenario: CA_AP_04_CheckPreparingForAnApprenticeshipPage
	Given the user navigates to Preparing For An Apprenticeship Page
	Then the links are not broken