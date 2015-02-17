namespace SecurePay.Model
{
    /// <summary>
    /// Credit card details included in a request to SecurePay
    /// </summary>
    public class CreditCardRequest
    {
		/// <summary>
		/// Credit card number, no spaces.
		/// </summary>
        public string CardNumber;

		/// <summary>
		/// Card check value
		/// </summary>
        public string Cvv;

		/// <summary>
		/// Card expiry: year and month
		/// </summary>
        public YearMonth Expires;

		/// <summary>
		/// Name on the credit card
		/// </summary>
		/// <remarks>
		/// This is optional, it is ignored by the SecurePay library.
		/// It can be used by the calling program to store anything.
		/// </remarks>
	    public string CardName;
    }
}