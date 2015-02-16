using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SecurePay.Messages
{
	public class CreditCardInfo
	{
		[XmlElement("cardNumber")]
		public string CardNumber;
		[XmlElement("cvv")]
		public string Cvv;
		[XmlElement("expiryDate")]
		public string ExpiryDate;
		[XmlElement("pan")]
		public string TruncatedCreditCardNumber;
		[XmlElement("cardType")]
		public int CardType;
		[XmlElement("cardDescription")]
		public string CardDescription;
		[XmlElement("recurringFlag")]
		public string RecurringFlag;
	}
}
