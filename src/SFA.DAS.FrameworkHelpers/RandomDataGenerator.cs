﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace SFA.DAS.FrameworkHelpers
{
    public class RandomDataGenerator
    {
        private const string Alphabets = LowerCaseAlphabets + UpperCaseAlphabets;
        private const string LowerCaseAlphabets = "abcdefghijklmnopqrstuvwxyz";
        private const string UpperCaseAlphabets = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string Numbers = "0123456789";
        private const string WholeNumbers = "123456789";
        private const string SpecialChars = "!@£$%^&*()_+{}:<>?-=[];',./";

        public static T GetRandomElementFromListOfElements<T>(List<T> elements)
        {
            var randomNumber = GenerateRandomNumberBetweenTwoValues(0, elements.Count);

            return elements[randomNumber];
        }

        public static T GetRandom<T>(IReadOnlyCollection<T> elements)
        {
            var randomNumber = GenerateRandomNumberBetweenTwoValues(0, elements.Count);

            return elements.ElementAt(randomNumber);
        }

        public static DateTime GenerateRandomDate(DateTime startDate, DateTime endDate)
        {
            var noOfdays = (endDate.Date - startDate.Date).TotalDays;

            var addDays = GenerateRandomNumberBetweenTwoValues(1, (int)noOfdays + 1);

            return startDate.AddDays(addDays);
        }

        public static string RandomPostCode() => GetRandomElementFromListOfElements(["SW1H 9NA", "SW1A 2AA", "SE1 8UG", "E14 4PU", "SW1A 1AA", "SW1P 3BT"]);

        public static string RandomTown() => GetRandomElementFromListOfElements(["London", "Coventry", "Harrow", "Manchester", "York", "Temple"]);

        public static string GenerateRandomSchool() => GetRandomElementFromListOfElements(["Church", "Grange", "Primary", "Academy", "Catholic"]);

        public static string GenerateRandomAlphabeticString(int length) => GenerateRandomString(Alphabets, length);

        public static string GenerateRandomNumber(int length) => GenerateRandomString(Numbers, length);

        public static string GenerateRandomWholeNumber(int length) => GenerateRandomString(WholeNumbers, length);

        public static string GenerateRandomAlphanumericString(int length) => GenerateRandomString(Alphabets + Numbers, length);

        public static string GenerateRandomAlphanumericStringWithSpecialCharacters(int length) => GenerateRandomString(Alphabets + Numbers + SpecialChars, length);

        public static string GenerateRandomPassword(int noOfUppercaseLetters, int noOfLowerCaseLetters, int noOfNumbers, int noOfSpecialChars)
        {
            var randomString = GenerateRandomString(LowerCaseAlphabets, noOfUppercaseLetters);
            randomString += GenerateRandomString(UpperCaseAlphabets, noOfLowerCaseLetters);
            randomString += GenerateRandomString(Numbers, noOfNumbers);
            randomString += GenerateRandomString(SpecialChars, noOfSpecialChars);
            return randomString;
        }

        public static int GenerateRandomDateOfMonth() => GenerateRandomNumberBetweenTwoValues(1, 28);

        public static int GenerateRandomMonth() => GenerateRandomNumberBetweenTwoValues(1, 13);

        public static int GenerateRandomNumberBetweenTwoValues(int min, int max) => new Random().Next(min, max);

        public static int GenerateRandomDobYear()
        {
            int yearsToAdd = GenerateRandomNumberBetweenTwoValues(-30, -18);
            DateTime date = DateTime.Now.AddYears(yearsToAdd);
            return date.Year;
        }

        public static string GenerateRandomUln()
        {
            System.String randomUln = GenerateRandomNumberBetweenTwoValues(10, 99).ToString() + DateTime.Now.ToString("ssffffff");

            for (int i = 1; i < 30; i++)
            {
                if (IsValidCheckSum(randomUln)) return randomUln;

                randomUln = (long.Parse(randomUln) + 1).ToString();
            }
            throw new Exception("Unable to generate ULN");
        }

        private static bool IsValidCheckSum(string uln)
        {
            var ulnCheckArray = uln.ToCharArray()
                                    .Select(c => long.Parse(c.ToString()))
                                    .ToList();

            var multiplier = 10;
            long checkSumValue = 0;
            for (var i = 0; i < 10; i++)
            {
                checkSumValue += ulnCheckArray[i] * multiplier;
                multiplier--;
            }

            return checkSumValue % 11 == 10;
        }

        private static string GenerateRandomString(string characters, int length) => new(Enumerable.Repeat(characters, length).Select(s => s[new Random().Next(s.Length)]).ToArray());

        public static string GenerateRandomPhoneNumber(int length)
        {
            return $"0{GenerateRandomString(Numbers, length - 1)}";
        }
    }
}