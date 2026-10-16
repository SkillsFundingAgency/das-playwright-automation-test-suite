@postapprovals
Feature: AP_EmpChk_01_DisplayEmploymentStatus

This test verify (part of) employment verification checks. 

Employment verification checks follow below workflow:
1. Upon full approval -> new EmploymentVerification request in comt-db
2. Some Azure data factory job runs every so often and gets above newly Approved apprenticeships and adds records to the EmploymentCheck DB
3. Employment check CRON job runs - picks up check and calls HMRC logs status 
4. Commitments CRON job runs - checks the status of Employment check ones and updates accordingly

Important:
- Commitments job is currently scheduled to run every 5 mins in test environments
- Integration b/w Employment Checks service and HMRC api is only available in PROD. 

Data Requirements:
	- Employer Account: <LevyUser>			<--- please refer to user secrets file for the actual value
	- Provider Account: <ProviderConfig>	<--- please refer to user secrets file for the actual value
	- FirstName:DoNotUse_TestData
	- LastName: EmploymentChecks
	- ULN: any
	- StartDate: -6 months in the past
	- EndDate: +6 months in the future
	- Training Course: Any GSO/short-course/AU
	- insert a row in [echk-db] > [Business].[EmploymentCheck]

@regression
Scenario: AP_EmpChk_01_Display Employment Verfication Status
	Given a Live apprenticeship record exists for learner with Firstname: "DoNotUse_TestData" and LastName: "EmploymentChecks"
	When Employement verification checks are "pending" for the apprentice
	And EmployerVerificationSyncSchedule job is executed and value of employment status remains 0 in commitments db
	Then employer verifies that Employment Verification section is "blank" 
	When Employement verification checks are "failed" for the apprentice
	And EmployerVerificationSyncSchedule job is executed and set value of employment status as 4 into commitments db
	Then employer verifies that Employment Status is "Not verified - missing PAYE scheme and invalid NINO" 
	When Employement verification checks are "completed" for the apprentice
	And EmployerVerificationSyncSchedule job is executed and set value of employment status as 3 into commitments db
	Then employer verifies that Employment Status is "Not employed" 
	Then provider verifies that Employment Status is "Not employed" 

	
