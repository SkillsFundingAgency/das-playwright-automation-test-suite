@approvals
Feature: AP_E2E_LE_EUA_04_GSO

A brief end-to-end journey for learners on Growth & Skills (GSO) short courses 
It starts by pushing data into outer api or event in the N-Service bus, that mimicks SLD pushing ILR data into AS.
Then it continues with provider creating a cohort from that data and sending it to employer for approval.
Finally, employer approves that cohort and apprentice record is created in Learning Db.

@regression
@e2escenarios
Scenario: AP_E2E_LE_EUA_04_GSO Provider add learners on short course and employer approves it
	Given Provider submits ILR for following learners for a "Levy" employer:
			| CourseType   | CourseLevel | StartDate   | DuationInDays	| LowerAgeLimit | UpperAgeLimit |
			| ShortCourses | N/A         | 2026-04-01  | 40				| 19			| 115			|
	Then a record is created in LearnerData Db for each learner
	When Provider sends an apprentice request (cohort) to the employer by selecting same apprentices	
	Then Commitments Db is updated with respective LearnerData Id
	When the Employer approves the apprentice request (cohort)
	#Then LearnerData Db is updated with respective Apprenticeship Id
	#Then Apprenticeship record is created in Learning Db
	Then Provider can access live apprentice records under Manager Your Apprentices section
	#And xyz are read-only for the short courses apprentices
