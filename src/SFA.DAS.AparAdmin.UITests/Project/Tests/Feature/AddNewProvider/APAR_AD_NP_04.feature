Feature: APAR_AD_NP_04

@apar
@regression
Scenario: APAR_AD_NP_04_Atempt Adding A New Training Provider that does not exist
    Given the provider logs into old apar admin portal
	When the admin initates an application with an invalid UKPRN "11111111", error message is displayed
