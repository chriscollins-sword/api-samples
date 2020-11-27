using ApiDemoConsoleApplication.ArmApi;
using System.Net;
using System.Security;

namespace ApiDemoConsoleApplication
{
    class AuthenticationConfigurer
    {
        public static void ConfigureAuthentication(PublicService service)
        {
            switch (ApiSettings.AuthenticationMode)
            {
                case AuthenticationMode.Ntlm:
                    service.Credentials = new NetworkCredential(ApiSettings.UserName, ApiSettings.Password, ApiSettings.Domain);
                    break;
                default:
                    throw new SecurityException("Authentication mode not recognised");
            }
        }
    }
}
