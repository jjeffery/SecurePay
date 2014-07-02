namespace SecurePay.Model
{
    /// <summary>
    /// Credit card details included in a response from SecurePay
    /// </summary>
    public class CreditCardResponse
    {
        public string TruncatedCardNumber;
        public YearMonth Expires;
        public bool Recurring;
        public string CardDescription;
    }
}