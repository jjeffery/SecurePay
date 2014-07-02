using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SecurePay.Tests
{
    [TestClass]
    public class EchoTests
    {
        [TestMethod]
        public async Task Echo()
        {
            var config = new ClientConfig().SetupForTesting();
            var client = new PeriodicClient(config);

            var response = await client.EchoAsync();
            Assert.IsNotNull(response);
            Console.WriteLine("Request:");
            Console.WriteLine(client.LastRequest);
            Console.WriteLine("Response:");
            Console.WriteLine(client.LastResponse);
            Assert.AreEqual("000", response.Status.StatusCode, "Unexpected status code");
            Assert.AreEqual("Normal", response.Status.Description);
        }

        [TestMethod]
        public async Task InvalidPassword()
        {
            var config = new ClientConfig().SetupForTesting();

            // modify password so it is now incorrect
            config.Password += "!!";

            var client = new PeriodicClient(config);

            try
            {
                await client.EchoAsync();
                Assert.Fail("Expected exception");
            }
            catch (SecurePayException ex)
            {
                Assert.AreEqual("504: Invalid merchant ID", ex.Message);
            }
            catch (Exception)
            {
                Console.WriteLine("Request:");
                Console.WriteLine(client.LastRequest);
                Console.WriteLine("Response:");
                Console.WriteLine(client.LastResponse);
                throw;
            }
        }

        [TestMethod]
        public async Task InvalidMerchantId()
        {
            var config = new ClientConfig().SetupForTesting();

            // modify password so it is now incorrect
            config.MerchantId = "!!!0099";

            var client = new PeriodicClient(config);

            try
            {
                await client.EchoAsync();
                Assert.Fail("Expected exception");
            }
            catch (SecurePayException ex)
            {
                Assert.AreEqual("504: Invalid merchant ID", ex.Message);
            }
            catch (Exception)
            {
                Console.WriteLine("Request:");
                Console.WriteLine(client.LastRequest);
                Console.WriteLine("Response:");
                Console.WriteLine(client.LastResponse);
                throw;
            }
        }
    }
}
