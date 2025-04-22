﻿Feature: MS_01_ReviewYourDetails_ApproveStandard_EditTrainingLocations

@managingstandards
@managingstandards01
@regression
Scenario: MS_01A_Verify_Organisation_Details_And_Update_Contact_Details
	Given the provider logs into portal
	Then the provider verifies organisation details
	And the provider verifies provider overview
	And the provider updates contact details


@managingstandards
@managingstandards02
@regression
Scenario: MS_01B_Approve_DisApprove_Standard
	Given the provider logs into portal
	When the provider is able to approve regulated standard
	Then the provider is able to disapprove regulated standard

@managingstandards
@managingstandards03
@regression
Scenario: MS_01C_WhereCanYouDeliver_EditLocations_Standard
	Given the provider logs into portal
	When the provider is able to change the standard delivered in both not a national provider
	And the provider is able to change the standard delivered in one of the training locations
	And the provider is able to add the training locations
	And the provider is able to change the standard delivered at an employers location national provider
	And the provider is able to edit the regions
