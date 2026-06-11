@approvals
Feature: AP_DO_01_ULN_Overlap_Provider

// Image
// Current Month = June 26
// Original Start Date = Jan 26 (<-6>)
// Original End Date   = Jan 26 (<+6>)
// Case 1 : New Start Date = Apr 26 (+3) and New End Date = Mar 26 (-3) => Display Overlap Error on both Start and End Date
// Case 2 : New Start Date = Mar 26 (-3) and New End Date = Sep 26 (+3) => Display Overlap Error on both Start and End Date
// Case 3 : New Start Date = Mar 26 (-3) and New End Date = Mar 26 (-3) => Display Overlap Error on End Date only
// Case 4 : New Start Date = Jan 26 (0) and New End Date = Jan 26 (0) => Display Overlap Error on both Start and End Date


@regression
@liveapprentice
@postapprovals
Scenario: AP_DO_01_ULN Overlap_Provider
	Given a live apprentice record exists with startdate of <-6> months and endDate of <+6> months from current date
	When Provider tries to add a new apprentice using details from table below
	| NewStartDateWithReespectToOld	 | NewEndDateWithReespectToOld	| DisplayOverlapErrorOnStartDate   | DisplayOverlapErrorOnEndDate 	|
	| +3		     | -3		    | true			                   | true				            |
	| -3		     | +3		    | true					           | true				            |
	| -3		     | -3		    | false					           | true				            |
	| 0				 | 0		    | true					           | true				            |
	#Then Display ULN Overlap error as per above table


#Note: all above dates use datetime.now as the reference date