using ApiDemoConsoleApplication.ArmService;
using System;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;

namespace ApiDemoConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            var service = new PublicServiceSoapClient(GetBinding(), GetEndpointAddress());

            service.ClientCredentials.Windows.ClientCredential = new NetworkCredential(Credentials.UserName, Credentials.Password, Credentials.Domain);
          
            RiskTransfer[] risks;
            int totalPages = 0;
            var impacts = service.GetImpactsForFilter(GetArmSoapHeader(), 1, 1, 1, 1, 1, 5000,out risks, out totalPages);

            foreach(var impact in impacts)
            {
                var risk = risks.First(r => r.Id == impact.RiskId);
                Console.WriteLine($"{impact.Id}: {risk.Name}");
            }
            Console.ReadLine();
        }

        static ArmSoapHeader GetArmSoapHeader()
        {
            var header =  new ArmSoapHeader();
            header.InstanceID = 1;
            header.BusinessAreaID = 1;
            header.ClientVersion = "4.0.2.0";
            return header;
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
            binding.Security.Mode = BasicHttpSecurityMode.TransportCredentialOnly;
            return binding;
        }

        static EndpointAddress GetEndpointAddress()
        {
            return new EndpointAddress("http://localhost/arm/webservices/public/publicservice.asmx");
        }
    }
}
