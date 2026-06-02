Feature: AA_01_AppLogin

	Apprentice can log into the Apprentice App

@ApprenticeApp @regression
Scenario: AA_01_Apprentice logs into the app and is taken to the KSBs tab
	Given the apprentice has logged into the app
	Then the apprentice is taken to the KSBs tab
