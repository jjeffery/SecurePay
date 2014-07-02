namespace SecurePay.Model
{
    /// <summary>
    /// Credit card details included in a request to SecurePay
    /// </summary>
    public class CreditCardRequest
    {
        public string CardNumber;
        public string Cvv;
        public YearMonth Expires;
        public bool Recurring;
    }
}