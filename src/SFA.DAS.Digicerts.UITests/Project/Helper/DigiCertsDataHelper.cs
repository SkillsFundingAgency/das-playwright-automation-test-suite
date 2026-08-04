namespace SFA.DAS.Digicerts.UITests.Project.Helpers;

public class DigiCertsDataHelper
{
    public string UserFirstName { get; private set; }
    public string UserLastName { get; private set; }


    public void GetNameFromEmail(string email)
    {
        var username = email.Split('@')[0];   // Emily.Carter
        var names = username.Split('.');      // ["Emily", "Carter"]

        UserFirstName = names[0];
        UserLastName = names[1];
    }
}