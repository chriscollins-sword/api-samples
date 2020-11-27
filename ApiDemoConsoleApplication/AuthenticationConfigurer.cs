using ApiDemoConsoleApplication.ArmApi;
using Microsoft.Web.Services3.Security.Tokens;
using System.IO;
using System.Net;
using System.Security;
using System.ServiceModel.Channels;
using System.Xml;

namespace ApiDemoConsoleApplication
{
    class AuthenticationConfigurer
    {
        public static void ConfigureAuthentication(PublicService service)
        {
            switch (ApiSettings.AuthenticationMode)
            {
                case AuthenticationMode.Ntlm:
                    ConfigureNtlm(service);
                    break;
                case AuthenticationMode.Forms:
                    ConfigureForms(service);
                    break;
                case AuthenticationMode.Saml:
                    ConfigureSaml(service);
                    break;
                default:
                    throw new SecurityException("Authentication mode not recognised");
            }
        }

        static void ConfigureNtlm(PublicService service)
        {
            service.Credentials = new NetworkCredential(ApiSettings.UserName, ApiSettings.Password, ApiSettings.Domain);

        }
        
        static void ConfigureForms(PublicService service)
        {
            var formsAuth = new FormsAuthenticator.OfflineFormsAuthenticator();
            formsAuth.CookieContainer = new CookieContainer();
            formsAuth.Url = Path.Combine(ApiSettings.ServerUrl, "Secure/Services/OfflineFormsAuthenticator.asmx");
            var loginSuccessful = formsAuth.Login(ApiSettings.UserName, ApiSettings.Password);
            service.CookieContainer = formsAuth.CookieContainer;
            service.Credentials = CredentialCache.DefaultCredentials;
        }

        static void ConfigureSaml (PublicService service)
        {
         //   service.Credentials = new NetworkCredential(ApiSettings.UserName, ApiSettings.Token);
            // Use the WSE 3.0 security token class
            var token = new UsernameToken(ApiSettings.UserName, ApiSettings.Token, PasswordOption.SendPlainText);

            // Serialize the token to XML
           // var securityToken = token.GetXml(new XmlDocument());
           // var securityHeader = MessageHeader.CreateHeader("Security", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd", securityToken, false);
            service.SetClientCredential(token);
            service.SetPolicy("ClientPolicy");
        }
    }
}
