Feature: LearnerData


@api
@approvalsapi
@learnerdataapi
@regression
Scenario Outline: Approvals - Update learner data
	When the user sends <Method> request to <Endpoint> with payload <Payload>
	Then api <ResponseStatus> response is received

	Examples: 
 | Method | Endpoint                                       | Payload   | ResponseStatus |
 | PUT    | /provider/10005760/academicyears/2425/learners | uln1.json | Accepted       |

