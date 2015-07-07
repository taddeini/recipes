using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Framework.Configuration;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.Logging;
using Recipes.Domain.Commands;
using Recipes.Domain.Common;
using Recipes.Domain.Queries;
using Serilog;

namespace Recipes
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        public Startup(IHostingEnvironment env)
        {
            // Setup configuration source
            Configuration = new ConfigurationBuilder()
                .AddJsonFile("config.json")
                .AddJsonFile($"config.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            Log.Logger = new LoggerConfiguration()
                .WriteTo.Trace()
                .CreateLogger();
        }

        // Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddMvc()

                // Register dependencies
                .AddSingleton(db => MongoDBFactory.GetDatabase(Configuration))
                .AddSingleton(logger => Log.Logger)

                .AddTransient<IRecipeCommandHandler, RecipeCommandHandler>()
                .AddSingleton<IQueryProvider<RecipeQuery>, MongoDBRecipeQueryProvider>();                
        }

        // Configure is called after ConfigureServices is called.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerfactory)
        {
            loggerfactory.AddSerilog();

            app.UseStaticFiles();

            // Add MVC to the request pipeline.
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller = "Home", action = "Index" });
            });
        }
    }
}
