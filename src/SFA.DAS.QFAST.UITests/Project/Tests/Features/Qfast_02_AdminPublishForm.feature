Feature: QFAT_02

@regression
@qfast
Scenario: QFAST Admin Publish Form and AO Create Funding Application
	Given the admin user log in to the portal
	When I select the Create a form option
	And I create a new submission form and publish the form 
	And I Sign out from the portal
# AO user creates a funding application
	Given the ao user2 user log in to the portal	
	And I create a new funding application and submit the application
	And I validate status is In review for RAD Advanced Vocational Graded Examination in Dance application
	When I Sign out from the portal
# Qfasu user assgin reviewer for the application
	Given the admin user log in to the portal
	When I select the Review applications for funding option
	And I assign aodp TestAdmin1 and aodp TestAdmin2 as reviewer for RAD Advanced Vocational Graded Examination in Dance application
	And I validate List of Qualifications approved for funding is a link and opens in a new tab and validate URL is https://www.qualifications.education.gov.uk/Home/FurtherInformation
	And I Sign out from the portal
# Qfau user change the status to On hold
	Given the admin user log in to the portal
	When I select the Review applications for funding option
	And I change the funding application status to Put application on hold for RAD Advanced Vocational Graded Examination in Dance application
	When I Sign out from the portal
# AO user validate Withdraw application option is availale when application status is On hold
	Given the ao user2 user log in to the portal
	And I validate status is On hold for RAD Advanced Vocational Graded Examination in Dance application
	When I withdraw the funding application
# AO user validates Application status is Withdrawn	
	Then I validate as an ao user application status is Withdrawn for RAD Advanced Vocational Graded Examination in Dance
	When I Sign out from the portal

	Given the admin user log in to the portal
	When I select the Review applications for funding option
	Then I validate as an admin application status is Withdrawn for RAD Advanced Vocational Graded Examination in Dance
	When I Sign out from the portal	

	Given the ofqual user log in to the portal
	Then I validate as an ofqual application status is Withdrawn for RAD Advanced Vocational Graded Examination in Dance
	When I Sign out from the portal

	Given the ifate user log in to the portal
	Then I validate as an ifate application status is Withdrawn for RAD Advanced Vocational Graded Examination in Dance
	When I Sign out from the portal	

	# Only Users belongs to an organisation can apply for funding for the qualifications associated with the organisation.
	# Ao user belongs to = Lantra National Training Organisation 
	# Ao user2 belongs to = Royal Academy of Dance 
	# The Qan and Title used in this test case belongs to Royal Academy of Dance organisation. 
	# Hence, only Ao user2 can apply for funding and Ao user will not be able to apply for funding for the qualification associated with Royal Academy of Dance organisation.

	Given the ao user user log in to the portal	
	And I create a new funding application on behalf of different organisation
	When I Sign out from the portal