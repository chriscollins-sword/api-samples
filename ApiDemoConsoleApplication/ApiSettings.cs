using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiDemoConsoleApplication
{
    class ApiSettings
    {
        public static string Url = "http://localhost/arm/webservices/public/publicservice.asmx";
        public static string UserName = "armtest";
        public static string Domain = "whiskers";
        public static string Password = "armtest1";
        public static string Token = "";
        public static string ClientVersion = "4.0.2.0";
        public static int InstanceId = 1;
        public static int BusinessAreaId = 1;
        public static AuthenticationMode AuthenticationMode = AuthenticationMode.Ntlm;

    }
}
