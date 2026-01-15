@approvals
Feature: AP_PP_01_ProviderPermission

This test verifies that an employer can grant and revoke provider permissions to create cohorts and reservations.

@regression
@providerpermissions
Scenario: AP_PP_01a_ProviderPermissions Create Cohort
	Given a Levy Employer grant create cohort permission to a provider
	Then Provider can Create Cohort
	When a Levy Employer revoke create cohort permission to a provider
	Then Provider cannot Create Cohort


@regression
@providerpermissions
Scenario: AP_PP_01b_ProviderPermissions Reservations
	Given a NonLevy Employer grant create cohort permission to a provider
	Then the Provider can create Reservations
	When a NonLevy Employer revoke create cohort permission to a provider
	Then the Provider cannot create Reservations