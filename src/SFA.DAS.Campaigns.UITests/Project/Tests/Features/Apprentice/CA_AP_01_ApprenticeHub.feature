Feature: CA_AP_01_ApprenticeHub
Verify navigation and ensure no broken links across all stepper cards and callouts on the "Become an apprentice" landing page.

@campaigns @apprentice @regression
Scenario Outline: Verify all navigation links on the Become an apprentice page - <CardName>
	Given the user navigates to the Become An Apprentice page
	When the user clicks on the apprentice card "<CardName>"
	Then the links are not broken

	Examples:
		| CardName                                                 |
		# Section 1: Is an apprenticeship right for you?
		| Check if an apprenticeship is right for you              |
		| Browse by the type of work you’re interested in          |
		| See what you’ll be paid and your future salary           |
		| Understand what experience you need                      |
		| Get £3,000 if you've been in care                        |
		| Find an apprenticeship                                   |

		# Section 2: Getting an apprenticeship
		| Getting an apprenticeship                                |
		| Find help if you can’t get hired                         |

		# Section 3: Get ready for your apprenticeship
		| Prepare for your apprenticeship                          |
		| Get ready for off-the-job (OTJ) training                 |
		| Understand knowledge, skills and behaviours (KSBs)       |
		| Apprenticeship assessments                               |

		# Section 4: Help during your apprenticeship
		| Thinking about dropping out?                             |
		| Connect and network with other apprentices               |
		| Apprenticeship rights and responsibilities               |
		| Your Apprenticeship app                                  |
		| I have a problem at work or training                     |
		| Help with money                                          |
		| Mental health support                                    |
		| Get support for a disability or learning need            |
		| What comes after my apprenticeship?                      |