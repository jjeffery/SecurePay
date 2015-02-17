using System.Text.RegularExpressions;

namespace SecurePay.Model
{
    /// <summary>
    /// Represents a year and a month. Used for credit card expiry.
    /// </summary>
    public struct YearMonth
    {
        public readonly int Year;
        public readonly int Month;

        public static readonly Regex Regex = new Regex(@"^\s*(\d{1,2})\s*/\s*(\d{1,4})\s*$");
	    public static readonly YearMonth Zero = default(YearMonth);

        public YearMonth(int year, int month)
        {
            Year = NormalizeYear(year);
            Month = month;
        }

        public bool HasValue
        {
            get { return Year != 0 && Month != 0; }
        }

        public YearMonth(string s)
        {
            if (!string.IsNullOrWhiteSpace(s))
            {
                var match = Regex.Match(s);
                if (match.Success)
                {
                    var month = int.Parse(match.Groups[1].Value);
                    var year = NormalizeYear(int.Parse(match.Groups[2].Value));

                    if (month >= 1 && month <= 12 && year >= 2000 && year < 2200)
                    {
                        Month = month;
                        Year = year;
                        return;
                    }
                }
            }

            // if we get here, the string is not in the valid format
            Month = 0;
            Year = 0;
        }

        private static int NormalizeYear(int year)
        {
            if (year > 0 && year < 100)
            {
                year += 2000;
            }
            return year;
        }

        /// <summary>
        /// Format "MM/YY" for SecurePay XML messages
        /// </summary>
        public string ToSecurePayString()
        {
            return HasValue ? string.Format("{0:00}/{1:00}", Month, Year % 100) : string.Empty;
        }

        /// <summary>
        /// Format "MM/YYYY" because I remember the 1990's and the Y2K scare.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return HasValue ? string.Format("{0:00}/{1:0000}", Month, Year) : string.Empty;
        }

	    public override int GetHashCode()
	    {
		    return Year*100 + Month;
	    }

	    public override bool Equals(object obj)
	    {
		    if (obj is YearMonth)
		    {
			    var other = (YearMonth) obj;
			    return other.Year == Year && other.Month == Month;
		    }
		    return false;
	    }

	    public static bool operator ==(YearMonth ym1, YearMonth ym2)
	    {
		    return ym1.Year == ym2.Year && ym1.Month == ym2.Month;
	    }

		public static bool operator !=(YearMonth ym1, YearMonth ym2)
		{
			return ym1.Year != ym2.Year || ym1.Month != ym2.Month;
		}

	    public static bool operator <(YearMonth ym1, YearMonth ym2)
	    {
		    if (ym1.Year == ym2.Year)
		    {
			    return ym1.Month < ym2.Month;
		    }
		    return ym1.Year < ym2.Year;
	    }

		public static bool operator >(YearMonth ym1, YearMonth ym2)
		{
			if (ym1.Year == ym2.Year) {
				return ym1.Month > ym2.Month;
			}
			return ym1.Year > ym2.Year;
		}

		public static bool operator <=(YearMonth ym1, YearMonth ym2)
		{
			if (ym1.Year == ym2.Year) {
				return ym1.Month <= ym2.Month;
			}
			return ym1.Year < ym2.Year;
		}

		public static bool operator >=(YearMonth ym1, YearMonth ym2)
		{
			if (ym1.Year == ym2.Year) {
				return ym1.Month >= ym2.Month;
			}
			return ym1.Year > ym2.Year;
		}
    }
}