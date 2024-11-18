using System;
using AGI.Adaptation;
using AGI.APIIntegration;
using AGI.DataManagement;
using AGI.WebInterface;

namespace AGI.Main
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to AGI.Main - Your Command Line Interface for AGI Management.");

            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("\nChoose an option:");
                Console.WriteLine("1. Adaptation Module");
                Console.WriteLine("2. API Integration");
                Console.WriteLine("3. Data Management");
                Console.WriteLine("4. Web Interface");
                Console.WriteLine("5. Exit");
                Console.Write("Enter your choice: ");

                string choice = Console.ReadLine() ?? string.Empty;
                switch (choice)
                {
                    case "1":
                        AdaptationModule();
                        break;
                    case "2":
                        APIIntegrationModule();
                        break;
                    case "3":
                        DataManagementModule();
                        break;
                    case "4":
                        WebInterfaceModule();
                        break;
                    case "5":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        static void AdaptationModule()
        {
            Console.WriteLine("\nAdaptation Module Selected.");
            // Code to initiate and manage adaptation processes using AGI.Adaptation
        }

        static void APIIntegrationModule()
        {
            Console.WriteLine("\nAPI Integration Module Selected.");
            using var httpClient = new HttpClient();
            ExtendedAPIIntegrationService apiService = new(httpClient);
            apiService.RunIntegration();
        }

        static void DataManagementModule()
        {
            Console.WriteLine("\nData Management Module Selected.");
            // Code to handle data management using AGI.DataManagement
        }

        static void WebInterfaceModule()
        {
            Console.WriteLine("\nWeb Interface Module Selected.");
            // Code to interact with AGI.WebInterface
        }
    }
}
