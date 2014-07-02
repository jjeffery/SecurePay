using System.Xml.Serialization;

namespace SecurePay.Model
{
    /// <summary>
    /// Type of transaction to be processed by SecurePay
    /// </summary>
    public enum PeriodicType
    {
        [XmlEnum("1")] OnceOffPayment,
        [XmlEnum("2")] DayBasedPeriodicPayment,
        [XmlEnum("3")] CalendarBasedPeriodicPayment,
        [XmlEnum("4")] TriggeredPayment,
    }
}