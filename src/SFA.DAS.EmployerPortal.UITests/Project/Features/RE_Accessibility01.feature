Feature: RE_E2E_ACC_Accessibility01
Navigation journey through EAS and PAS

@employerportal
@accessibility
Scenario: RE_E2E_ACC_01 Create Account by completing one task at a time
	Given user logs into stub
	Then User is prompted to enter first and last name
	And user can amend name before submitting it
	When user adds name successfully to the account
	Then user can change user details from the task list
	And user <DoesAddPAYE> add PAYE details
	And user <CanSetAccountName> set account name and <DoesSetAccountName>
	And user <CanSignEmployerAgreement> accept the employer agreement and <DoesSignEmployerAgreement>
	And user <CanAddTrainingProvider> add training provider and <DoesAddTrainingProvider>, the user <DoesGrantProviderPermissions> grant training provider permissions
	Then user logs out and log back in
	And user can resume employer registration journey
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