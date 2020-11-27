using ApiDemoConsoleApplication.ArmApi;

namespace ApiDemoConsoleApplication
{
    class PublicServiceProvider
    {
        public static PublicService GetService()
        {
            var proxy = new PublicService();
            proxy.ArmSoapHeaderValue = new ArmSoapHeader();
            proxy.ArmSoapHeaderValue.InstanceID = ApiSettings.InstanceId;
            proxy.ArmSoapHeaderValue.BusinessAreaID = ApiSettings.BusinessAreaId;
            proxy.ArmSoapHeaderValue.ClientVersion = ApiSettings.ClientVersion; ;

            proxy.Url = ApiSettings.Url;
            return proxy;
        }
    }
}
