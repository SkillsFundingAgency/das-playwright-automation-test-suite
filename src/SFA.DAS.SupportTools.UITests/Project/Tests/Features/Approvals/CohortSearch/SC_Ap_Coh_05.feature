﻿Feature: SC_Ap_Coh_05

@supportconsole
@approvalssupportconsole
Scenario: SC_Ap_Coh_05 - View Previous Training Providers
	Given the Tier 1 User is logged into Support Tool
	And the user navigates to employer support page
	And the User is on the Account details page
	When the User searches for a Cohort with Training provider history
	And the User clicks on 'View this cohort' button with Training provider history
	And the user chooses to view Uln of the Cohort with Training provider history
	And the ULN details page is displayed with Training provider history
	Then the Training provider history is displayed