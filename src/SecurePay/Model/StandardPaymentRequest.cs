using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurePay.Model
{
	public class StandardPaymentRequest
	{
		public string PurchaseOrder;
		public int Amount;
		public string Currency = "AUD";
		public CreditCardRequest CreditCard = new CreditCardRequest();
	}
}
