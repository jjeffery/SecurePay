using System;

namespace SecurePay.Model
{
    public class TriggerPaymentResponse : PeriodicResponse
    {
        public int Amount;
        public string Currency;
        public string TransactionId;
        public CreditCardResponse CreditCard;
        public DateTime? SettlementDate;
    }
}