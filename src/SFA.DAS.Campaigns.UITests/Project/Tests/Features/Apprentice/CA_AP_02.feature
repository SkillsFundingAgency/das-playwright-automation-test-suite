Feature: CA_AP_02_CheckIsAnApprenticeshipRightForYouPage

Check navigation and links on the "Is an apprenticeship right for you?" page.

@campaigns
@apprentice
@regression
Scenario: CA_AP_02_CheckIsAnApprenticeshipRightForYouPage
	Given the user navigates to Is An Apprenticeship Right For You Page
	Then the links are not broken