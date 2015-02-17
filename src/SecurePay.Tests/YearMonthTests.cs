using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecurePay.Model;

namespace SecurePay.Tests
{
    [TestClass]
    public class YearMonthTests
    {
        [TestMethod]
        public void Parse()
        {
            var testData = new[]
            {
                new {Text = "1/1", Month = 1, Year = 2001},
                new {Text = "12/2019", Month = 12, Year = 2019},
                new {Text = "\t\t11  /\t 2091  ", Month = 11, Year = 2091},
                new {Text = "13/2091  ", Month = 0, Year = 0},
                new {Text = "12/0000  ", Month = 0, Year = 0},
                new {Text = null as string, Month = 0, Year = 0},
                new {Text = "  ", Month = 0, Year = 0},
                new {Text = string.Empty, Month = 0, Year = 0},
                new {Text = "not-valid", Month = 0, Year = 0}
            };

            foreach (var td in testData)
            {
                var yearMonth = new YearMonth(td.Text);
                Assert.AreEqual(td.Year, yearMonth.Year);
                Assert.AreEqual(td.Month, yearMonth.Month);
                if (yearMonth.Month == 0)
                {
                    Assert.AreEqual(0, yearMonth.Year);
                    Assert.AreEqual(false, yearMonth.HasValue);
                }
                else
                {
                    Assert.AreEqual(true, yearMonth.HasValue);
                }
            }
        }

        [TestMethod]
        public void ToStringTest()
        {
            var testData = new[] {
                new { Month = 1, Year = 1, Text = "01/2001"},
                new { Month = 12, Year = 2098, Text = "12/2098"},
                new { Month = 0, Year = 0, Text = string.Empty},
            };

            foreach (var td in testData)
            {
                var yearMonth = new YearMonth(td.Year, td.Month);
                Assert.AreEqual(td.Text, yearMonth.ToString());
            }
        }

	    [TestMethod]
	    public void Equality()
	    {
		    var equalTestData = new[] {
			    "01/2001",
			    "12/2072",
		    };

		    foreach (var date in equalTestData)
		    {
			    var ym1 = new YearMonth(date);
			    var ym2 = new YearMonth(date);

			    Assert.IsTrue(ym1.Equals(ym2));
			    Assert.IsTrue(ym2.Equals(ym1));
			    Assert.IsTrue(ym1 == ym2);
				Assert.IsTrue(ym2 == ym1);
				Assert.IsFalse(ym1 != ym2);
				Assert.IsFalse(ym2 != ym1);

			    Assert.IsTrue(ym1 <= ym2);
			    Assert.IsTrue(ym2 <= ym1);
				Assert.IsTrue(ym1 >= ym2);
				Assert.IsTrue(ym2 >= ym1);
			}
	    }

		[TestMethod]
		public void Inequality()
		{
			var equalTestData = new[] {
			    new { Date1 = "01/2091", Date2 = "02/2092" },
			    new { Date1 = "01/2081", Date2 = "01/2082" },
			    new { Date1 = "01/2071", Date2 = "11/2071" },
		    };

			foreach (var testData in equalTestData) {
				var ym1 = new YearMonth(testData.Date1);
				var ym2 = new YearMonth(testData.Date2);

				Assert.IsFalse(ym1.Equals(ym2));
				Assert.IsFalse(ym2.Equals(ym1));
				Assert.IsFalse(ym1 == ym2);
				Assert.IsFalse(ym2 == ym1);
				Assert.IsTrue(ym1 != ym2);
				Assert.IsTrue(ym2 != ym1);
			}
		}

		[TestMethod]
		public void LessThan()
		{
			var equalTestData = new[] {
			    new { Date1 = "01/2091", Date2 = "02/2092" },
			    new { Date1 = "12/2081", Date2 = "01/2082" },
			    new { Date1 = "01/2071", Date2 = "11/2071" },
		    };

			foreach (var testData in equalTestData) {
				var ym1 = new YearMonth(testData.Date1);
				var ym2 = new YearMonth(testData.Date2);

				Assert.IsTrue(ym1 < ym2);
				Assert.IsFalse(ym2 < ym1);
				Assert.IsTrue(ym1 <= ym2);
				Assert.IsFalse(ym2 <= ym1);
			}
		}

		[TestMethod]
		public void GreaterThan()
		{
			var equalTestData = new[] {
			    new { Date1 = "02/2092", Date2 = "01/2092" },
			    new { Date1 = "01/2082", Date2 = "12/2081" },
			    new { Date1 = "9/2071", Date2 = "8/2071" },
		    };

			foreach (var testData in equalTestData) {
				var ym1 = new YearMonth(testData.Date1);
				var ym2 = new YearMonth(testData.Date2);

				Assert.IsTrue(ym1 > ym2);
				Assert.IsFalse(ym2 > ym1);
				Assert.IsTrue(ym1 >= ym2);
				Assert.IsFalse(ym2 >= ym1);
			}
		}
    }
}