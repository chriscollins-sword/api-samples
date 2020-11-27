using ApiDemoConsoleApplication.ArmService;
using System;
using System.Linq;

namespace ApiDemoConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = PublicClientProvider.GetClient();

            RiskTransfer[] risks;
            int totalPages = 0;

            var itemId = -2; // all items
            var recordTypeId = 0; // all record types
            short treeType = 0; // all tree type
            var filterId = -2; // no filter
            var pageNumber = 1; // first page
            short recordsPerPage = 5000; // 5000 records per page

            var impacts = client.GetImpactsForFilter(GetArmSoapHeader(), itemId, recordTypeId, treeType, filterId, pageNumber, recordsPerPage, out risks, out totalPages);

            foreach (var impact in impacts)
            {
                var risk = risks.First(r => r.Id == impact.RiskId);
                
                var responses = client.GetResponsesForPlan(GetArmSoapHeader(), impact.PlanId, itemId);

                Console.WriteLine($"{impact.Id}: {risk.Name}");
                foreach (var response in responses)
                {
                    Console.WriteLine($"Response: {response.Id} - {response.Name} {response.AchievedScore}");
                }
                Console.WriteLine("------------------------");
                Console.WriteLine();
            }
            Console.ReadLine();
        }

        static ArmSoapHeader GetArmSoapHeader()
        {
            var header = new ArmSoapHeader();
            header.InstanceID = 1;
            header.BusinessAreaID = 1;
            header.ClientVersion = "4.0.2.0";
            return header;
        }
    }
}

