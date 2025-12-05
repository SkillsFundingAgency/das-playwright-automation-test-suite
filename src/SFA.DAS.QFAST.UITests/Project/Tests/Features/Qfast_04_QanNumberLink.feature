Feature: QFAST_04
	@regression
	@qfast
Scenario: QAN Number Link Verification
	Given the admin user log in to the portal
	When I select the Review newly regulated qualifications option	
	Then I verify that QAN number is a clickable link	
	Then I verify that the QAN number link opens the correct page in a new tab

	