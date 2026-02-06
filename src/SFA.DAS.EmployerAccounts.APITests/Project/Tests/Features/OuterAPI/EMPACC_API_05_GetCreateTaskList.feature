Feature: EMPACC_API_05_GetCreateTaskList

@api
@employeraccountsapi
@outerapi
@regression
Scenario: EMPACC_API_05_GetCreateTaskList
	Then endpoint /Accounts/{accountId}/create-task-list can be accessed