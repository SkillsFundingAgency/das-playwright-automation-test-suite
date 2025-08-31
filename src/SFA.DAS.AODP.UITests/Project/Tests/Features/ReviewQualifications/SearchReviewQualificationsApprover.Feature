Feature: Verify the new and changed qualification screens for search properties as qfau_user_approver

    @dfe_aodp
    Scenario Outline: Verify the AODP login as qfau_user_approver and verify the search in new qualification review page

        Given a user with <role> role should able to login
        Then User should able to see aodp home page
        When Click on <option> in the aodp home page
        When Click on continue button in the aodp home page
        Then User should able to see <targetscreen>
        When Enter text <QualName> in qualification name field on dfe new qualifications page
        When Click on search button in the dfe new qualifications page
        Then Verify the message for no matching qualication exists in the dfe new qualifications page

        Examples:
            | QualName     | role               | option                           | targetscreen                | actualRole                            |
            | randomString | qfau_user_approver | review new qualifications option | dfe new qualifications page | qfau_user_approver/qfau_user_reviewer |


    @dfe_aodp
    Scenario Outline: Verify the AODP login as qfau_user_approver and verify the search in changed qualification review page

        Given a user with <role> role should able to login
        Then User should able to see aodp home page
        When Click on <option> in the aodp home page
        When Click on continue button in the aodp home page
        Then User should able to see <targetscreen>
        When Enter text <QualName> in qualification name field on dfe changed qualifications page
        When Click on search button in the dfe changed qualifications page
        Then Verify the message for no matching qualication exists in the dfe changed qualifications page

        Examples:
            | QualName     | role               | option                               | targetscreen                    | actualRole                            |
            | randomString | qfau_user_approver | review changed qualifications option | dfe changed qualifications page | qfau_user_approver/qfau_user_reviewer |
