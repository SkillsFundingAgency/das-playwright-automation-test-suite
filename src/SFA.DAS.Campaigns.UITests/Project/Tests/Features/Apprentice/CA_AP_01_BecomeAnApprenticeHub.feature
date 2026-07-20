Feature: CA_AP_01_BecomeAnApprenticeHub

Verify navigation and ensure no broken links across all cards and key callouts on the "Become an apprentice" landing page.

@campaigns
@apprentice
@regression
Scenario Outline: Verify all navigation links on the Become an apprentice page - <CardName>
	Given the user navigates to the Become An Apprentice page
	When the user clicks on the apprentice card "<CardName>"
	Then the links are not broken

	Examples:
		| CardName                                                    |
		# Main Callout Panel
		| Find an apprenticeship                                      |
		# First Steps Cards
		| About apprenticeships                                       |
		| Is an apprenticeship right for you?                         |
		| Apprentice pay and future salary                            |
		| Create an account to search and apply for apprenticeships   |
		| Browse by interest                                          |
		# What to Expect Cards
		| Apprenticeship assessments: what you need to know           |
		| Off-the-job (OTJ) training: what you need to know           |
		| Apprentice training                                         |
		| Preparing for an apprenticeship                             |
		# Support Cards
		| Join the Apprenticeship Ambassador Network as an apprentice |