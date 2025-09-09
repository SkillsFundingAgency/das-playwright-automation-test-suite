@approvals
@pasproviderrole
Feature: AP_PR_VW_ManageFunding

@regression
@Approvalproviderrole
Scenario: AP_PR_VW_Provider Roles Viewer Manage Funding Reservations
	Given the provider logs in as a Viewer
	When user naviagates to FundingForNonLevyEmployers page
	Then the user "cannot" reserve new funding
	And the user "cannot" delete existing reservervations
	And the user "cannot" add apprentices to an existing reservation
