﻿@approvals
@pasproviderrole
Feature: AP_PR_AO_ManageApprentices


@regression
@Approvalproviderrole
Scenario: AP_PR_AO_Provider Roles Account Owner Manage Apprentices
	Given the provider logs in as a AccountOwner
	When user navigates to Manage Your Apprentices page
	Then the user can download csv file
	Then the user can view details of the apprenticeship on apprenticeship details page
	And the user can view changes via view changes link in the banner
	And the user "can" take action on review changes page
	And the user can view details of ILR mismatch via view details link in the ILR data mismatch banner
	Then the user "can" take action on details of ILR mismatch page by selecting any radio buttons on the page
	And the user can view details of ILR mismatch request restart via view details link in the ILR data mismatch banner
	And the user "can" take action on details of ILR mismatch request restart via view details link in the ILR data mismatch banner
	And the user can view review changes via review details link in the banner
	And the user "can" take action on review changes page
	Then the user can view view changes nonCoE page via view changes link in the banner
	And the user "can" take action on View changes on nonCoE page
	And the user "can" trigger change of employer journey using change link against the employer field
	And the user "can" edit an existing apprenticeship record by selecting edit apprentice link under manage apprentices


	
	
	