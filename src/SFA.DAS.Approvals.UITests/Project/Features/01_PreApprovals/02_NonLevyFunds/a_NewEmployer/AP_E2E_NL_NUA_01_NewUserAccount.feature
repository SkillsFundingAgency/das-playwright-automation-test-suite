@approvals
Feature: AP_E2E_NL_NUA_01_NewUserAccount

A short summary of the feature

@regression
@e2escenarios
@addnonlevyfunds
@providerpermissions
Scenario: AP_E2E_NL_NUA_01 Create NonLevy Employer account | Provider adds apprentices and approves | then employer approves cohort
	Given The User creates "NonLevy" Employer account and sign an agreement
	And the employer has 1 apprentice ready to start training
	When the employer create and send an empty cohort to the training provider to add learner details
	Then provider cannot add apprentices as they do not have permissions to create reservations
	When the employer grants provider permissions to add apprentices
	Then the provider can add apprentice details and approve the cohort
	And the Employer can approve the cohort
	And the Employer can access live apprentice records under Manager Your Apprentices section


