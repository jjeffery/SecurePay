using System;

namespace SecurePay
{
    public class SecurePayException : Exception
    {
        public SecurePayException(string message) : base(message) { }
        public SecurePayException(string statusCode, string description)
            : base(string.Format("{0}: {1}", statusCode, description))
        {
        }

        // TODO: these fields will not serialize across contexts or appdomains

        /// <summary>
        /// Request XML as a string. Useful for diagnostics.
        /// </summary>
        public string Request;

        /// <summary>
        /// Response XML as a string. Useful for diagnostics.
        /// </summary>
        public string Response;
    }
}