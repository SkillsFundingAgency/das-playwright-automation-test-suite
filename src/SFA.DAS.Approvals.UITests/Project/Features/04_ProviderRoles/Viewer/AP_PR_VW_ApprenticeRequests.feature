@approvals
@pasproviderrole
Feature: AP_PR_VW_ApprenticeRequests


@regression
@Approvalproviderrole
Scenario: AP_PR_VW_Provider Roles Viewer Apprentice Requests
Given the provider logs in as a Viewer
When user navigates to Apprentice requests page
Then the user can view apprentice details from items under section: "Ready for review"
Then the user can view apprentice details from items under section: "With employers" 
Then the user can view apprentice details from items under section: "Drafts"
Then the user can view apprentice details from items under section: "With transfer sending employers"
And the user cannot start add apprentice journey
And the user cannot edit email address of the apprentice in an existing cohort
And the user cannot send an existing cohort to employer 
And the user cannot add another apprentice to a cohort
And the user cannot delete an apprentice in an existing cohort
And the user cannot delete an existing cohort
