Feature: Apar_AD_01_VerifyLinks

@apar
@oldaparpadmin
@regression
Scenario: Apar_AD_01_Verify old apar login
	Given the provider logs into old apar admin portal
	Then the user verifies links available in Manage Training Provider page
	