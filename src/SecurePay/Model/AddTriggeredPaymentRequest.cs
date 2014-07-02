namespace SecurePay.Model
{
    public class AddTriggeredPaymentRequest : PeriodicRequest
    {
        public int Amount;
        public CreditCardRequest CreditCard;
    }
}
