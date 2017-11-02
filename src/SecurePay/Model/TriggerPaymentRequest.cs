namespace SecurePay.Model
{
    public class TriggerPaymentRequest : PeriodicRequest
    {
        public int? Amount;
        public string TransactionReference { get; set; }
    }
}