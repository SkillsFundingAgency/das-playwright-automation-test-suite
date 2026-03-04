Feature: FAA_CreateAndDeleteAccount


@regression
@faa
Scenario: FAA_CreateAndDeleteAccount
	Given appretince creates an account
	Then apprentice is able to delete account
