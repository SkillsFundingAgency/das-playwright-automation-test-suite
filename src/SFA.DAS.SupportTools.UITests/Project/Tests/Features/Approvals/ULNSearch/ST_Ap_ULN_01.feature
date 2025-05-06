Feature: ST_Ap_ULN_01

@supporttools
@approvalssupport
Scenario: ST_Ap_ULN_01 - View ULN details
	Given the User is logged into Support Tool
	And the user navigates to employer support page
	And the User is on the Account details page
	When the User searches for an ULN
	Then the ULN details are displayed