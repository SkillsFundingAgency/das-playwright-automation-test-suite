using SFA.DAS.FrameworkHelpers;

namespace SFA.DAS.FATe.UITests.Helpers
{
    public class FATeDataHelper
    {
        public FATeDataHelper()
        {
            Firstname = RandomDataGenerator.GenerateRandomAlphabeticString(6);
            Lastname = RandomDataGenerator.GenerateRandomAlphabeticString(9);
            FullName = $"{Firstname} {Lastname}";
            Email = $"{Firstname}.{Lastname}@example.com";
            Positions = RandomDataGenerator.GenerateRandomNumber(1);
            Course = $"Abattoir worker (Level 2)";
            PartialCourseName = $"Worker";
            Location = $"Coventry";
            UKPRN = $"10000528";
            ProviderDetails = $"BARKING AND DAGENHAM COLLEGE UKPRN: 10000528";
            LocationDetails = $"Coventry, West Midlands";
            InvalidUKPRN = $"12345678";
        }

        public string FullName { get; }

        public string Firstname { get; }

        public string Lastname { get; }

        public string Email { get; }

        public string Positions { get; }

        public string Course { get; }

        public string Location { get; }

        public string UKPRN { get; }
        public string ProviderDetails { get; }
        public string InvalidUKPRN { get; }
        public string LocationDetails { get; }
        public string PartialCourseName { get; }
    }
}
