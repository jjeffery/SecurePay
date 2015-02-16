using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SecurePay.Messages
{
	[XmlRoot("SecurePayMessage")]
	public class PaymentRequestMessage : SecurePayMessage
	{
		public PaymentRequestMessage()
		{
			RequestType = "Payment";
		}

		public Payment Payment = new Payment();
	}

	[XmlRoot("SecurePayMessage")]
	public class PaymentResponseMessage : SecurePayResponseMessage
	{
		public Payment Payment;
	}

	public class Payment
	{
		[XmlElement("TxnList")]
		public PaymentList PaymentList = new PaymentList();
	}

	/// <summary>
	/// See Appendix A: Secure XML API Integration Guide
	/// </summary>
	/// <remarks>
	/// Only relevant codes included here.
	/// </remarks>
	public enum PaymentTransactionType
	{
		[XmlEnum("0")] StandardPayment,
		[XmlEnum("4")] Refund,
		[XmlEnum("6")] ClientReversal,
		[XmlEnum("10")] Preauthorise,
		[XmlEnum("11")] PreauthComplete,
	}

	/// <summary>
	/// See Appendix B: Secure XML API Integration Guide
	/// </summary>
	/// <remarks>
	/// Only relevant codes included here.
	/// </remarks>
	public enum PaymentTransactionSource
	{
		/// <summary>
		/// This is the only valid value for the XML API.
		/// </summary>
		[XmlEnum("23")] Xml,
	}

	public class PaymentList
	{
		[XmlAttribute("count")]
		public int Count = 1;

		[XmlElement("Txn")]
		public PaymentTransaction Transaction = new PaymentTransaction();
	}

	public class PaymentTransaction
	{
		[XmlAttribute("ID")]
		public int Id = 1;

		[XmlElement("txnType")]
		public PaymentTransactionType TransactionType;

		[XmlElement("txnSource")]
		public PaymentTransactionSource TransactionSource = PaymentTransactionSource.Xml;

		/// <summary>
		/// Amount in cents
		/// </summary>
		[XmlElement("amount")]
		public int Amount;

		[XmlElement("currency")] 
		public string Currency;

		[XmlElement("purchaseOrderNo")]
		public string PurchaseOrderNumber;

		[XmlElement("approved")]
		public string Approved;

		[XmlElement("responseCode")]
		public string ResponseCode;

		[XmlElement("responseText")]
		public string ReponseText;

		[XmlElement("settlementDate")]
		public string SettlementDate;

		[XmlElement("txnID")]
		public string TransactionId;

		[XmlElement("preauthID")]
		public string PreauthId;

		[XmlElement("CreditCardInfo")]
		public CreditCardInfo CreditCardInfo;
	}
}
