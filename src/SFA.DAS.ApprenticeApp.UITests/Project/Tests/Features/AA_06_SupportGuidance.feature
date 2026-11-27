Feature: AA_06_Support and Guidance

Support and guidance articles are displayed under the Home tab

@apprenticeapp
@regression
Scenario: AA_06A_Support and guidance articles are listed
	Given the apprentice has logged into the app
	When the apprentice skips the onboarding tour if present
	And the apprentice is on the Home tab
	Then all support and guidance article links are displayed

@apprenticeapp
@regression
Scenario Outline: AA_06B_Apprentice can view and save articles across different categories
	Given the apprentice has logged into the app
	When the apprentice skips the onboarding tour if present
	And the apprentice is on the Home tab
	And the apprentice opens the "<CategoryName>" support category
	And the apprentice opens an article and saves it
	Then the article appears under "Your saved articles"
	When the apprentice removes the article from the saved list
	Then the article is removed from the saved articles

	Examples:
		| CategoryName                              |
		| Off-the-job (OTJ) training and hours      |
		| Mental health support                     |
		| Contact us                                |