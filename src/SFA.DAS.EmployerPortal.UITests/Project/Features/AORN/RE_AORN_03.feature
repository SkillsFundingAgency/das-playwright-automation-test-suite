﻿@regression
@employerportal
@addnonlevyfunds
Feature: RE_AORN_03

Scenario: RE_AORN_03_Validate AORN route check details page links
	Given a User Account is created
	When the User is on the 'Check your details' page after adding PAYE details through AORN route
	Then choosing to change the AORN number displays 'Enter your PAYE scheme details' page
	And choosing to change the PAYE scheme displays 'Enter your PAYE scheme details' page
	And choosing to change the Organisation selected displays 'Search for your Organisation' page