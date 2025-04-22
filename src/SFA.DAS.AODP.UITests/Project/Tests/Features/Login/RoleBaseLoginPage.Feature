Feature: Verify the login for AODP with different user roles

    @dfe_aodp
    Scenario Outline: Verify the AODP login as qfau_user_approver and verify the screen <targetScreen>

        Given a user with <role> role should able to login
        Then User should able to see aodp home page
        When Click on <option> in the aodp home page
        When Click on continue button in the aodp home page
        Then User should able to see <targetscreen>

        Examples:
            | role               | option                               | targetscreen                    |
            | qfau_user_approver | review ao applications option        | ao review page                  |
            | qfau_user_approver | review new qualifications option     | dfe new qualifications page     |
            | qfau_user_approver | review changed qualifications option | dfe changed qualifications page |


    @dfe_aodp
    Scenario Outline: Verify the AODP login as qfau_user_reviewer and verify the screen <targetScreen>

        Given a user with <role> role should able to login
        Then User should able to see aodp home page
        When Click on <option> in the aodp home page
        When Click on continue button in the aodp home page
        Then User should able to see <targetscreen>

        Examples:
            | role               | option                               | targetscreen                    |
            | qfau_user_reviewer | review ao applications option        | ao review page                  |
            | qfau_user_reviewer | review new qualifications option     | dfe new qualifications page     |
            | qfau_user_reviewer | review changed qualifications option | dfe changed qualifications page |

    @dfe_aodp
    Scenario Outline: Verify the AODP login as qfau_admin_data_importer and verify the screen <targetScreen>

        Given a user with <role> role should able to login
        Then User should able to see aodp home page
        When Click on <option> in the aodp home page
        When Click on continue button in the aodp home page
        Then User should able to see <targetscreen>

        Examples:
            | role                     | option             | targetscreen     |
            | qfau_admin_data_importer | import data option | import data page |
            | qfau_admin_data_importer | output file option | output file page |

    @dfe_aodp
    Scenario Outline: Verify the AODP login as qfau_admin_form_editor and verify the screen <targetScreen>

        Given a user with <role> role should able to login
        Then User should able to see aodp home page
        When Click on <option> in the aodp home page
        When Click on continue button in the aodp home page
        Then User should able to see <targetscreen>

        Examples:
            | role                   | option                 | targetscreen      |
            | qfau_admin_form_editor | form management option | form builder page |

    @dfe_aodp
    Scenario Outline: Verify the AODP login as ifate_admin_form_editor and verify the screen <targetScreen>

        Given a user with <role> role should able to login
        Then User should able to see <targetscreen>

        Examples:
            | role                    | targetscreen      |
            | ifate_admin_form_editor | unauthorised page |

    @dfe_aodp
    Scenario Outline: Verify the AODP login as ifate_user_reviewer and verify the screen <targetScreen>

        Given a user with <role> role should able to login
        Then User should able to see <targetscreen>

        Examples:
            | role                | targetscreen   |
            | ifate_user_reviewer | ao review page |

    @dfe_aodp
    Scenario Outline: Verify the AODP login as ofqual_user_reviewer and verify the screen <targetScreen>

        Given a user with <role> role should able to login
        Then User should able to see <targetscreen>

        Examples:
            | role                 | targetscreen   |
            | ofqual_user_reviewer | ao review page |

    @dfe_aodp
    Scenario Outline: Verify the AODP login as ao_user and verify the screen <targetScreen>

        Given a user with <role> role should able to login
        Then User should able to see <targetscreen>

        Examples:
            | role    | targetscreen |
            | ao_user | ao home page |