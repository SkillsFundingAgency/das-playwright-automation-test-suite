﻿Feature: MS_02_AddTrainingVenues

@managingstandards
@managingstandards04
@regression
Scenario: MS_02A_Verify_Organisation_Details_And_Update_Contact_Details
	Given the provider logs into portal
	Then the provider is able to add a new training venue
	And the provider is able to update the new training venue
	