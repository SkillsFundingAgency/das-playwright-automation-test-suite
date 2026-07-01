@approvals
Feature: AP_CoC_02_LearningWithdrawnEvent

Commitments receives LearningWithdrawnEvent from Learning domain for variety of reasons such as:
    - 2: Learner has transferred to another provider 
    - 3: Learner injury / illness 
    - 7: Learner has transferred between providers due to intervention by or with the written agreement of the ESFA 
    - 29: Learner has been made redundant 
    - 40: Learner has transferred to a new learning aim with the same provider 
    - 41: Learner has transferred to another provider to undertake learning that meets a specific government strategy 
    - 42: Academic failure / left in bad standing / not permitted to progress – HE learning aims only 
    - 43: Financial reasons 
    - 44: Other personal reasons 
    - 45: Written off after lapse of time – HE learning aims only 
    - 46: Exclusion 
    - 47: Learner has transferred to another provider due to merger 
    - 48: Industry placement learner has withdrawn due to circumstances outside the providers' control 
    - 97: Other 
    - 98: Reason not known 
This test validates that event is processed correctly and the apprentice record is updated (stopped) with correct reason code and stop date 


@regression
@liveapprentice
@postapprovals
Scenario: AP_CoC_02_Verify Learning Withdrawal Event marks the apprenticeship as Stopped
    Given a Live apprenticeship record exists for learner with Firstname: "DoNotUse_TestData" and LastName: "ChangeStatusApprentice"
	When LearningWithdrawnEvent is received for the apprentice
    Then provider verifies that record is set as "Stopped" in Provider portal
    And employer verifies that record has been "Stopped" in Employer portal    