using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using SecurePay.Messages;

namespace SecurePay.Runtime
{
    public abstract class WebServiceClient
    {
        public readonly ClientConfig Config;
        private byte[] _lastRequest;
        private byte[] _lastResponse;

        public string LastRequest
        {
            get { return Encoding.UTF8.GetString(_lastRequest); }
        }

        public string LastResponse
        {
            get { return Encoding.UTF8.GetString(_lastResponse); }
        }

        protected WebServiceClient(ClientConfig config)
        {
            Config = config ?? new ClientConfig();
        }

        protected async Task<TResponse> PostAsync<TRequest, TResponse>(TRequest requestMessage) 
            where TRequest: SecurePayMessage
            where TResponse: SecurePayResponseMessage
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/xml"));
            var url = GetServiceUrl();

            requestMessage.MessageInfo.MessageId = Guid.NewGuid().ToString();
            requestMessage.MessageInfo.MessageTimestamp = DateTimeOffset.Now.ToSecurePayTimestampString();
            requestMessage.MerchantInfo.MerchantId = Config.MerchantId;
            requestMessage.MerchantInfo.Password = Config.Password;
            var httpContent = CreateContent(requestMessage);

            var httpResponseMessage = await httpClient.PostAsync(url, httpContent);

            _lastResponse = await httpResponseMessage.Content.ReadAsByteArrayAsync();

            using (var stream = new MemoryStream(_lastResponse))
            {
                var serializer = new XmlSerializer(typeof (TResponse));
                var responseMessage = (TResponse)serializer.Deserialize(stream);

                if (responseMessage.Status == null)
                {
                    throw new SecurePayException("Missing status in response") {
                        Request = LastRequest,
                        Response = LastResponse
                    };
                }

                int statusCode;
                if (!int.TryParse(responseMessage.Status.StatusCode, out statusCode) || statusCode != 0)
                {
                    throw new SecurePayException(responseMessage.Status.StatusCode, responseMessage.Status.Description) {
                        Request = LastRequest,
                        Response = LastResponse
                    };
                }

                return responseMessage;
            }
        }

        protected HttpContent CreateContent<T>(T requestMessage)
        {
            using (var stream = new MemoryStream())
            {
                var serializer = new XmlSerializer(typeof (T));
                serializer.Serialize(stream, requestMessage);
                _lastRequest = stream.ToArray();
                var content = new ByteArrayContent(_lastRequest);
                // Content type of 'application/xml' is more normal for XML these days,
                // but the SecurePay documentation examples use 'text/xml'.
                content.Headers.ContentType = new MediaTypeHeaderValue("text/xml");
                return content;
            }
        }

        protected abstract Uri GetServiceUrl();
    }
}
