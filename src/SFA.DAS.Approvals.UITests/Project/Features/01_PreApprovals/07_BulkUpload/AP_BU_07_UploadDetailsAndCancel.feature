@approvals
Feature: AP_BU_07_UploadDetailsAndCancel
	

@regression
@newBUJourney
Scenario: AP_BU_07_Upload Details then Cancel
	Given the Employer logins using existing Levy Account
	When the employer create and send an empty cohort to the training provider to add learner details
	When Provider add 2 apprentice details using bulkupload 
	When Provider uploads the updated csv file
	Then Correct Information is displayed on review apprentices details page
	When Provider selects to Cancel bulk upload
	When Provider says No on Confirm cancel confirmation page
	Then Correct Information is displayed on review apprentices details page
	When Provider selects to Cancel bulk upload
	When Provider says Yes on Confirm cancel confirmation page
	When Upload cancelled confirmation page is displayed
