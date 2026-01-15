Feature: APAR_AD_NP_02

@rpadnp02
@apar
@oldroatpadmin
@deletetrainingprovider
@regression
Scenario: APAR_AD_NP_02_Add A New Training Provider as Employer Provider
    Given the provider logs into old apar admin portal
	And the admin initates an application as Employer provider
	And the user navigates to training providers page
	And the provider status should be set to On-Boarding
