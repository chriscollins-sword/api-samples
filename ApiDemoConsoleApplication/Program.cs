using ApiDemoConsoleApplication.ArmService;
using System;
using System.Linq;

namespace ApiDemoConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            DataWarehouse.Run();

            Kpi.Run();

            UpdateResource.Run();

            Console.ReadLine();
        }


    }
}

