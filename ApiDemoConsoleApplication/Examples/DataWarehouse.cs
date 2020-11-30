using ApiDemoConsoleApplication.ArmService;
using System;
using System.Linq;

namespace ApiDemoConsoleApplication
{
    class DataWarehouse
    {
        public static void Run()
        {
            var client = PublicClientProvider.GetClient();

            RiskTransfer[] risks;
            int totalPages = 0;

            //These inputs return all records, ignoring folder and filter
            var itemId = -2; // all items
            var recordTypeId = 0; // all record types
            short treeType = 0; // all tree type
            var filterId = -2; // no filter
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

                //insert data into target repository
                /* 
                 * session.CreateSqlQuery("INSERT INTO Impact (id, name) VALUES (:impactId, :impactName)")
                 * .SetValue("impactId", impact.Id)
                 * .SetValue("impactName", risk.Name)
                 * .ExecuteQuery();
                */
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
