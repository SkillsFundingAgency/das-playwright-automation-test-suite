using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.Approvals.UITests.Project.Helpers.DataHelpers.ApprenticeshipModel
{
    internal class ApprenticeFactory : IApprenticeFactory
    {
        private readonly int age;
        public ApprenticeFactory(int Age = 0)
        {
            age = Age; 
        }

        public async Task<Apprentice> CreateApprenticeAsync()
        {
            Apprentice apprentice = new Apprentice();

            apprentice.ULN = long.Parse(RandomDataGenerator.GenerateRandomUln());
            apprentice.FirstName = RandomDataGenerator.GenerateRandomAlphabeticString(6);
            apprentice.LastName = RandomDataGenerator.GenerateRandomAlphabeticString(9);
            apprentice.Email = $"{apprentice.FirstName}.{apprentice.LastName}@l38cxwya.mailosaur.net";
            apprentice.DateOfBirth = age == 0 ? RandomDataGenerator.GenerateRandomDate(DateTime.Now.AddYears(-30), DateTime.Now.AddYears(-16)) : DateTime.Now.AddYears(-age);

            await Task.Delay(100);
            return apprentice;
        }

    }
}
