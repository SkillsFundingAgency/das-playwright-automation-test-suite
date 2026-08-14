Feature: CA_EMP_UBAF_01

As the apprenticeship service 
I want to update the calculation for estimating benefit funding used by the Understanding apprenticeship funding and benefits screen (UBAF) 
So that changes to policy are correctly reflected

@campaigns
@employer
@regression
Scenario Outline: Verify funding calculation for annual payroll options
	Given the employer is on the Understanding apprenticeship benefits and funding page
	When the employer calculates funding selecting "<PayrollOption>"
	Then the estimated funding result should be calculated successfully

	Examples:
		| PayrollOption      |
		| Under £3 million   |
		| Over £3 million    |