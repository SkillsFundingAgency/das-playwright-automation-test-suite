@approvals
Feature: AP_E2E_LE_NUA_01_NewUserAccount

A short summary of the feature

@regression
@e2escenarios
@addlevyfunds
Scenario: AP_E2E_LE_NUA_01 Create Employer account Provider adds apprentices and approves then employer approves cohort
	#Given an Employer creates a Levy Account and Signs the Agreement
	Given an Employer creates a Levy Account
	#When the Employer create a cohort and send to provider to add apprentices
	#And the provider adds 2 apprentices approves them and sends to employer to approve
	#Then the Employer can approve the cohort
