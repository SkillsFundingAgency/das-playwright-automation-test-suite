Feature: ST_Ap_Coh_03

@supporttools
@approvalssupport
Scenario: ST_Ap_Coh_03 - Search for a unauthorised Cohort
	Given the User is logged into Support Tool
	And the user navigates to employer support page
	And the User is on the Account details page
	When the user tries to view another Employer's Cohort Ref
	Then unauthorised Cohort access error message is shown to the user