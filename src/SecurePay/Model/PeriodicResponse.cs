namespace SecurePay.Model
{
    /// <summary>
    /// Base class for all responses from SecurePay
    /// </summary>
    public class PeriodicResponse
    {
        public string ClientId;
        public string ResponseCode;
        public string ResponseText;
        public bool Successful;
    }
}