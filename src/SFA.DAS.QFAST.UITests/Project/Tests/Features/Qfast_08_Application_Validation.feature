Feature: Qfast_08
# Only Users belongs to an organisation can apply for funding for the qualifications associated with the organisation.
	# Ao user belongs to = Lantra National Training Organisation 
	# Ao user2 belongs to = Royal Academy of Dance 
	# The Qan and Title used in this test case belongs to Royal Academy of Dance organisation. 
	# Hence, only Ao user2 can apply for funding and Ao user will not be able to apply for funding for the qualification associated with Royal Academy of Dance organisation.
@regression
@qfast
Scenario: Application Validation
	Given the ao user log in to the portal	
	And I create a new funding application on behalf of different organisation
	When I Sign out from the portal