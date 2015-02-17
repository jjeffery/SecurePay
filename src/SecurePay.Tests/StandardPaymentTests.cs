using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecurePay.Model;

namespace SecurePay.Tests
{
    [TestClass]
    public class StandardPaymentTests
    {
        public string PurchaseOrder = "X987654";
        public ClientConfig Config;
        public PaymentClient Client;
        public int PaymentAmount;
        public string CardNumber;
        public YearMonth Expires;
        public string Ccv;

        [TestInitialize]
        public void TestInitialize()
        {
            Config = new ClientConfig().SetupForTesting();
            Client = new PaymentClient(Config);

            PaymentAmount = 3000;
            CardNumber = "4444333322221111";
            Expires = new YearMonth(DateTime.Today.Year + 1, DateTime.Today.Month);
            Ccv = "123";
        }

        [TestMethod]
        public async Task SuccessfulPayment()
        {
            var response = await StandardPayment();

	        try
	        {
				Assert.AreEqual(true, response.Approved);
				Assert.AreEqual("444433...111", response.CreditCard.TruncatedCardNumber);
				Assert.AreEqual("00", response.ResponseCode);
				Assert.AreEqual("Approved", response.ResponseText);
		        Assert.IsFalse(string.IsNullOrWhiteSpace(response.TransactionId));
		        //LogMessages();
	        }
	        catch (Exception ex)
	        {
		        LogMessages();
		        throw;
	        }
        }

        [TestMethod]
        public async Task InsufficientFunds()
        {
            PaymentAmount = 3051;
	        var response = await StandardPayment(false);

			try {
				Assert.AreEqual(false, response.Approved);
				Assert.AreEqual("444433...111", response.CreditCard.TruncatedCardNumber);
				Assert.AreEqual("51", response.ResponseCode);
				Assert.AreEqual("Insufficient Funds", response.ResponseText);
			}
			catch {
				LogMessages();
				throw;
			}
        }

        [TestMethod]
        public async Task IncorrectCardNumber()
        {
            CardNumber = "1234567890123456";
            var response = await StandardPayment(false);

            try
            {
                Assert.AreEqual(false, response.Approved);
                Assert.AreEqual("123456...456", response.CreditCard.TruncatedCardNumber);
                Assert.AreEqual("101", response.ResponseCode);
                Assert.AreEqual("Invalid Credit Card Number", response.ResponseText);
            }
            catch
            {
                LogMessages();
                throw;
            }
        }

        private async Task<StandardPaymentResponse> StandardPayment(bool expectSuccess = true)
        {
	        var request = new StandardPaymentRequest {
		        Amount = PaymentAmount,
		        PurchaseOrder = PurchaseOrder,
		        CreditCard = new CreditCardRequest {
			        CardNumber = CardNumber,
			        Expires = Expires,
			        Cvv = Ccv,
		        }
	        };

            try
            {
                var response = await Client.StandardPaymentAsync(request);
                Assert.AreEqual(expectSuccess, response.Approved);
                return response;
            }
            catch
            {
                LogMessages();
                throw;
            }
        }

        private void LogMessages()
        {
            Console.WriteLine("Request:");
            Console.WriteLine("========");
            Console.WriteLine(Client.LastRequest);
            Console.WriteLine("Response:");
            Console.WriteLine("=========");
            Console.WriteLine(Client.LastResponse);
        }
    }
}
