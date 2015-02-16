using System.Xml.Serialization;

namespace SecurePay.Messages
{
    public class SecurePayMessage
    {
        public MessageInfo MessageInfo = new MessageInfo();
        public MerchantInfo MerchantInfo = new MerchantInfo();
        public string RequestType;
    }

    public class MessageInfo
    {
        [XmlElement("messageID")]
        public string MessageId;

        [XmlElement("messageTimestamp")]
        public string MessageTimestamp;

        [XmlElement("timeoutValue")]
        public int TimeoutValue = 60;

        [XmlElement("apiVersion")]
        public string ApiVersion;
    }

    public class MerchantInfo
    {
        [XmlElement("merchantID")]
        public string MerchantId;

        [XmlElement("password")]
        public string Password;
    }

    [XmlRoot("SecurePayMessage")]
    public class SecurePayResponseMessage : SecurePayMessage
    {
        public ResponseStatus Status;
    }

    public class ResponseStatus
    {
        [XmlElement("statusCode")]
        public string StatusCode;

        [XmlElement("statusDescription")]
        public string Description;
    }
}
