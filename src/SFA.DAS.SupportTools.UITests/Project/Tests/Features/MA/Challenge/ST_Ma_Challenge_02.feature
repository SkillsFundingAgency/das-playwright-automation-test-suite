﻿Feature: ST_Ma_Challenge_02


@regression
@supporttools
@levychallenge
Scenario: ST_Ma_Challenge_02 Submit incorrect details
	Given the Tier 1 User is logged into Support Tool
	And the User is on the Account details page
	When the user navigates to finance page
	Then the user is redirected to a challenge page
	When the user enters invalid payscheme
	But enters correct levybalance
	When the user submits the challenge
	Then the user should see the error message Incorrect information entered.
