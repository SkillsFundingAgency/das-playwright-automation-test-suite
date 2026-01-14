Feature: APAR_AD_NP_01

@rpadnp01
@apar
@oldroatpadmin
@deletetrainingprovider
@regression
Scenario: APAR_AD_NP_01_Add A New Training Provider as Main Provider
	Given the provider logs into old apar admin portal
	And the admin initates an application as Main provider
	And the user navigates to training providers page
	And the provider status should be set to On-Boarding
