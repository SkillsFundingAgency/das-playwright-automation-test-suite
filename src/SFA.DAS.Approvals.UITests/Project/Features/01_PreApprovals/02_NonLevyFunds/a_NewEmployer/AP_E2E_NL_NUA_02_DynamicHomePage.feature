@approvals
Feature: AP_E2E_NL_NUA_02_DynamicHomePage

A short summary of the feature

@regression
@e2escenarios
@addnonlevyfunds
@providerpermissions
Scenario: AP_E2E_NL_NUA_02_NonLevyEmployer reserves funding to add an apprentice from dynamic homepage journey
	
	Given The User creates "NonLevy" Employer account and sign an agreement
    And the employer has 1 apprentice ready to start training
	When  The NonLevyEmployer reserves funding for an apprenticeship course from reserved panel
	Then  The nonlevyemployer follows the link from dynamicHomePage to create a cohort and send it to the training Provider
	Then the provider adds apprentice details, select an existing reservation, approves the cohort and sends it to the employer for approval
	Then the NonLevyEmployer reviews and approves the cohort from dynamic homepage


