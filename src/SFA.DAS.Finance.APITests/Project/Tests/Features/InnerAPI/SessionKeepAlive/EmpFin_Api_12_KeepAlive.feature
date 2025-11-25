Feature: EmpFin_Api_12_KeepAlive

@api
@employerfinanceapi
@regression
@innerapi
Scenario: KeepAlive
	Then endpoint /service/keepalive can be accessed
