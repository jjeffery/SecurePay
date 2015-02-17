using System;

namespace SecurePay.Model
{
	public class PreauthRequest
	{
		public string PurchaseOrder;
		public int Amount;
		public string Currency = "AUD";
		public CreditCardRequest CreditCard = new CreditCardRequest();
	}

	public class PreauthResponse
	{
		public string PurchaseOrder;
		public int Amount;
		public string Currency = "AUD";
		public bool Approved;
		public string ResponseCode;
		public string ResponseText;
		public DateTime? SettlementDate;
		public string TransactionId;
		public string PreauthId;
		public CreditCardResponse CreditCard;
	}
}
