Feature: Verify the login for AODP with available features as an admin

    @dfe_aodp
    Scenario Outline: Verify the AODP login as <role> and verify the screen <targetScreen>

        Given a user with <role> role should able to login
        Then User should able to see aodp home page
        When Click on <option> in the aodp home page
        When Click on continue button in the aodp home page
        Then User should able to see <targetscreen>

        Examples:
            | role      | option                               | targetscreen                    |
            | dfe admin | review ao applications option        | ao review page                  |
            | dfe admin | review new qualifications option     | dfe new qualifications page     |
            | dfe admin | review changed qualifications option | dfe changed qualifications page |
            | dfe admin | form management option               | form builder page               |
            | dfe admin | output file option                   | output file page                |
            | dfe admin | import data option                   | import data page                |


