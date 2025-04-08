Feature: ST_Ap_Coh_01

@supportconsole
@approvalssupportconsole
Scenario: ST_Ap_Coh_01 - View Cohort details
	Given the User is logged into Support Tool
	And the user navigates to employer support page
	And the User is on the Account details page
	When the User searches for a Cohort
	And the user chooses to view Uln of the Cohort 
	Then the ULN details page is displayed