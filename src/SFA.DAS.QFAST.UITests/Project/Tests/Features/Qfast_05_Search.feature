Feature: Qfast_05
@regression
@qfast
Scenario: Fuzzy Search for a qualification
	Given the admin user log in to the portal
	When I select the Search for a qualification option
	And I validate the error messages when I search without entering correct details
	And I search for a qualification using partial title Elect and validate the search result has Qualifcation with title containing Electrical
	
	