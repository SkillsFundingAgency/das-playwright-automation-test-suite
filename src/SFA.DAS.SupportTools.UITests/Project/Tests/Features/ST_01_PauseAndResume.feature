﻿Feature: ST_01_PauseAndResume

@supporttools
@approvalssupport
@BulkUtility
Scenario: ST_01A_Pause Apprentice Records
	Given the SCP User is logged into Support Tools
	And Opens the Pause Utility
	And Search for Apprentices using following criteria
		| EmployerName			| ProviderName | Ukprn		| EndDate	| Uln | Status	| TotalRecords	|
		| COMPLIANCE LIMITED    |              | 10005310   |			|     |			|   100			| 
	
	When User selects all records and click on Pause Apprenticeship button
	Then User should be able to pause all the live records


@supporttools
@approvalssupport
@BulkUtility
Scenario: ST_01B_Resume Apprentice Records
	Given the SCP User is logged into Support Tools
	And Opens the Resume Utility
	And Search for Apprentices using following criteria
		| EmployerName			| ProviderName | Ukprn		| EndDate	| Uln | Status	| TotalRecords	|
		| COMPLIANCE LIMITED    |              | 10005310   |			|     |			|   100			| 
	
	When User selects all records and click on Resume Apprenticeship button
	Then User should be able to resume all the paused records
