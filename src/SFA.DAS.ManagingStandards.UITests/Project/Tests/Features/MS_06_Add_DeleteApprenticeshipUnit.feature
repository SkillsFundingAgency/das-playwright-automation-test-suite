Feature: MS_06_AddDeleteApprenticeshipUnit

@managingstandards
@managingstandardsadddeleteaiapprenticeshipunit
@managingstandards06
@regression
Scenario: MS_06A_Add_Delete_ApprenticeshipUnit_TrainingLocation

	Given the provider logs into portal
	When the provider is able to add the ApprenticeshipUnit delivered in one of the training locations


@managingstandards
@managingstandardsadddeleteaiapprenticeshipunit
@managingstandards07
@regression
Scenario: MS_06B_Add_Delete_ApprenticeshipUnit_employerLocation

	Given the provider logs into portal
	When the provider is able to add the ApprenticeshipUnit delivered in employers locations


	
@managingstandards
@managingstandardsadddeleteaiapprenticeshipunit
@managingstandards07
@regression
Scenario: MS_06C_Add_Delete_ApprenticeshipUnit_Online

	Given the provider logs into portal
	When the provider is able to add the ApprenticeshipUnit delivered online

@managingstandards
@managingstandardsregulatedapprenticeshipunit
@managingstandards08
@regression
Scenario: MS_06D_Add_Delete_RegulatedApprenticeshipUnit
	Given the provider logs into portal
	Then the provider is able to add a regulated ApprenticeshipUnit
	And the provider is able to delete the regulated ApprenticeshipUnit