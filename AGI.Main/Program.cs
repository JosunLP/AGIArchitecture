
using System;
using System.Threading.Tasks;
using AGI.DataManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AGI.Main
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Setup the dependency injection
            var serviceProvider = new ServiceCollection()
                .AddDbContext<KnowledgeGraphContext>(options => options.UseInMemoryDatabase("KnowledgeGraphDB"))
                .AddScoped<DataManager>()
                .BuildServiceProvider();

            // Get an instance of the DataManager
            var dataManager = serviceProvider.GetService<DataManager>() ?? throw new InvalidOperationException("DataManager service is not available.");
            await dataManager.AddKnowledgeNodeAsync("AI Basics", "Introduction to AI concepts");
            await dataManager.AddKnowledgeNodeAsync("Machine Learning", "Core concepts of machine learning");

            var knowledgeNodes = await dataManager.GetKnowledgeNodesAsync();
            Console.WriteLine("Knowledge Nodes:");
            foreach (var node in knowledgeNodes)
            {
                Console.WriteLine($"- {node.Name}: {node.Description}");
            }

            // Test adding and retrieving Experiences
            await dataManager.AddExperienceAsync("Completed AI Basics", DateTime.Now.AddDays(-1));
            await dataManager.AddExperienceAsync("Started Machine Learning", DateTime.Now);

            var experiences = await dataManager.GetExperiencesAsync();
            Console.WriteLine("\nExperiences:");
            foreach (var experience in experiences)
            {
                Console.WriteLine($"- Event: {experience.Event} on {experience.Timestamp}");
            }
        }
    }
}
