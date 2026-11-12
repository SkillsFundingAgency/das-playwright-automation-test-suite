Feature: MS_03_AddDeleteStandard

@managingstandards
@managingstandardsadddeleteOralHealthapprenticeship
@managingstandards05
@regression
Scenario: MS_03A_Add_Delete_Standard
	Given the provider logs into portal
	When the provider is able to add the standard delivered in one of the training locations
	And the provider is able to delete the standard
	And the provider is able to add the standard using new contact details
	And the provider is able to delete the standard
	And the provider is able to add the standard delivered nationally
	And the provider is able to delete the standard
	Then the provider cannot add a standard that is restricted for the provider
	And the provider can add a standard that it restricted but with a date in the future

	