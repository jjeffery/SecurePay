using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecurePay.Runtime;

namespace SecurePay.Tests
{
    [TestClass]
    public class DateUtilsTests
    {
        [TestMethod]
        public void ParseYyyymmdd()
        {
            var testData = new[] {
                new {Text = "20990908", Date = DateFor(2099, 9, 8)},
                new {Text = "20991231", Date = DateFor(2099, 12, 31)},
                new {Text = " 2099 12 31 ", Date = DateFor(2099, 12, 31)},
                new {Text = "991231", Date = (DateTime?) null},
                new {Text = (string) null, Date = (DateTime?) null},
                new {Text = "xyzlkjdflkj", Date = (DateTime?) null},
            };

            foreach (var td in testData)
            {
                var date = DateUtils.ParseYyyymmdd(td.Text);
                Assert.AreEqual(td.Date, date);
            }
        }

        private static DateTime? DateFor(int year, int month, int day)
        {
            return new DateTime(year, month, day, 0, 0, 0, DateTimeKind.Local);
        }
    }
}
