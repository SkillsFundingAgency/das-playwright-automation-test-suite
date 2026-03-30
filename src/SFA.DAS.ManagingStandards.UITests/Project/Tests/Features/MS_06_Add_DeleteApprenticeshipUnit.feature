Feature: MS_06_AddDeleteApprenticeshipUnit

@managingstandards
@managingstandardsadddeleteapprenticeshipunit
@managingstandards06
@regression
Scenario: MS_06A_Add_Delete_ApprenticeshipUnit_TrainingLocation

	Given the provider logs into portal
	When the provider is able to add the ApprenticeshipUnit delivered in one of the training locations


@managingstandards
@managingstandardsadddeleteapprenticeshipunit
@managingstandards07
@regression
Scenario: MS_06B_Add_Delete_ApprenticeshipUnit_employerLocation

	Given the provider logs into portal
	When the provider is able to add the ApprenticeshipUnit delivered in employers locations


	
@managingstandards
@managingstandardsadddeleteapprenticeshipunit
@managingstandards07
@regression
Scenario: MS_06C_Add_Delete_ApprenticeshipUnit_Online

	Given the provider logs into portal
	When the provider is able to add the ApprenticeshipUnit delivered online
	