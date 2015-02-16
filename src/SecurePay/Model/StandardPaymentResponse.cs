using System;

namespace SecurePay.Model
{
	public class StandardPaymentResponse
	{
		public string PurchaseOrder;
		public int Amount;
		public string Currency = "AUD";
		public bool Approved;
		public string ResponseCode;
		public string ResponseText;
		public DateTime? SettlementDate;
		public string TransactionId;
		public CreditCardResponse CreditCard;
	}
}
