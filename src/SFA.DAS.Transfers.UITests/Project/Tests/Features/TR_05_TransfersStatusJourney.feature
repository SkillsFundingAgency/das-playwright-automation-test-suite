﻿
Feature: TR_05_TransfersStatusJourney

@transfers
@regression
Scenario: TR_05_01 Transfers - Verify transfer status when agreement is not signed
	Given We have a Sender with sufficient levy funds without signing an agreement
	Then the sender transfer status is disabled

@transfers
@regression
Scenario: TR_05_02 Transfers - Verify transfer status when agreement is signed
	Given We have a Sender with sufficient levy funds
	Then the sender transfer status is enabled