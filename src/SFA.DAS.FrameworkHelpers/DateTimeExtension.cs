using System;
using System.Globalization;

namespace SFA.DAS.FrameworkHelpers
{
    public static class DateTimeExtension
    {
        public static string GetDateTimeValue() => DateTime.Now.ToString("ddMMMyyyyHHmmss").ToUpper();

        public static string ToSeconds(this DateTime dateTime) => $"{dateTime:ddMMMyyyy_HHmmss}";

        public static string ToNanoSeconds(this DateTime dateTime) => dateTime.ToString("fffff");

        public static string FormatWithCustomMonth(DateTime date)
        {
            var customCulture = (CultureInfo)CultureInfo.InvariantCulture.Clone();
            customCulture.DateTimeFormat.AbbreviatedMonthNames = new[]
            { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec", "" };
            return date.ToString("MMM yyyy", customCulture);
        }
    }
}
