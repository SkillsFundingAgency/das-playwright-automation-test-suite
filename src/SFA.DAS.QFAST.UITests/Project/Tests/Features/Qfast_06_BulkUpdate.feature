Feature: Qfast_06

@regression
@qfast
Scenario: Bulk update of qualifications - MVS1 - New Qualifications
	Given the admin user log in to the portal
	When I select the Review newly regulated qualifications option
	Then I bulk update the qualifications
	And I verify user is able to update the qualification manually

@regression
@qfast
Scenario: Bulk update of qualifications - MVS1 - Changed Qualifications
	Given the admin user log in to the portal
	When I select the Review qualifications with changes option
	Then I bulk update the qualifications
	And I verify user is able to update the qualification manually