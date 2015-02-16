using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecurePay.Messages;
using SecurePay.Model;
using SecurePay.Runtime;

namespace SecurePay
{
	public interface ISecurePayPayment : IWebServiceClient
	{
		Task<StandardPaymentResponse> StandardPaymentAsync(StandardPaymentRequest request);
	}
}
