@approvals
Feature: AP_DO_01_ULN_Overlap_Provider


@regression
@liveapprentice
@postapprovals
Scenario: AP_DO_01_ULN Overlap_Provider
	Given a live apprentice record exists with startdate of <-6> months and endDate of <+6> months from current date
	When Provider tries to add a new apprentice using details from table below
	| NewStartDate	 | NewEndDate	| DisplayOverlapErrorOnStartDate   | DisplayOverlapErrorOnEndDate 	|
	| +3		     | -3		    | true			                   | true				            |
	| -3		     | +3		    | true					           | true				            |
	| -3		     | -3		    | false					           | true				            |
	| 0				 | 0		    | true					           | true				            |
	#Then Display ULN Overlap error as per above table


#Note: all above dates use datetime.now as the reference date