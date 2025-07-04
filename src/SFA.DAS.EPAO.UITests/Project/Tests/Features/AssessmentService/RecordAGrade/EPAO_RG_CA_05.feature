﻿Feature: EPAO_RG_CA_05

@ignore
@epao
@recordagrade
@regression
@epaoca2standard1version0option
Scenario: EPAO_RG_CA_05A - Certify an Apprentice as Pass - multiple Standard - 1 Version - No Options
	Given the Assessor User is logged into Assessment Service Application
	When the User certifies an Apprentice as 'pass' using 'apprentice' route
	Then the User can navigates to record another grade
	And the Assessment is recorded as 'pass'

@ignore
@epao
@recordagrade
@regression
@epaoca2standard1version0option
Scenario: EPAO_RG_CA_05B - Certify an Apprentice as Fail to Pass - multiple Standard - 1 Version - No Options
	Given the Assessor User is logged into Assessment Service Application
	When the User certifies an Apprentice as 'fail' using 'apprentice' route
	Then the User can navigates to record another grade
	And the Assessment is recorded as 'fail'
	When the User certifies same Apprentice as pass using 'apprentice' route
	Then the Assessment is recorded as 'pass'