﻿Feature: EPAO_RG_CA_03

@epao
@recordagrade
@regression
@epaoca1standard2version0option
Scenario: EPAO_RG_CA_03A - Certify an Apprentice as Pass - 1 Standard - multiple Version - No Options
	Given the Assessor User is logged into Assessment Service Application
	When the User certifies an Apprentice as 'pass' using 'apprentice' route
	Then the User can navigates to record another grade
	And the Assessment is recorded as 'pass'

@epao
@recordagrade
@regression
@epaoca1standard2version0option
Scenario: EPAO_RG_CA_03B - Certify an Apprentice as Fail to Pass - 1 Standard - multiple Version - No Options
	Given the Assessor User is logged into Assessment Service Application
	When the User certifies an Apprentice as 'fail' using 'apprentice' route
	Then the User can navigates to record another grade
	And the Assessment is recorded as 'fail'
	When the User certifies same Apprentice as pass using 'apprentice' route
	Then the Assessment is recorded as 'pass'
