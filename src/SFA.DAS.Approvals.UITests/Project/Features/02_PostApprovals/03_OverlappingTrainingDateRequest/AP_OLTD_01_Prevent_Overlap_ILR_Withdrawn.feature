@approvals
Feature: AP_OLTD_01_Prevent_Overlap_ILR_Withdrawn


@regression
@liveapprentice
@OltdOnWitdrawnRecord
Scenario: AP_OLTD_01_Prevent_Overlap_ILR_Withdrawn
	Given a learner record stopped via ILR with startdate of <-6> months and endDate of <+6> months from current date
	When Provider tries to add a new apprentice for existing ILR Withdrawn apprentice using details from table below
	| NewStartDate	 | NewEndDate	| DisplayOverlapErrorOnStartDate   | DisplayOverlapErrorOnEndDate 	|
	| +3		     | +12		    | true			                   | true				            |
	Then Provider can view the draft apprentice overlap options