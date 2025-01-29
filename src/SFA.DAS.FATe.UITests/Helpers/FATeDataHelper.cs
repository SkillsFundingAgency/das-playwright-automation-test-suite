﻿using SFA.DAS.FrameworkHelpers;

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
        }

        public string FullName { get; }

        public string Firstname { get; }

        public string Lastname { get; }

        public string Email { get; }

        public string Positions { get; }

        public string Course { get; }
    }
}
