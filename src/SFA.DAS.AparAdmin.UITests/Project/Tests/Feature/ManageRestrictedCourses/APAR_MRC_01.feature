Feature: APAR_MRC_01

@aparmrc01
@apar
@regression
Scenario: APAR_MRC_01_Verify filter functionality on restricted courses
	Given the provider logs into old apar admin portal
	And Verifies the Filters functionality 
	When the user searches and filters for a course
	Then the user is able to verify results for the filters set
	// And the user is able to add multiple filters and clear all 