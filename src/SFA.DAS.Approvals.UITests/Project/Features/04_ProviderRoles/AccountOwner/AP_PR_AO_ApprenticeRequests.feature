@approvals
@pasproviderrole
Feature: AP_PR_AO_ApprenticeRequests


@regression
@Approvalproviderrole
Scenario: AP_PR_AO_Provider Roles Account Owner Apprentice Requests
Given the provider logs in as a AccountOwner
When user navigates to Apprentice requests page
Then the user can view apprentice details from items under section: "Ready for review"
Then the user can view apprentice details from items under section: "With employers" 
Then the user can view apprentice details from items under section: "Drafts"
Then the user can view apprentice details from items under section: "With transfer sending employers"
And the user can bulk upload apprentices
Then the user can create a cohort by selecting learners from ILR
Then the user can edit email address of the apprentice before approval
And the user can send a cohort to employer 
And the user can delete an apprentice in a cohort
And the user can delete a cohort
