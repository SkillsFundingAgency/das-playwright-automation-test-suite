namespace SFA.DAS.ApprenticeLogin.Service.Project.Helpers;

public class SetApprenticeDetailsHelper(ScenarioContext context)
{
    private readonly ObjectContext objectContext = context.Get<ObjectContext>();

    //public (string firstName, string lastName) SetApprenticeDetails(ApprenticeUser user)
    //{
    //    (string firstName, string lastName) = SetApprenticeDetailsInObjectContext(user);

    //    context.Get<ApprenticeDataHelper>().UpdateCurrentApprenticeName(firstName, lastName);

    //    return (firstName, lastName);
    //}

    public (string firstName, string lastName) SetApprenticeDetailsInObjectContext(ApprenticeUser user)
    {
        (string apprenticeId, string firstName, string lastName, string username) = (user.Id, user.FirstName, user.LastName, user.Username);

        objectContext.SetApprenticeId(apprenticeId);
        objectContext.SetApprenticeEmail(username);
        objectContext.SetApprenticePassword(user.IdOrUserRef);

        objectContext.SetFirstName(firstName);
        objectContext.SetLastName(lastName);

        return (firstName, lastName);
    }
}