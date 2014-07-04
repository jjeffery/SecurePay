using System.Threading.Tasks;
using SecurePay.Messages;
using SecurePay.Model;
using SecurePay.Runtime;

namespace SecurePay
{
    public interface ISecurePayPeriodic : IWebServiceClient
    {
        Task<EchoResponseMessage> EchoAsync();
        Task<AddTriggeredPaymentResponse> AddTriggeredPaymentAsync(AddTriggeredPaymentRequest request);
        Task<DeletePaymentResponse> DeletePaymentAsync(DeletePaymentRequest request);
        Task<TriggerPaymentResponse> TriggerPaymentAsync(TriggerPaymentRequest request);
    }
}