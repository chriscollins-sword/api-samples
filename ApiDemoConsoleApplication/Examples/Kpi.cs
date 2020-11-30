using ApiDemoConsoleApplication.ArmService;
using System;
using System.Linq;

namespace ApiDemoConsoleApplication
{
    class Kpi
    {
        public static void Run()
        {
            var client = PublicClientProvider.GetClient();

            RiskTransfer[] risks;
            int totalPages = 0;

            var itemId = -2; // all items
            var recordTypeId = 0; // all record types
            short treeType = 0; // all tree type
            var filterId = 21; // filter configured for required kpi data
            var pageNumber = 1; // first page
            short recordsPerPage = 5000; // 5000 records per page

            var impacts = client.GetImpactsForFilter(
                PublicClientProvider.GetArmSoapHeader(),
                itemId,
                recordTypeId,
                treeType,
                filterId,
                pageNumber,
                recordsPerPage,
                out risks,
                out totalPages);

            foreach (var impact in impacts)
            {
                var risk = risks.First(r => r.Id == impact.RiskId);

                var responses = client.GetResponsesForPlan(PublicClientProvider.GetArmSoapHeader(), impact.PlanId, itemId);

                Console.WriteLine($"{impact.Id}: {risk.Name}");
                foreach (var response in responses)
                {
                    Console.WriteLine($"Response: {response.Id} - {response.Name} {response.AchievedScore}");
                }
                Console.WriteLine("------------------------");
                Console.WriteLine();
            }
        }
    }
}
