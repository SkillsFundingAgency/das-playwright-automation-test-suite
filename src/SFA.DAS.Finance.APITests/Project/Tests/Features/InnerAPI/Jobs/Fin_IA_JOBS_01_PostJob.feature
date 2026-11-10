Feature: Fin_IA_JOBS_01_PostJob

@api
@employerfinanceapi
@regression
@innerapi
Scenario: Fin_IA_JOBS_01 Post a job and validate DB data
	Given post a new job via api
	When find the job in JobRun table
	Then verify the job in JobRun table with the data posted via api
