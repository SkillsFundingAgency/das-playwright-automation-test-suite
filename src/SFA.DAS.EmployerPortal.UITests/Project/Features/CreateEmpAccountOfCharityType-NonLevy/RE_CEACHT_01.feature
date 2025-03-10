﻿Feature: RE_CEACHT_01

@regression
@employerportal
@addnonlevyfunds
Scenario: RE_CEACHT_01_Create an Employer Account with Charity Type Org
	When a User Account is created
	And the User adds PAYE details
	And adds Charity Type Organisation details
	And the Employer Signs the Agreement
	Then ApprenticeshipEmployerType in Account table is marked as 0