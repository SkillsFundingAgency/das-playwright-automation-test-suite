@approvals
@pasproviderrole
Feature: AP_PR_CO_ManageFunding


@regression
@Approvalproviderrole
Scenario: AP_PR_CO_Provider Roles Contributor Manage Funding Reservations
	Given the provider logs in as a Contributor
	When user naviagates to FundingForNonLevyEmployers page
	Then the user "can" reserve new funding
	And the user "can" delete existing reservervations
	And the user "can" add apprentices to an existing reservation