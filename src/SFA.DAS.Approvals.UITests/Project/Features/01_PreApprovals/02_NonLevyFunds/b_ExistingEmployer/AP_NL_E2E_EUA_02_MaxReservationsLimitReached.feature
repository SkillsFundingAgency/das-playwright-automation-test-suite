@approvals
Feature: AP_NL_E2E_EUA_02_MaxReservationsLimitReached

This feature validates funding retrictions when non-levy employer account has reached its max reservation limit.

@regression
@e2escenarios
Scenario: AP_NL_E2E_EUA_02_Block users to add apprentice when max reservation limit is reached
	Given the Employer logins using an existing NonLevy Account which has reached it max reservations limit
	When the Employer tries to add another apprentice to an existing cohort
	Then the Employer is blocked with a shutter page for existing cohort
	And the employer is blocked to create new reservations
	When the Provider tries to add another apprentice to an existing cohort
	Then the Provider is blocked with a shutter page for existing cohort
	And the Provider is blocked to create new reservations
