@approvals
@linkedScenarios
Feature: AP_E2E_EUA_04_Employer_Approval


@regression
@e2escenarios
	Scenario: AP_E2E_EUA_04 Provider sends cohort to employer for review then employer approves then provider approves
	Given new learner details are processed in ILR for 2 apprentices
	And Employer create and send an empty cohort to the training provider to add learner details
	When the provider adds apprentices along with RPL details and sends to employer to review
	Then the Employer sees the cohort in Ready to review with status of Ready for review
	And verify RPL details
	When the Employer approves the cohort and sends to provider
	Then the provider approves the cohorts