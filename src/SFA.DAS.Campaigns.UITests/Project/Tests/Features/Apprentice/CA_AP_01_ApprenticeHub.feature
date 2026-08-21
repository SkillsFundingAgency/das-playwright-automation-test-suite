Feature: CA_AP_01_ApprenticeHub

Verify navigation and ensure no broken links across all cards and key callouts on the "Become an apprentice" landing page.

@campaigns
@apprentice
@regression
Scenario Outline: Verify all navigation links on the Become an apprentice page - <CardName>
	Given the user navigates to the Become An Apprentice page
	When the user clicks on the apprentice card "<CardName>"
	Then the links are not broken

	Examples:
		| CardName                                   |
		# First steps to becoming an apprentice
		| Is an apprenticeship right for you?        |
		| Browse by interest                         |
		| Getting an apprenticeship                  |
		| Apprentice pay and future salary           |
		| Get £3,000 if you've been in care          |
		| Find an apprenticeship                     |
		# What to expect during your apprenticeship
		| Preparing for your apprenticeship          |
		| Off-the-job (OTJ) training                 |
		| Knowledge, skills and behaviours (KSBs)    |
		| Apprenticeship assessments                 |
		# Support to achieve your apprenticeship
		| Get support with your apprenticeship       |
		| Apprenticeship rights and responsibilities |
		| Connect and network with other apprentices |
		| Download Your Apprenticeship app           |