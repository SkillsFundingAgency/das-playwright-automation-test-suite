Feature: QFAT_01


@regression
@qfast
Scenario: QFAST Admin User login in to the portal
	Given the admin user log in to the portal
	And I validate opitons on the page with the following expected options
		| Option										|
		| Review funding requests						|
		| Review newly regulated qualifications         |
		| Review regulated qualifications with changes	|
		| Import data							        |
		| Create a submission form						|
		| Create an output file					        |
	
@regression
@qfast
Scenario: QFAST IFATe User login in to the portal
	Given the ifate user log in to the portal

@regression
@qfast
Scenario: QFAST OFQUAL User login in to the portal
	Given the ofqual user log in to the portal
	
@regression
@qfast
Scenario: QFAST AO User login in to the portal
	Given the ao user log in to the portal

@regression
@qfast
Scenario: QFAST data Importer User login in to the portal
	Given the data importer user log in to the portal
	And I validate opitons on the page with the following expected options
		| Option                        |
		| Import data                   |
		| Create an output file         |

@regression
@qfast
Scenario: QFAST reviewer User login in to the portal	
	Given the reviewer user log in to the portal
	And I validate opitons on the page with the following expected options
		| Option										|
		| Review funding requests						|
		| Review newly regulated qualifications         |
		| Review regulated qualifications with changes	|
	
@regression
@qfast
Scenario: QFAST form editor login in to the portal
	Given the form editor user log in to the portal
	And I validate opitons on the page with the following expected options
		| Option										|
		| Create a submission form						|

	