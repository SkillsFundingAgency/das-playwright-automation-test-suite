Feature: RAA_E_OV_01

@regression
@raaemployer
Scenario: RAA_E_OV_01 - Employer creates offline vacancy and QA approves
	When the Employer creates an Offline vacancy
	Then the Reviewer Approves the vacancy