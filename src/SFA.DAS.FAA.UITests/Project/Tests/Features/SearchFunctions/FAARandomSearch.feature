Feature: FAARandomSearch

User searches for a vacancy using without populating search fields

@faa
@regression
Scenario: FAA_USFV_01 User searches for a vacancy at random and filters by location on search results page
	Given the candidate can login in to faa
	When the user does a search without populating search fields
	Then the user is presented with search results
	Then the user is presented with sort order as 'New'
	When the user does a where only search on search results page for 'Coventry'
	Then the user is presented with sort order as 'New'