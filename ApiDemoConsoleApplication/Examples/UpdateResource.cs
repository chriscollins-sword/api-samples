using System;
using System.Linq;

namespace ApiDemoConsoleApplication
{
    class UpdateResource
    {
        public static void Run()
        {
            var client = PublicClientProvider.GetClient();

            //load list of roles in ARM
            var roles = client.GetResourceRoles(PublicClientProvider.GetArmSoapHeader());

            //load resource
            var resource = client.GetResource(PublicClientProvider.GetArmSoapHeader(), 23);

            var originalRole = roles.Single(r => r.ID == resource.Role.ID);

            Console.WriteLine($"Resource {resource.Id}: {resource.Name}");
            Console.WriteLine($"Resource role is {resource.Role.Name}");

            //replace resource role with another
            resource.Role = roles.First(r => r.ID != resource.Role.ID);

            Console.WriteLine($"Updating Resource role from {originalRole.Name} to {resource.Role.Name}");

            //update resource via api
            client.UpdateResource(PublicClientProvider.GetArmSoapHeader(), resource, false);


            //reload resource
            resource = client.GetResource(PublicClientProvider.GetArmSoapHeader(), 23);

            Console.WriteLine($"Resource role is now {resource.Role.Name}");

            //replace original role
            resource.Role = originalRole;

            //update resource again
            client.UpdateResource(PublicClientProvider.GetArmSoapHeader(), resource, false);

            Console.WriteLine($"Resource role returned to {resource.Role.Name}");



        }
    }
}
