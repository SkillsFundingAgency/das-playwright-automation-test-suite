﻿Feature: EPAO_AD_AmendCert_01

@epao
@recordagrade
@epaoadmin
@regression
Scenario: EPAO_AD_AmendCert_01 Amend a Certificate
	Given the Assessor User is logged into Assessment Service Application
	And the User certifies an Apprentice as 'pass' with 'employer' route and records a grade
	And the Admin all roles user is logged into the Admin Service Application 
	And the Admin can search using learner uln
	When the Admin amends the certificate
	Then the ticket reference 'INC123456' and reason for amend 'Incorrect apprentice details' can be entered
	And the amend can be confirmed
	And the certificate history contains the incident number 'INC123456' and amend reason 'Incorrect apprentice details'