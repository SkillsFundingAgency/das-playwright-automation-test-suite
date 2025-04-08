Feature: ST_Ap_Coh_02

@supportconsole
@approvalssupportconsole
Scenario: ST_Ap_Coh_02 - Invalid Cohort search
	Given the User is logged into Support Tool
	And the user navigates to employer support page
	And the User is on the Account details page
	When the User searches with a invalid Cohort Ref
	Then appropriate Cohort error message is shown to the user
	When the User searches with a invalid Cohort Ref having special characters
	Then appropriate Cohort error message is shown to the user