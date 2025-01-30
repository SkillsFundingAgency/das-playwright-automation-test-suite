﻿Feature: ST_05_ValidateCombinationoffilters

@supporttools
@approvalssupportconsole
@BulkUtility
Scenario: ST_05_Validate Combination of filters
	Given the SCP User is logged into Support Tools
	When user opens Pause Utility
	Then following filters should return the expected number of TotalRecords
		| EmployerName				| ProviderName						| Ukprn		| EndDate		| Uln		 | Status			| TotalRecords	|
		| SCIENCE RESEARCH LIMITED	| EDUC8 TRAINING (ENGLAND) LIMITED	|			|				|			 | Live				|   3000		| 
		|							|									|			|				| 1405403089 | Paused			|   1			| 
		| METRO BANK PLC			|									| 10005310	|				|			 | Waiting to Start	|   80			| 
		|							|									|			| 2024-01-12	|			 | Any				|   400			| 
		| COMPLIANCE LIMITED		|									| 10005310	|				| 8305402974 | Live				|   0			| 
