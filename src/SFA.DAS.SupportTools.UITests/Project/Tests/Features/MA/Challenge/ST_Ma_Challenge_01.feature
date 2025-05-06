Feature: ST_Ma_Challenge_01


@regression
@supporttools
@levychallenge
Scenario: ST_Ma_Challenge_01 Submit correct details
	Given the Tier 1 User is logged into Support Tool
	And the User is on the Account details page
	When the user navigates to finance page
	Then the user is redirected to a challenge page
	When the user enters valid payscheme and levybalance
	And the user submits the challenge
	Then the user is redirected to finance page