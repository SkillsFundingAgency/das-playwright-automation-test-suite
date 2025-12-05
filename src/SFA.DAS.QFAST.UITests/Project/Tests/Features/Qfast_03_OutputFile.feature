Feature: QFAST_03
	@regression
	@qfast
Scenario: QFAST Output File Verification
	Given the admin user log in to the portal
	When I select the Create an output file option
	And I verify error message Choose a publication date option. when date is not selected
	And I verify first option always has present date and download the file with present date
	And I select a publication date and generate the output file