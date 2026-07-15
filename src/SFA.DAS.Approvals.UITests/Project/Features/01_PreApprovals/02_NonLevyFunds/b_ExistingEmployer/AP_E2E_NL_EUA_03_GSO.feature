@approvals
Feature: AP_E2E_NL_EUA_03_GSO

A brief end-to-end journey for learners on Growth & Skills (GSO) short courses  in reservations space
Key rules to validate:
1) Employer can create reservations using GSO standard but cannot add learner 
2) Provider can add GSO learners via ILR route by using existing reservations or by creating nre reservations on the fly

@regression
@e2escenarios
Scenario: AP_E2E_NL_EUA_03_GSO_Employer led reservations journey
	Given Provider submits ILR for following learners for a "NonLevy" employer:
			| CourseType   | CourseLevel | StartDateOffset	| DuationInDays	| LowerAgeLimit | UpperAgeLimit |
			| ShortCourses | N/A         |	-30				| 40			| 19			| 115			|
			| ShortCourses | N/A         |	0				| 10			| 19			| 115			|
	Given Employer creates a reservation with GSO standard and tries to add a learner to the reservation
	Then the Employer is blocked with error message 
	When Employer sends an empty cohort to the provider
	Then Provider can add learners to above cohort using existing and new reservations
	And Provider can approve the cohort and send it to the employer for final approval
	And the Employer can approve the cohort
	And the Employer can access live apprentice records under Manager Your Apprentices section

