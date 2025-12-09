@approvals
Feature: AP_PP_01_ProviderPermission

This test verifies that an employer can grant and revoke 'cohort creation' permissions to a provider

@regression
@providerpermissions
Scenario: AP_PP_01 Employer grant and revoke Create Cohort permission to a provider
	Given a Levy Employer grant create cohort permission to a provider
	Then Provider can Create Cohort
	When Employer revoke create cohort permission to a provider
	Then Provider cannot Create Cohort