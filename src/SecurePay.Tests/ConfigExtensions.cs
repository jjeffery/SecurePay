namespace SecurePay.Tests
{
    public static class ConfigExtensions
    {
        public static ClientConfig SetupForTesting(this ClientConfig config)
        {
            config.MerchantId = "ABC0001";
            config.Password = "abc123";
            config.Url = "https://test.securepay.com.au";
            return config;
        }
    }
}