Feature: Apar_AD_01

@apar
@oldaparpadmin
@regression
Scenario: Apar_AD_01_Verify old apar login
	Given the provider logs into old apar admin portal
	And the user verifies links available in Manage Training Provider page
	