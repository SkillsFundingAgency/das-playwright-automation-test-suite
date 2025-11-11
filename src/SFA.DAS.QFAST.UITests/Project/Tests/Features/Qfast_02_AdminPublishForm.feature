Feature: QFAT_02

@regression
@qfast
Scenario: QFAST Admin Publish Form
	Given the admin user log in to the portal
	When I select the Create a submission form option
	And I create a new submission form and publish the form 
		
	#And I publish the created form
	#Then I should see the form published successfully message