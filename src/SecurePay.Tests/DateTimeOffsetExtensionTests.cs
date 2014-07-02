using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecurePay.Runtime;

namespace SecurePay.Tests
{
    [TestClass]
    public class DateTimeOffsetExtensionTests
    {
        [TestMethod]
        public void ToSecurePayTimestampString()
        {
            var dt = new DateTimeOffset(2002, 6, 24, 17, 12, 16, 789, TimeSpan.FromHours(10));
            Assert.AreEqual("20022406171216789000+600", dt.ToSecurePayTimestampString());
        }
    }
}
