Feature: CA_AP_BrowseByTypeOfWork
Verify navigation to sector pages from the Browse by type of work page.

@campaigns
@apprentice
Scenario Outline: Verify navigation to sector pages from Browse by type of work
	Given the user is on the Browse by type of work page
	When the user selects the "<SectorName>" sector
	Then the user should be directed to the "<SectorName>" page

	Examples:
		| SectorName                             |
		| Agriculture, environmental and animal care |
		| Business and administration            |
		| Care services                          |
		| Catering and hospitality               |
		| Construction and building              |
		| Creative and design                    |
		| Digital                                |
		| Education and early years              |
		| Engineering and manufacturing          |
		| Hair and beauty                        |
		| Health and science                     |
		| Legal, finance and accounting          |
		| Protective services                    |
		| Sales and marketing                    |
		| Transport and logistics                |