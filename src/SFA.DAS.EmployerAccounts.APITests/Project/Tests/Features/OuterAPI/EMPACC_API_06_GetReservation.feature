Feature: EMPACC_API_06_GetReservation

@api
@employeraccountsapi
@outerapi
@regression
Scenario: EMPACC_API_06_GetReservation
	Then endpoint /Reservation/{accountId} can be accessed