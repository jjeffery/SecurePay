using System;
using System.Threading.Tasks;
using SecurePay.Messages;
using SecurePay.Model;
using SecurePay.Runtime;

namespace SecurePay
{
    public class PeriodicClient : WebServiceClient, ISecurePayPeriodic
    {
        public PeriodicClient(ClientConfig config = null) : base(config, "spxml-3.0")
        {
        }

        public async Task<AddTriggeredPaymentResponse> AddTriggeredPaymentAsync(AddTriggeredPaymentRequest request)
        {
            var requestMessage = new PeriodicRequestMessage();
            var periodicItem = requestMessage.Periodic.PeriodicList.PeriodicItem;
            periodicItem.ActionType = "add";
            periodicItem.ClientId = request.ClientId;
            periodicItem.CreditCardInfo = new CreditCardInfo {
                CardNumber = request.CreditCard.CardNumber,
                Cvv = request.CreditCard.Cvv,
                ExpiryDate = request.CreditCard.Expires.ToSecurePayString()
            };
            periodicItem.Amount = request.Amount;
            periodicItem.PeriodicType = 4;

            var responseMessage = await PostAsync<PeriodicRequestMessage, PeriodicResponseMessage>(requestMessage);
            periodicItem = responseMessage.Periodic.PeriodicList.PeriodicItem;

            var response = new AddTriggeredPaymentResponse {
                Successful = periodicItem.Successful == "yes",
                ResponseCode = periodicItem.ResponseCode,
                ResponseText = periodicItem.ResponseText,
                Amount = periodicItem.Amount ?? 0,
            };

            if (periodicItem.CreditCardInfo != null)
            {
                response.CreditCard = new CreditCardResponse {
                    Expires = new YearMonth(periodicItem.CreditCardInfo.ExpiryDate),
                    TruncatedCardNumber = periodicItem.CreditCardInfo.TruncatedCreditCardNumber,
                    CardDescription = periodicItem.CreditCardInfo.CardDescription,
                };
            }

            return response;
        }

        public async Task<DeletePaymentResponse> DeletePaymentAsync(DeletePaymentRequest request)
        {
            var requestMessage = new PeriodicRequestMessage();
            var periodicItem = requestMessage.Periodic.PeriodicList.PeriodicItem;
            periodicItem.ActionType = "delete";
            periodicItem.ClientId = request.ClientId;

            var responseMessage = await PostAsync<PeriodicRequestMessage, PeriodicResponseMessage>(requestMessage);
            periodicItem = responseMessage.Periodic.PeriodicList.PeriodicItem;

            var response = new DeletePaymentResponse
            {
                Successful = periodicItem.Successful == "yes",
                ResponseCode = periodicItem.ResponseCode,
                ResponseText = periodicItem.ResponseText,
            };

            return response;
        }

        public async Task<TriggerPaymentResponse> TriggerPaymentAsync(TriggerPaymentRequest request)
        {
            var requestMessage = new PeriodicRequestMessage();
            var periodicItem = requestMessage.Periodic.PeriodicList.PeriodicItem;
            periodicItem.ActionType = "trigger";
            periodicItem.ClientId = request.ClientId;
            periodicItem.Amount = request.Amount;
            periodicItem.TransactionReference = request.TransactionReference;
            var responseMessage = await PostAsync<PeriodicRequestMessage, PeriodicResponseMessage>(requestMessage);
            periodicItem = responseMessage.Periodic.PeriodicList.PeriodicItem;

            var response = new TriggerPaymentResponse
            {
                Successful = periodicItem.Successful == "yes",
                ResponseCode = periodicItem.ResponseCode,
                ResponseText = periodicItem.ResponseText,
                Amount =  periodicItem.Amount ?? 0,
                Currency = periodicItem.Currency,
                TransactionId = periodicItem.TransactionId,
                Receipt = periodicItem.Receipt,
                PoNum = periodicItem.PoNum,
                SettlementDate = DateUtils.ParseYyyymmdd(periodicItem.SettlementDate),
            };

            if (periodicItem.CreditCardInfo != null)
            {
                response.CreditCard = new CreditCardResponse {
                    CardDescription = periodicItem.CreditCardInfo.CardDescription,
                    Expires = new YearMonth(periodicItem.CreditCardInfo.ExpiryDate),
                    TruncatedCardNumber = periodicItem.CreditCardInfo.TruncatedCreditCardNumber,
                };
            }

            return response;
        }

        protected override Uri GetServiceUrl()
        {
            var baseUrl = new Uri(Config.Url);
            return new Uri(baseUrl, "/xmlapi/periodic");
        }
    }
}