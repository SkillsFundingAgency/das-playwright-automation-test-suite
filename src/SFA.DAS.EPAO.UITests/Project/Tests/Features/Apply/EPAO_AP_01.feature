Feature: EPAO_AP_01

@epao
@epaoapply
@regression
Scenario: EPAO_AP_01A - Validate SignIn and SignOut
	When the Apply User is logged into Assessment Service Application
	Then the User Name is displayed in the Logged In Home page
	And the Apply User is able to Signout from the application

@epao
@epaoapply
@regression
Scenario: EPAO_AP_01B - Validate Organisation Search funcationality
	When the Apply User is logged into Assessment Service Application
	Then no matches are shown for Organisation searches with Invalid search term