﻿Feature: CA_ApprenticeHub_01

@campaigns
@apprentice
@regression
Scenario: CA_ApprenticeHub_01_Check Apprentice Hub Page Details
	Given the user navigates to the apprentice page
	Then the links are not broken
	And the video links are not broken
	And the fire it up tile card links are not broken