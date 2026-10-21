Feature: APAR_AD_NP_03

@rpadnp03
@apar
@oldroatpadmin
@deletetrainingprovider
@regression
Scenario: APAR_AD_NP_03_Add A New Training Provider as Supporting Provider
    Given the provider logs into old apar admin portal
	When the admin initates an application as Supporting provider
	And the user navigates to training providers page
	Then the provider status should be set to Active
