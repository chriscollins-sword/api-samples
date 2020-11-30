using ApiDemoConsoleApplication.ArmService;
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;

namespace ApiDemoConsoleApplication
{
    class PublicClientProvider
    {
        public static PublicServiceSoapClient GetClient()
        {
            var client = new PublicServiceSoapClient(GetBinding(), GetEndpointAddress());

            AuthenticationConfigurer.ConfigureAuthentication(client);

            return client;
        }

        static Binding GetBinding()
        {
            if (ApiSettings.UseSsl)
            {
                var binding = new BasicHttpsBinding();
                binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Ntlm;
                binding.Security.Transport.ProxyCredentialType = HttpProxyCredentialType.None;
                binding.Security.Message.ClientCredentialType = BasicHttpMessageCredentialType.UserName;
                binding.MaxReceivedMessageSize = 6553600;
                binding.MessageEncoding = WSMessageEncoding.Text;
                binding.TextEncoding = Encoding.UTF8;
                binding.Security.Mode = BasicHttpsSecurityMode.Transport;
                return binding;
            }
            else
            {
                var binding = new BasicHttpBinding();
                binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Ntlm;
                binding.Security.Transport.ProxyCredentialType = HttpProxyCredentialType.None;
                binding.Security.Message.ClientCredentialType = BasicHttpMessageCredentialType.UserName;
                binding.MaxReceivedMessageSize = 6553600;
                binding.MessageEncoding = WSMessageEncoding.Text;
                binding.TextEncoding = Encoding.UTF8;
                binding.Security.Mode = BasicHttpSecurityMode.TransportCredentialOnly;
                return binding;
            }
        }

        static EndpointAddress GetEndpointAddress()
        {
            return new EndpointAddress(Path.Combine(ApiSettings.ServerUrl, "webservices/public/publicservice.asmx"));
        }

        public static ArmSoapHeader GetArmSoapHeader()
        {
            var header = new ArmSoapHeader();
            header.InstanceID = ApiSettings.InstanceId;
            header.BusinessAreaID = ApiSettings.BusinessAreaId;
            header.ClientVersion = ApiSettings.ClientVersion;
            return header;
        }
    }
}
