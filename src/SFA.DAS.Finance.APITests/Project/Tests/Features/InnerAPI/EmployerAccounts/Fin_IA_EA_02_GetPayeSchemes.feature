Feature: View PAYE schemes for an employer account
	As a product owner
	I want PAYE schemes to be returned for a valid employer account
	So that users see the correct PAYE references in their experience

Scenario: Employer account returns its PAYE schemes
	Given a valid employer account with a PAYE reference
	When PAYE schemes are requested for that employer account
	Then the returned PAYE schemes include that PAYE reference
