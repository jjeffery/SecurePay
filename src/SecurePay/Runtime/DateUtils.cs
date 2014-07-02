using System;
using System.Text.RegularExpressions;

namespace SecurePay.Runtime
{
    public static class DateUtils
    {
        private readonly static Regex YyyymmddRegex = new Regex(@"^\s*(\d{4})\s*(\d{2})\s*(\d{2})\s*$");
        public static DateTime? ParseYyyymmdd(string s)
        {
            if (s != null)
            {
                var match = YyyymmddRegex.Match(s);
                if (match.Success)
                {
                    var year = int.Parse(match.Groups[1].Value);
                    var month = int.Parse(match.Groups[2].Value);
                    var day = int.Parse(match.Groups[3].Value);

                    return new DateTime(year, month, day, 0, 0, 0, DateTimeKind.Local);
                }
            }
            return null;
        }
    }
}
