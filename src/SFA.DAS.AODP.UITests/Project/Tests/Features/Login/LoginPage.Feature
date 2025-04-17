Feature: Verify the login for AODP with different user roles

    @dfe_aodp
    Scenario Outline: Verify the AODP login as <role> and verify the screen <targetScreen>

        Given a user with <role> role should able to login
        Then User should able to see aodp home page
        When Click on <option> in the aodp home page
        When Click on continue button in the aodp home page
        Then User should able to see <targetscreen>

        Examples:
            | role      | option                               | targetscreen                    | actualRole                            |
            | dfe admin | review ao applications option        | ao home page                    | qfau_user_approver/qfau_user_reviewer |
            | dfe admin | review new qualifications option     | dfe new qualifications page     | qfau_user_approver/qfau_user_reviewer |
            | dfe admin | review changed qualifications option | dfe changed qualifications page | qfau_user_approver/qfau_user_reviewer |
            | dfe admin | form management option               | form builder page               | qfau_admin_data                       |
            | dfe admin | output file option                   | output file page                | qfau_admin_forms                      |
            | dfe admin | import data option                   | dfe home page                   | qfau_admin_data                       |


