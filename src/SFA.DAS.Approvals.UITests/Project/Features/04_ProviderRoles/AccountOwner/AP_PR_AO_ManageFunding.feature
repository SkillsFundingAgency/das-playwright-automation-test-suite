@approvals
@pasproviderrole
Feature: AP_PR_AO_ManageFunding


@regression
@Approvalproviderrole
Scenario: AP_PR_AO_Provider Roles Account Owner Manage Funding Reservations
	Given the provider logs in as a AccountOwner
	When user naviagates to FundingForNonLevyEmployers page
	Then the user "can" reserve new funding
	And the user "can" delete existing reservervations
	And the user "can" add apprentices to an existing reservation