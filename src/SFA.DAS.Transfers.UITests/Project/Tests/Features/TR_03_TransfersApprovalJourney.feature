Feature: TR_03_TransfersApprovalJourney

@regression
@transfers
@liveapprentice
@addtransferslevyfunds
@addsecondlevyfunds
Scenario: TR_03 Transfers - Creating Cohort rejected by Sender and then approved by all 3 parties
	Given We have two Employer accounts
	And First is a Sender connected to Second as a Receiver
	And Receiver Second has 1 apprentice ready to start training in ILR
	When Receiver Second sends empty cohort using transfer funds from Sender First to the provider for review and approval
	And Provider adds the ILR apprentice from the receiver's ILR and approves the cohort
	And Receiver Second approves the cohort
	Then Receiver Second sees the cohort in With transfer sending employers with status of Pending - with funding employer
	And 'Transfer request received' task link is displayed under Tasks pane for the Sender First account
	When Sender First rejects the cohort
	Then Receiver Second sees the cohort in Ready to review with status of Rejected by transfer sending employer
	And No 'Transfer request received' task link is displayed under Tasks pane for the Sender First account
	When Receiver Second requests provider review of the rejected ILR cohort
	And Provider approves the cohort
	And Receiver Second approves the cohort
	Then 'Transfer request received' task link is displayed under Tasks pane for the Sender First account
	When Sender First rejects the cohort
	Then No 'Transfer request received' task link is displayed under Tasks pane for the Sender First account
	When Receiver Second requests provider review of the rejected ILR cohort
	And Provider approves the cohort and sends to receiver for approval
	And Receiver Second approves the cohort
	Then 'Transfer request received' task link is displayed under Tasks pane for the Sender First account
	When Sender First approves the cohort
	Then No 'Transfer request received' task link is displayed under Tasks pane for the Sender First account
	And Receiver Second has a new live apprenticeship record created
