using System;
using System.Text;

namespace SecurePay.Runtime
{
    public static class DateTimeOffsetExtensions
    {
        /// <summary>
        /// Returns a string in the SecurePay timestamp format
        /// </summary>
        public static string ToSecurePayTimestampString(this DateTimeOffset dt)
        {
            var sb = new StringBuilder();
            // note the date is US format, ie year then day then month
            sb.Append(dt.ToString("yyyyddMMHHmmssfff"));
            sb.Append("000"); // nanoseconds
            var offsetMinutes = Convert.ToInt32(dt.Offset.TotalMinutes);
            sb.Append(offsetMinutes.ToString("+000;-000;+000"));
            return sb.ToString();
        }
    }
}
