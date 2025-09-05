@approvals
Feature: AP_E2E_LE_NUA_01_NewUserAccount

A short summary of the feature

@regression
@e2escenarios
@addlevyfunds
Scenario: AP_E2E_LE_NUA_01 Create Employer account Provider adds apprentices and approves then employer approves cohort
	Given an Employer creates a Levy Account
	And the employer has 1 apprentice ready to start training
	When the new Employer create a cohort and send to provider to add apprentices
	Then the provider adds apprentice details, approves the cohort and sends it to the employer for approval
	Then the Employer can approve the cohort

