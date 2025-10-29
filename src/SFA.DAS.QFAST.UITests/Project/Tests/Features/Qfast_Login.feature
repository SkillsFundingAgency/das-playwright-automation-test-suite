Feature: QFAT_01


@regression
@qfast
Scenario: QFAST Admin User login in to the portal
	Given the admin user log in to the portal
	And I validate opitons on the page


Scenario: QFAST IFATe User login in to the portal
	Given the ifate user log in to the portal


Scenario: QFAST OFQUAL User login in to the portal
	Given the ofqual user log in to the portal
	

Scenario: QFAST AO User login in to the portal
	Given the ao user log in to the portal