Feature: AP_E2E_NL_EUA_03_GSO

A brief end-to-end journey for learners on Growth & Skills (GSO) short courses  in reservations space
Key rules to validate:
1) Employer can create reservations using GSO standard but cannot add learner 
2) Provider can create reservations using GSO standard and only add learner via ILR route


@tag1
Scenario: AP_E2E_NL_EUA_03_GSO_Employer creates reservations and provider adds learners via ILR route
	Given Provider submits ILR for following learners for a "NonLevy" employer:
			| CourseType   | CourseLevel | StartDate   | DuationInDays	| LowerAgeLimit | UpperAgeLimit |
			| ShortCourses | N/A         | 2026-05-01  | 40				| 19			| 115			|
			| ShortCourses | N/A         | 2026-06-01  | 10				| 19			| 115			|
	Given Employer creates a reservation with GSO standard and tries to add a learner to the reservation
	#Then the Employer is blocked with a shutter page prompting to send the cohort to provider for adding learners
	#When Employer sends an empty cohort to the provider
	#And Provider adds a learner to the cohort via Existing Reservation > ILR route
	#And provider add another GSO learner to the same cohort via Auto Reservation route
	#Then Provider can approve the cohort and send it to the employer for final approval
	#And Employer can review and approve the cohort

