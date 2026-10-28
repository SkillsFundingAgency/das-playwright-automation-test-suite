Feature: Fin_IA_PE_01_PostPeriodEnds

@api
@employerfinanceapi
@regression
@innerapi
Scenario: Fin_IA_PE_01 Post period end and validate DB data
	Given a new period end is submitted
	When the saved period end details are checked
	Then the period end details are correct

@api
@employerfinanceapi
@regression
@innerapi
Scenario: Fin_IA_PE_02 Get period-ends list contains posted period end id
	Given a new period end is submitted
	When the period end list is requested
	Then the new period end is included in the period end list

@api
@employerfinanceapi
@regression
@innerapi
Scenario: Fin_IA_PE_03 Get period end by id and validate DB data
	Given a new period end is submitted
	When the period end is requested by its id
	And the saved period end details are checked
	Then the period end details are correct
