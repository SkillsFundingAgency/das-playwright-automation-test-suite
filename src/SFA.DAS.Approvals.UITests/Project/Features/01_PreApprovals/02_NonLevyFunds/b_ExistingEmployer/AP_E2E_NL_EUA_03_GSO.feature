Feature: AP_E2E_NL_EUA_03_GSO

A brief end-to-end journey for learners on Growth & Skills (GSO) short courses  in reservations space
Key rules to validate:
1) Employer can create reservations using GSO standard but cannot add learner 
2) Provider can create reservations using GSO standard and only add learner via ILR route


@tag1
Scenario: AP_E2E_NL_EUA_03_GSO_Employer led reservations journey
	Given Provider submits ILR for following learners for a "NonLevy" employer:
			| CourseType   | CourseLevel | StartDate   | DuationInDays	| LowerAgeLimit | UpperAgeLimit |
			| ShortCourses | N/A         | 2026-05-01  | 40				| 19			| 115			|
			| ShortCourses | N/A         | 2026-05-01  | 40				| 19			| 115			|
	Given Employer creates a reservation with GSO standard and tries to add a learner to the reservation
	Then the Employer is blocked with error message 
	When Employer sends an empty cohort to the provider
	#And Provider select reservation created by the employer and add a learner via ILR route
	#And provider add another learner to the same cohort via Auto Reservation
	#Then Provider can approve the cohort and send it to the employer for final approval
	#And Employer can review and approve the cohort

