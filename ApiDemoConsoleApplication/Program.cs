using ApiDemoConsoleApplication.ArmApi;
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
            var service = PublicServiceProvider.GetService();
            AuthenticationConfigurer.ConfigureAuthentication(service);

            RiskTransfer[] risks;
            int totalPages = 0;

            var itemId = -2; // all items
            var recordTypeId = 0; // all record types
            short treeType = 0; // all tree type
            var filterId = -2; // no filter
            var pageNumber = 1; // first page
            short recordsPerPage = 5000; // 5000 records per page

            var impacts = service.GetImpactsForFilter(itemId, recordTypeId, treeType, filterId, pageNumber, recordsPerPage,out risks, out totalPages);

            foreach(var impact in impacts)
            {
                var risk = risks.First(r => r.Id == impact.RiskId);
                var responses = service.GetResponsesForPlan(impact.PlanId, itemId);

                Console.WriteLine($"{impact.Id}: {risk.Name}");
                foreach(var response in responses)
                {
                    Console.WriteLine($"Response: {response.Id} - {response.Name} {response.AchievedScore}");
                }
                Console.WriteLine("------------------------");
                Console.WriteLine();
            }
            Console.ReadLine();
        }
    }
}
