Feature: QFAT_02

@regression
@qfast
Scenario: QFAST Admin Publish Form
	Given the admin user log in to the portal
	When I select the Create a submission form option
	And I create a new submission form and publish the form 

@regression
@qfast
Scenario: AO user creates a funding application
	Given the aO user log in to the portal	
	And I create a new funding application and submit the application