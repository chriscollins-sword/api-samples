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
            var binding = new BasicHttpBinding();
            binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Ntlm;
            binding.Security.Transport.ProxyCredentialType = HttpProxyCredentialType.None;
            binding.Security.Message.ClientCredentialType = BasicHttpMessageCredentialType.UserName;
            binding.MaxReceivedMessageSize = 6553600;
            binding.MessageEncoding = WSMessageEncoding.Text;
            binding.TextEncoding = Encoding.UTF8;
            binding.Security.Mode = BasicHttpSecurityMode.Transport;
            return binding;
        }

        static EndpointAddress GetEndpointAddress()
        {
            return new EndpointAddress(Path.Combine(ApiSettings.ServerUrl, "webservices/public/publicservice.asmx"));
        }
    }
}
