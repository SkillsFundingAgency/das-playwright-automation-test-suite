Feature: QFAT_02

@regression
@qfast
Scenario: QFAST Admin Publish Form and AO Create Funding Application
	Given the admin user log in to the portal
	When I select the Create a form option
	And I create a new submission form and publish the form 
	And I Sign out from the portal
# AO user creates a funding application
	Given the aO user log in to the portal	
	And I create a new funding application and submit the application
	And I validate status is In review for Level 3 Award in Cricket Coaching application
	When I Sign out from the portal
# Qfau user change the status to On hold
	Given the admin user log in to the portal
	When I select the Review applications for funding option
	And I change the funding application status to Put application on hold for Level 3 Award in Cricket Coaching application
	When I Sign out from the portal
# AO user validate Withdraw application option is availale when application status is On hold
	Given the aO user log in to the portal
	And I validate status is On hold for Level 3 Award in Cricket Coaching application
	When I withdraw the funding application
# AO user validates Application status is Withdrawn	
	Then I validate as an ao user application status is Withdrawn for Level 3 Award in Cricket Coaching
	When I Sign out from the portal

	Given the admin user log in to the portal
	When I select the Review applications for funding option
	Then I validate as an admin application status is Withdrawn for Level 3 Award in Cricket Coaching
	When I Sign out from the portal
	

	Given the ofqual user log in to the portal
	Then I validate as an ofqual application status is Withdrawn for Level 3 Award in Cricket Coaching
	When I Sign out from the portal


	Given the ifate user log in to the portal
	Then I validate as an ifate application status is Withdrawn for Level 3 Award in Cricket Coaching
	When I Sign out from the portal	