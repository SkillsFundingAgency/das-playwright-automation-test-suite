@raa
@raaprovider
@regression
@recruitmentapikey
Feature: RAA_P_RA_04


Scenario: RAA_E_RA_04_01 - View display advert api dev hub pages
	Given the Provider navigates to 'Recruit' Page
	And the provider selects the developer get started page
	When the provider selects 'Display Advert API' link
	Then the provider can view the 'Display advert API' page

Scenario: RAA_E_RA_04_02 - View recruitment api dev hub pages
	Given the Provider navigates to 'Recruit' Page
	And the provider selects the developer get started page
	When the provider selects 'Recruitment API' link
	Then the provider can view the 'Recruitment API' page

Scenario: RAA_E_RA_04_03 - Sign in to api dev hub
	Given the Provider navigates to 'Recruit' Page
	And the provider selects the developer get started page
	When the provider signs in to dev hub
	Then the provider can view the 'API list' page