using System.Xml.Serialization;

namespace SecurePay.Messages
{
    [XmlRoot("SecurePayMessage")]
    public class EchoRequestMessage : SecurePayMessage
    {
        public EchoRequestMessage()
        {
            RequestType = "Echo";
        }
    }

    [XmlRoot("SecurePayMessage")]
    public class EchoResponseMessage : SecurePayResponseMessage
    {
    }
}