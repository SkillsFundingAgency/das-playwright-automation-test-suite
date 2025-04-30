Feature: ST_Ma_Tier2_01


@regression
@supporttools
@supportnavigation
Scenario: ST_Ma_Tier2_01_Tier2Navigation To Finance page
	Given the Tier 2 User is logged into Support Tool
	And the user navigates to employer support page
	And the User is on the Account details page
	When the user navigates to finance page
	Then the user is redirected to finance page
	And the user can view levy declarations
	When the user navigates to finance page
	Then the user is redirected to finance page
	And the user can view transactions
