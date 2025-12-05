Feature: Apar_AD_02_ChangeProviderDetails

@apar
@rpadaparproviderdetails
@oldroatpadmin
@regression
Scenario: Apar_AD_02_UpdateProviderDetails
	Given the provider logs into old apar admin portal
	And the user navigates to training providers page
	And the user updated the training provider route status to Active
	And the user updated the training provider route status to Active but not taking on apprentices
	And the user updated the training provider route status to On-boarding
	And the user updated the training provider route status to Removed
	And the user updated the training provider route status to Active
	And the user updated the training provider type to Employer provider
	And the user updated the training provider type to Supporting provider
	And the user updated the training provider type to Main provider
	And the user updated the training provider Organisation type to Police
	And the user updated the training provider Organisation type to NHS Trust
	And the user updated the training provider Organisation type to Local authority
	And the user updated the training provider type to Main Provider
	And the user updated the training provider apprenticeship units to No
	And the user updated the training provider apprenticeship units to Yes
	And the user updated the training provider type to Supporting provider
	And the user cannot update the training provider type to Main provider without offering apprenticehsips or apprenticeship units
	And the user updated the training provider type to Supporting provider
	And the user cannot update the training provider type to Employer provider without offering apprenticehsips or apprenticeship units
	