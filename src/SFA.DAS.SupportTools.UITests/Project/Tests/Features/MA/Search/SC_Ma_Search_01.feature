Feature: ST_Ma_Search_01

@regression
@supportconsole
@masupportconsole
Scenario: ST_Ma_Search_01 Search By Hashed account id, account name or PAYE scheme
	Given the User is logged into Support Tool
	And the user navigates to employer support page
	Then the user can search by Hashed account id, account name or PAYE scheme