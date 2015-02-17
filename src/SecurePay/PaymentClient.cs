using System;
using System.Threading.Tasks;
using SecurePay.Messages;
using SecurePay.Model;
using SecurePay.Runtime;

namespace SecurePay
{
	public class PaymentClient : WebServiceClient, ISecurePayPayment
	{
		public PaymentClient(ClientConfig config = null) : base(config, "xml-4.2")
		{
		}

		public async Task<StandardPaymentResponse> StandardPaymentAsync(StandardPaymentRequest request)
		{
			var requestMessage = new PaymentRequestMessage();
			var transaction = requestMessage.Payment.PaymentList.Transaction;
			transaction.TransactionType = PaymentTransactionType.StandardPayment;
			transaction.TransactionSource = PaymentTransactionSource.Xml;
			transaction.PurchaseOrderNumber = request.PurchaseOrder;
			transaction.Amount = request.Amount;
			transaction.Currency = request.Currency;
			transaction.CreditCardInfo = new CreditCardInfo {
				CardNumber = request.CreditCard.CardNumber,
				Cvv = request.CreditCard.Cvv,
				ExpiryDate = request.CreditCard.Expires.ToSecurePayString(),
			};

			var responseMessage = await PostAsync<PaymentRequestMessage, PaymentResponseMessage>(requestMessage);
			transaction = responseMessage.Payment.PaymentList.Transaction;

			var response = new StandardPaymentResponse {
				Approved = transaction.Approved == "Yes",
				PurchaseOrder = transaction.PurchaseOrderNumber,
				ResponseCode = transaction.ResponseCode,
				ResponseText = transaction.ReponseText,
				Amount = transaction.Amount,
				Currency = transaction.Currency,
				TransactionId = transaction.TransactionId,
				SettlementDate = DateUtils.ParseYyyymmdd(transaction.SettlementDate),
			};

			if (transaction.CreditCardInfo != null)
			{
				response.CreditCard = new CreditCardResponse {
					Expires = new YearMonth(transaction.CreditCardInfo.ExpiryDate),
					TruncatedCardNumber = transaction.CreditCardInfo.TruncatedCreditCardNumber,
					CardDescription = transaction.CreditCardInfo.CardDescription,
				};
			}

			return response;
		}

		public async Task<PreauthResponse> PreauthAsync(PreauthRequest request)
		{
			var requestMessage = new PaymentRequestMessage();
			var transaction = requestMessage.Payment.PaymentList.Transaction;
			transaction.TransactionType = PaymentTransactionType.Preauthorise;
			transaction.TransactionSource = PaymentTransactionSource.Xml;
			transaction.PurchaseOrderNumber = request.PurchaseOrder;
			transaction.Amount = request.Amount;
			transaction.Currency = request.Currency;
			transaction.CreditCardInfo = new CreditCardInfo {
				CardNumber = request.CreditCard.CardNumber,
				Cvv = request.CreditCard.Cvv,
				ExpiryDate = request.CreditCard.Expires.ToSecurePayString(),
			};

			var responseMessage = await PostAsync<PaymentRequestMessage, PaymentResponseMessage>(requestMessage);
			transaction = responseMessage.Payment.PaymentList.Transaction;

			var response = new PreauthResponse {
				Approved = transaction.Approved == "Yes",
				PurchaseOrder = transaction.PurchaseOrderNumber,
				ResponseCode = transaction.ResponseCode,
				ResponseText = transaction.ReponseText,
				Amount = transaction.Amount,
				Currency = transaction.Currency,
				TransactionId = transaction.TransactionId,
				PreauthId = transaction.PreauthId,
				SettlementDate = DateUtils.ParseYyyymmdd(transaction.SettlementDate),
			};

			if (transaction.CreditCardInfo != null) {
				response.CreditCard = new CreditCardResponse {
					Expires = new YearMonth(transaction.CreditCardInfo.ExpiryDate),
					TruncatedCardNumber = transaction.CreditCardInfo.TruncatedCreditCardNumber,
					CardDescription = transaction.CreditCardInfo.CardDescription,
				};
			}

			return response;
		}

		protected override Uri GetServiceUrl()
		{
			var baseUrl = new Uri(Config.Url);
			return new Uri(baseUrl, "/xmlapi/payment");
		}
	}
}
