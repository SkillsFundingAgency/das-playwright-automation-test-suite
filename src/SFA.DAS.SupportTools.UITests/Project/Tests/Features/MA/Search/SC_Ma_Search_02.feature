﻿Feature: SC_Ma_Search_02

@regression
@supportconsole
@masupportconsole
Scenario: SC_Ma_Search_02 Search By name or email address
	Given the User is logged into Support Console
	And the user navigates to employer support page
	Then the user can search by name or email address