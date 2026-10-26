Feature: FAA_AC_01

@faa
@regression
@accessibility
Scenario: FAA_AC_01_01 - Apprentice journeys through the key Find an apprenticeship pages and actions
	Given appretince creates an account
	Then the apprentice can view and update their personal details in settings, including a validation error
	And the applicant can save and remove a randomly selected vacancy from search results
	When the apprentice has submitted their first application
	Then the apprentice answers the equality questions prompted after submitting an application
	And the apprentice can view and change their equality question answers in settings
	Then the Applicant can view submitted applications page
	And the Applicant can withdraw a random application

@faa
@regression
@accessibility
Scenario: FAA_AC_01_02 - Apprentice journeys through the application process and validation errors
	Given appretince creates an account
	When the apprentice has submitted their application with validation errors
