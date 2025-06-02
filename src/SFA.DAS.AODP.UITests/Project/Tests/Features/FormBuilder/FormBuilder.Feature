Feature: Verify the login for DfE admin able to create a form and manage

     @dfe_aodp
     Scenario Outline: Verify the form builder functionality with dfe admin user

          # Given a user <dfe_user> create a new form and publish
          # Then verify the form created successfully
          # Then a user <ao_user> should see the published form

          Given a user with <role> role should able to login
          Then User should able to see aodp home page
          When Click on <option> in the aodp home page
          When Click on continue button in the aodp home page
          Then User should able to see <targetscreen>

          When Click on form builder button in the dfe home page
          Then User should able to see form builder page
          When create a new form <formName> and save the changes
          Then Verify the new form exists in list of forms page containing <formName>

          Then Verify the text Draft following to <formName>
          When Click the button Edit draft version following text <formName>
          Then Verify the first button in edit forms page it should contain Update Description Preview
          When Click on back to forms button in the edit forms page

          Examples:
               | role      | option                 | targetscreen      | formName              | status |
               | dfe admin | form management option | form builder page | AutomationTestForm_12 | Draft  |