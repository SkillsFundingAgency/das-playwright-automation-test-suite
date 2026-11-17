@approvals
Feature: AP_BU_08_UploadDetailsOnBehalfOfNonLevyEmployer

@regression
@newBUJourney
Scenario: AP_BU_08_Upload Details On Behalf Of NonLevy Employer
	Given the provider logs into portal
	And  Provider uses BulkUpload to add 2 apprentice details for a non-levy employer into a non-existing cohort
	When Provider uploads the updated csv file
	Then Correct Information is displayed on review apprentices details page
	Then Provider approves the cohorts and send them to employer to approve 
	Then Save the cohort reference details to the learner
	When the Employer logins to NonLevy Account
	Then the Employer can approve the cohort
	Then the Employer can access live apprentice records under Manager Your Apprentices section
