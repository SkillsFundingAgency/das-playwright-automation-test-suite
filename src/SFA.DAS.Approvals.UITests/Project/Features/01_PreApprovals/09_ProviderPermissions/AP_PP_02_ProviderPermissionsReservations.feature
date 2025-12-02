@approvals
Feature: AP_PP_02_ProviderPermissionsReservations

This test verifies that 'create cohort permissions' allow provider to create reservations for an employer.

@regression
@providerpermissions
Scenario: AP_PP_02_ProviderPermissions Reservations
	Given a NonLevy Employer grant create cohort permission to a provider
	Then the Provider can create Reservations
	When Employer revoke create cohort permission to a provider
	Then the Provider cannot create Reservations
