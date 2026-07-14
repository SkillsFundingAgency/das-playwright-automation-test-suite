@approvals
Feature: AP_NL_E2E_EUA_03_ReservationWindowRules

# This test focuses on verifying cohort statuses as they move b/w employer and provider
# this test is for non levy funding route and will cover the following scenarios
# 1. Provider tries to add a new learner using auto reservation route with start date not matching course dates
# 2. Provider tries to add a new learner using manage funding reservation route with start date not matching course dates

@regression
@e2escenarios
Scenario: AP_NL_E2E_EUA_03 Reservations window rules validation
	
	# Auto reservation route
	When The Provider tries to add a new learner using details from table below
	| NewStartDate	 | NewEndDate	|
	| -9		     | 0		    |
	Then Provider Check Learner DetailsPage is stopped with an error message <ErrorOnStratDateIsNotMatchingCourseDates>
	
	# manage funding reservation route add learner
	When Provider logs into Provider-Portal
    And creates reservations for each learner
    When provider use above reservation and learner details to create a cohort
	Then Provider Check Learner DetailsPage is stopped with an error message <ErrorOnStratDateIsNotMatchingCourseDates>
 Examples:
 		|ErrorOnStratDateIsNotMatchingCourseDates      										|	
 	    |Training start date must be between the funding reservation dates 				    |
