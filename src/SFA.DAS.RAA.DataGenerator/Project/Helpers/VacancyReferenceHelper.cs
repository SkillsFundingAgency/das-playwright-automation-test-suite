

namespace SFA.DAS.RAA.DataGenerator.Project.Helpers;

public class VacancyReferenceHelper(ObjectContext objectContext)
{
    public void SetVacancyReference(string referenceNumber)
    {
        objectContext.SetVacancyReference(RegexHelper.GetVacancyReference(referenceNumber));
    }
}
