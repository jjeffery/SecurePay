using System;
using System.Configuration;

namespace SecurePay
{
    /// <summary>
    /// Configuration required for successfully interacting with SecurePay XML services.
    /// </summary>
    public class ClientConfig
    {
        /// <summary>
        /// The merchant ID for authentication with SecurePay
        /// </summary>
        public string MerchantId { get; set; }

        /// <summary>
        /// Password associated with the <see cref="MerchantId"/> property.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// The URL of the SecurePay service. Requires just the URL scheme and the hostname.
        /// Examples "https://test.securepay.com.au" and "https://api.securepay.com.au".
        /// </summary>
        /// <remarks>
        /// The endpoint used depends on the type of client. For example the period client sends
        /// all requests to the endpoing "/xmlapi/periodic".
        /// </remarks>
        public string Url { get; set; }

        public ClientConfig()
        {
            MerchantId = ConfigurationManager.AppSettings["SecurePay.MerchantId"];
            Password = ConfigurationManager.AppSettings["SecurePay.Password"];
            Url = ConfigurationManager.AppSettings["SecurePay.Url"];
        }
    }
}
