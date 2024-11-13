
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AGI.DataManagement;
using AGI.MLServices;
using AGI.NLPServices;
using AGI.ReasoningEngine;
using AGI.PlanningEngine;
using AGI.EthicsAndSafety;
using AGI.SelfMonitoring;
using AGI.Adaptation;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace AGI.WebInterface
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Configure database context (in-memory for demo purposes)
            services.AddDbContext<KnowledgeGraphContext>(options => options.UseInMemoryDatabase("KnowledgeGraphDB"));

            // Register AGI services
            services.AddScoped<DataManager>();
            services.AddSingleton<MachineLearningService>();
            services.AddSingleton<NLPService>();
            services.AddSingleton<ReasoningEngine.ReasoningEngine>();
            services.AddSingleton<PlanningEngine.PlanningEngine>();
            services.AddSingleton<EthicsAndSafetyModule>();
            services.AddSingleton<SelfMonitoringModule>();
            services.AddSingleton<AdaptationModule>();

            services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "AGI API", Version = "v1" });
                });

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "AGI API V1");
                    c.RoutePrefix = string.Empty; // Sets Swagger UI at the root of the app
                });
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
