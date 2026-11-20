@approvals
Feature: AP_BU_01_UploadDetailsOnSingleCohort

@regression
@newBUJourney
@addlevyfunds
Scenario: AP_BU_01_Upload Details On Single Cohort

	Given the Employer logins using existing Levy Account
	When the employer create and send an empty cohort to the training provider to add learner details
    When Provider add 2 apprentice details using bulkupload
	When Provider uploads the updated csv file
	Then Correct Information is displayed on review apprentices details page
	When User selects to upload an amended file
	When Provider selects Yes on confirmation for upload an amended file
	When Provider uploads another file
	Then Correct Information is displayed on review apprentices details page
	When User selects to upload an amended file through link
	When Provider selects No on confirmation for upload an amended file
	Then Correct Information is displayed on review apprentices details page
	When Provider selects to save all but don't send to employer
