﻿Feature: RE_EAN_03

@regression
@employerportal
Scenario: RE_EAN_03_Verify Login for Existing View user
	Given the Employer logins using existing view user account
	Then the user can not add an organisation
	And the user can not remove the organisation
	And the user can not add Payee Scheme
	And the user can not invite a team members
	And the user can not accept agreement
	And the user can not add an apprentices	