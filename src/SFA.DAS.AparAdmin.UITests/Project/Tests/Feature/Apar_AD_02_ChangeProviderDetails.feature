Feature: Apar_AD_02_ChangeProviderDetails

@apar
@oldaparpadmin
@regression
Scenario: Apar_AD_02_UpdateStatus
	Given the provider logs into old apar admin portal
	And the user navigates to training providers page
	And the user updated the training provider route status to Active
	And the user updated the training provider route status to Active but not taking on apprentices
	And the user updated the training provider route status to On-boarding
	And the user updated the training provider route status to Removed
	