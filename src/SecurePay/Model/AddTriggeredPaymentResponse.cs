namespace SecurePay.Model
{
    public class AddTriggeredPaymentResponse : PeriodicResponse
    {
        public CreditCardResponse CreditCard;
        public int Amount;
    }
}