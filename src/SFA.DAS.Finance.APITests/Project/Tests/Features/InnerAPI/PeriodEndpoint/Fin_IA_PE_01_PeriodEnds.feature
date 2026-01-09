Feature: Fin_IA_PE_01_PeriodEnds_01

# @api
# @employerfinanceapi
# @regression
# @innerapi
# Scenario: Fin_IA_PE_01_PeriodEnds_1  POST periodends and verify the response
#     Given remove the existing period end data from DB
#     When period end details are posted via endpoint: api/period-ends
#     Then the posted period-end matches the payload

# @api
# @employerfinanceapi
# @regression
# @innerapi
# Scenario: Fin_IA_PE_01_PeriodEnds_2 Post periods and verify retrieval by list
#     Given remove the existing period end data from DB
#     When period end details are posted via endpoint: api/period-ends
#     Then its details can be accessed via endpoint: api/period-ends
#     Then the posted period-end matches the payload

# @api
# @employerfinanceapi
# @regression
# @innerapi
# Scenario: Fin_IA_PE_01_PeriodEnds_3 Post periods and verify retrieval by periodEndId
#     Given remove the existing period end data from DB
#     When period end details are posted via endpoint: api/period-ends
#     Then its details can be accessed via endpoint: api/period-ends/{periodEndId}
#     Then the posted period-end matches the payload
