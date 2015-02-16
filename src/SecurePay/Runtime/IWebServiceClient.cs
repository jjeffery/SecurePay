using System.Threading.Tasks;
using SecurePay.Messages;

namespace SecurePay.Runtime
{
    /// <summary>
    /// Base interface for web service client interfaces.
    /// </summary>
    public interface IWebServiceClient
    {
        ClientConfig Config { get; }
		Task<EchoResponseMessage> EchoAsync();
    }
}