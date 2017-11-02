using System.Xml.Serialization;

namespace SecurePay.Messages
{
    [XmlRoot("SecurePayMessage")]
    public class PeriodicRequestMessage : SecurePayMessage
    {
        public PeriodicRequestMessage()
        {
            RequestType = "Periodic";
        }

        public Periodic Periodic = new Periodic();
    }

    [XmlRoot("SecurePayMessage")]
    public class PeriodicResponseMessage : SecurePayResponseMessage
    {
        public Periodic Periodic;
    }

    public class Periodic
    {
        public PeriodicList PeriodicList = new PeriodicList();
    }

    public class PeriodicList
    {
        [XmlAttribute("count")] 
        public int Count = 1;

        public PeriodicItem PeriodicItem = new PeriodicItem();
    }

    public class PeriodicItem
    {
        [XmlAttribute("ID")] 
        public int Id = 1;

        [XmlElement("actionType")]
        public string ActionType;

        [XmlElement("periodicType")]
        public int? PeriodicType;

        [XmlElement("paymentInterval")]
        public string PaymentInterval;

        [XmlElement("amount")] 
        public int? Amount;

        [XmlElement("currency")]
        public string Currency;

        [XmlElement("clientID")]
        public string ClientId;

        [XmlElement("startDate")]
        public string StartDate;

        [XmlElement("numberOfPayments")]
        public string NumberOfPayments;

        [XmlElement("endDate")]
        public string EndDate;

        [XmlElement("responseCode")] 
        public string ResponseCode;

        [XmlElement("responseText")]
        public string ResponseText;

        [XmlElement("successful")]
        public string Successful;

        [XmlElement("settlementDate")]
        public string SettlementDate;

        [XmlElement("txnID")]
        public string TransactionId;

        [XmlElement("transactionReference")]
        public string TransactionReference;

        [XmlElement("receipt")] 
        public string Receipt;

        [XmlElement("ponum")] 
        public string PoNum;

        public CreditCardInfo CreditCardInfo;
    }
}
