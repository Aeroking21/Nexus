using System;
using System.IO;
using System.Text;
using Microsoft.Fast.Components.FluentUI;
using TimeZoneConverter;

namespace GraphSample.SharedFns
{
    public class SharedFunctions
    {
        public FluentDialog? MyFluentDialog;

        public string ConvertWebVttStreamToString(Stream? stream)
        {
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                return reader.ReadToEnd();
            }
        }


         public string FormatIso8601DateTime(string? iso8601DateTime, string? dateTimeFormat)
    {
        if (string.IsNullOrEmpty(iso8601DateTime))
        {
            return string.Empty;
        }
        // Load into a DateTime
        var dateTime = 
            DateTime
            .Parse(iso8601DateTime);
        if (!string.IsNullOrWhiteSpace(dateTimeFormat))
        {
            // Format it using the user's settings
            return dateTime.ToString(dateTimeFormat);
        }

        // Fallback to return original value
        return iso8601DateTime;
    }

    public DateTime GetUtcStartOfWeekInTimeZone(DateTime today, string timeZoneId)
    {
        TimeZoneInfo userTimeZone = 
            TZConvert
            .GetTimeZoneInfo(timeZoneId);
        int diff = 
            System.DayOfWeek.Sunday - today.DayOfWeek;
        var unspecifiedStart = 
            DateTime.SpecifyKind(today.AddDays(diff), DateTimeKind.Unspecified);
        
        return TimeZoneInfo.ConvertTimeToUtc(unspecifiedStart, userTimeZone);
    }
    }

 
}
