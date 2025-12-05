Feature: AP_NL_E2E_EUA_02_MaxReservationsLimitReached

A short summary of the feature

@tag1
Scenario: AP_NL_E2E_EUA_02_Block users to add apprentice when max reservation limit is reached
	Given the Employer logins using an existing NonLevy Account which has reached it max reservations limit
	When the Employer tries to create reservation
	Then the Employer is blocked with a shutter page
	When the Employer tries to add another apprentice to an existing cohort
	Then the Employer is blocked with a shutter page for existing cohort
	When the Provider with create reservation permission logs in
	Then the Provider with suitable permissions tries to create reservation on behalf of this employer
	Then the Provider is blocked with a shutter page
	When the Provider tries to add another apprentice to an existing cohort
	Then the Provider is blocked with a shutter page for existing cohort
