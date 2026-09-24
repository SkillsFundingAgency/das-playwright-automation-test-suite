Feature: CA_EM_01_EmployerHub

Verify navigation and ensure no broken links across all cards, panels, and callouts on the "Employers" landing page.

@campaigns
@employer
@regression
Scenario Outline: Verify all navigation links on the Hire an apprentice page - <CardName>
	Given the user navigates to the Hire An Apprentice page
	When the user clicks on the employer card "<CardName>"
	Then the links are not broken

	Examples:
		| CardName                                            |
		# Considering hiring an apprentice?
		| Choose the right training for your business        |
		| Check who can do apprenticeship training            |
		| Understanding apprenticeship benefits and funding   |
		| Find funding and support                            |
		| Check what you’re responsible for                   |
		# Get started
		| Find the right training                             |
		| Choose a training provider                          |
		| Create an apprenticeship service account            |
		| Recruit your apprentice                             |
		# What's next?
		| Have an initial assessment                          |
		| Support your apprentice                             |
		| Plan what's next for your apprentice                |
		| Celebrate apprenticeships and upcoming events       |