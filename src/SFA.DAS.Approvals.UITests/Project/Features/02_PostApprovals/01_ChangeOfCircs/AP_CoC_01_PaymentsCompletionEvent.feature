@approvals
Feature: AP_CoC_01_PaymentsCompletionEvent

Commitments receives payment completion event from Payments team upon completion of a training.
This test validates that event is processed correctly and the apprentice record is updated accordingly

@regression
@liveapprentice
@postapprovals
@cleanup-db-pymt-completion-status
Scenario: AP_CoC_01_Verify Payment Completion event marks the apprenticeship as Complete
	Given a live apprentice record exists with startdate of <-6> months and endDate of <+6> months from current date
	When When payments completion event is received for the apprentice
	Then the apprenticeship is marked as Completed