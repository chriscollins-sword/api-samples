using ApiDemoConsoleApplication.ArmService;
using Microsoft.Web.Services3.Security.Tokens;
using System.IO;
using System.Net;
using System.Security;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Xml;

namespace ApiDemoConsoleApplication
{
    class AuthenticationConfigurer
    {
        public static void ConfigureAuthentication(PublicServiceSoapClient client)
        {
            switch (ApiSettings.AuthenticationMode)
            {
                case AuthenticationMode.Ntlm:
                    ConfigureNtlm(client);
                    break;
                case AuthenticationMode.Forms:
                    ConfigureForms(client);
                    break;
                case AuthenticationMode.Saml:
                    ConfigureSaml(client);
                    break;
                default:
                    throw new SecurityException("Authentication mode not recognised");
            }
        }

        static void ConfigureNtlm(PublicServiceSoapClient service)
        {
            service.ClientCredentials.Windows.ClientCredential = new NetworkCredential(ApiSettings.UserName, ApiSettings.Password, ApiSettings.Domain);

        }

        static void ConfigureForms(PublicServiceSoapClient client)
        {
            new OperationContextScope(client.InnerChannel);
            var formsAuth = new FormsAuthenticator.OfflineFormsAuthenticator();
            formsAuth.CookieContainer = new CookieContainer();
            formsAuth.Url = Path.Combine(ApiSettings.ServerUrl, "Secure/Services/OfflineFormsAuthenticator.asmx");
            var loginSuccessful = formsAuth.Login(ApiSettings.UserName, ApiSettings.Password);
            HttpRequestMessageProperty requestMessage = new HttpRequestMessageProperty();
            requestMessage.Headers["cookie"] = formsAuth.CookieContainer.GetCookieHeader(new System.Uri(ApiSettings.ServerUrl));
            OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = requestMessage;
        }

        static void ConfigureSaml(PublicServiceSoapClient client)
        {
            new OperationContextScope(client.InnerChannel);
            var token = new UsernameToken(ApiSettings.UserName, ApiSettings.Token, PasswordOption.SendPlainText);

            var securityToken = token.GetXml(new XmlDocument());

            var securityHeader = MessageHeader.CreateHeader("Security", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd", securityToken, false);
            OperationContext.Current.OutgoingMessageHeaders.Add(securityHeader);
        }
    }
}
