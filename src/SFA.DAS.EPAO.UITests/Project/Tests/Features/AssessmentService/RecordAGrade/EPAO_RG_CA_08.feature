﻿Feature: EPAO_RG_CA_08

@epao
@recordagrade
@regression
@epaoca2standard2version1option
Scenario: EPAO_RG_CA_08A - Certify an Apprentice as Pass - multiple Standard - multiple Version - with Options
	Given the Assessor User is logged into Assessment Service Application
	When the User certifies an Apprentice as 'pass' using 'apprentice' route
	Then the User can navigates to record another grade
	And the Assessment is recorded as 'pass'


@epao
@recordagrade
@regression
@epaoca2standard2version1option
Scenario: EPAO_RG_CA_08B - Certify an Apprentice as Fail to Pass - multiple Standard - multiple Version - with Options
	Given the Assessor User is logged into Assessment Service Application
	When the User certifies an Apprentice as 'fail' using 'apprentice' route
	Then the User can navigates to record another grade
	And the Assessment is recorded as 'fail'
	When the User certifies same Apprentice as pass using 'apprentice' route
	Then the Assessment is recorded as 'pass'