@approvals
Feature: AP_E2E_LE_NUA_01_NewUserAccount

A new non-levy employer creates an employer account and initiate a cohort.
Provider add apprentice details and employer does the final approval.

@regression
@e2escenarios
@addlevyfunds
Scenario: AP_E2E_LE_NUA_01 Create Employer account Provider adds apprentices and approves then employer approves cohort
	Given an Employer creates a Levy Account
	And the employer has 1 apprentice ready to start training
	When the employer create and send an empty cohort to the training provider to add learner details
	Then the provider adds apprentice details, approves the cohort and sends it to the employer for approval
	And the Employer can approve the cohort

