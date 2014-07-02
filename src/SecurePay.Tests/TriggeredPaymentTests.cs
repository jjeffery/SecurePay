using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecurePay.Model;

namespace SecurePay.Tests
{
    [TestClass]
    public class TriggeredPaymentTests
    {
        public string ClientId = "X987654-1";
        public ClientConfig Config;
        public PeriodicClient Client;
        public int PaymentAmount;
        public string CardNumber;
        public YearMonth Expires;
        public string Ccv;

        [TestInitialize]
        public void TestInitialize()
        {
            Config = new ClientConfig().SetupForTesting();
            Client = new PeriodicClient(Config);

            PaymentAmount = 3000;
            CardNumber = "4444333322221111";
            Expires = new YearMonth(DateTime.Today.Year + 1, DateTime.Today.Month);
            Ccv = "123";
        }

        [TestMethod]
        public async Task SuccessfulPayment()
        {
            await AddTriggeredPayment();

            try
            {
                var triggerRequest = new TriggerPaymentRequest {
                    ClientId = ClientId
                };

                var triggerResponse = await Client.TriggerPaymentAsync(triggerRequest);
                Assert.AreEqual(true, triggerResponse.Successful);
                Assert.AreEqual(3000, triggerResponse.Amount);
                Assert.AreEqual("444433...111", triggerResponse.CreditCard.TruncatedCardNumber);
            }
            catch
            {
                LogMessages();
                throw;
            }

            await DeleteTriggeredPayment();
        }

        [TestMethod]
        public async Task InsufficientFunds()
        {
            PaymentAmount = 3051;
            await AddTriggeredPayment();

            try
            {
                var triggerRequest = new TriggerPaymentRequest
                {
                    ClientId = ClientId,
                };

                var triggerResponse = await Client.TriggerPaymentAsync(triggerRequest);
                Assert.AreEqual(false, triggerResponse.Successful);
                Assert.AreEqual(PaymentAmount, triggerResponse.Amount);
                Assert.AreEqual("444433...111", triggerResponse.CreditCard.TruncatedCardNumber);
                Assert.AreEqual("51", triggerResponse.ResponseCode);
                Assert.AreEqual("Insufficient Funds", triggerResponse.ResponseText);
            }
            catch
            {
                LogMessages();
                throw;
            }

            await DeleteTriggeredPayment();
        }

        [TestMethod]
        public async Task IncorrectCardNumber()
        {
            CardNumber = "1234567890123456";
            var addResponse = await AddTriggeredPayment(false);

            try
            {
                Assert.AreEqual(false, addResponse.Successful);
                Assert.AreEqual("123456...456", addResponse.CreditCard.TruncatedCardNumber);
                Assert.AreEqual("301", addResponse.ResponseCode);
                Assert.AreEqual("Invalid Credit Card Number", addResponse.ResponseText);
            }
            catch
            {
                LogMessages();
                throw;
            }

            await DeleteTriggeredPayment();
        }

        private async Task<AddTriggeredPaymentResponse> AddTriggeredPayment(bool expectSuccess = true)
        {
            var addRequest = new AddTriggeredPaymentRequest
            {
                Amount = PaymentAmount,
                ClientId = ClientId,
                CreditCard = new CreditCardRequest
                {
                    CardNumber = CardNumber,
                    Expires = Expires,
                    Cvv = Ccv,
                }
            };

            try
            {
                var addResponse = await Client.AddTriggeredPaymentAsync(addRequest);
                if (!addResponse.Successful && addResponse.ResponseCode == "346")
                {
                    await DeleteTriggeredPayment();
                    addResponse = await Client.AddTriggeredPaymentAsync(addRequest);
                }

                Assert.AreEqual(expectSuccess, addResponse.Successful);
                return addResponse;
            }
            catch
            {
                LogMessages();
                throw;
            }
        }

        private async Task DeleteTriggeredPayment()
        {
            try
            {
                var deleteRequest = new DeletePaymentRequest {ClientId = ClientId};
                var deleteResponse = await Client.DeletePaymentAsync(deleteRequest);
                Assert.AreEqual(true, deleteResponse.Successful);
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
