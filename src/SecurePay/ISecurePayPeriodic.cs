using System.Threading.Tasks;
using SecurePay.Messages;
using SecurePay.Model;

namespace SecurePay
{
    public interface ISecurePayPeriodic
    {
        Task<EchoResponseMessage> EchoAsync();
        Task<AddTriggeredPaymentResponse> AddTriggeredPaymentAsync(AddTriggeredPaymentRequest request);
        Task<DeletePaymentResponse> DeletePaymentAsync(DeletePaymentRequest request);
        Task<TriggerPaymentResponse> TriggerPaymentAsync(TriggerPaymentRequest request);
    }
}