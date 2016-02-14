using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Recipes.Domain;
using Recipes.Domain.Commands;
using Recipes.Domain.Common;
using Recipes.Domain.Queries;
using Recipes.Domain.Repositories;
using Serilog;

namespace Recipes
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; set; }

        public Startup(IHostingEnvironment hostEnv)
        {
            // Setup configuration source
            Configuration = new ConfigurationBuilder()
                .AddJsonFile("config.json")
                .AddJsonFile($"config.{hostEnv.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            Log.Logger = new LoggerConfiguration()
                .WriteTo.Trace()
                .CreateLogger();
        }

        // Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            //// Register dependencies
            //.AddSingleton(db => MongoDBFactory.GetDatabase(Configuration))
            //.AddSingleton(logger => Log.Logger)

            //.AddTransient<IRecipeCommandHandler, RecipeCommandHandler>()
            //.AddSingleton<IRepository<RecipeAggregate>, EventStoreRepository<RecipeAggregate>>()
            //.AddSingleton<IQueryProvider<RecipeQuery>, MongoDBRecipeQueryProvider>();
        }

        // Configure is called after ConfigureServices is called.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerfactory)
        {
            loggerfactory.AddSerilog();
            
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseStaticFiles();
        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
