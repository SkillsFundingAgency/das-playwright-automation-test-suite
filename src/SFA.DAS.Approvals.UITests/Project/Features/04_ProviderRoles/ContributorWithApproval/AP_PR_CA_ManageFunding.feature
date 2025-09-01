@approvals
@pasproviderrole
Feature: AP_PR_CA_ManageFunding

@regression
@Approvalproviderrole
Scenario: AP_PR_CO_Provider Roles Contributor With Approval Manage Funding Reservations
	Given the provider logs in as a ContributorWithApproval
	When user naviagates to FundingForNonLevyEmployers page
	Then the user "can" reserve new funding
	And the user "can" delete existing reservervations
	And the user "can" add apprentices to an existing reservation