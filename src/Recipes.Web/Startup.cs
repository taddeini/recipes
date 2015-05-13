using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Framework.ConfigurationModel;
using Microsoft.Framework.DependencyInjection;
using Recipes.Domain.Aggregates;
using Recipes.Domain.Commands;
using Recipes.Domain.Common;
using Recipes.Domain.Repositories;

namespace Recipes
{
    public class Startup
    {        
        public IConfiguration Configuration { get; set; }

        public Startup(IHostingEnvironment env)
        {
            // Setup configuration source
            Configuration = new Configuration()
                .AddJsonFile("config.json")
                .AddJsonFile($"config.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();            
        }
        
        // Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddMvc()    

                // Wire up configuration settings
                .Configure<EventStoreSettings>(Configuration.GetSubKey("eventStore"))
                .Configure<MongoDBSettings>(Configuration.GetSubKey("mongoDb"))

                // Register dependencies
                .AddTransient<IRecipeCommandHandler, RecipeCommandHandler>()
                .AddSingleton<IEventStore<Recipe>, RecipeEventStore>()
                .AddSingleton<IMongoDB<Recipe>, RecipeMongoDB>()
                .AddSingleton<IRepository<Recipe>, RecipeRepository>();          
        }

        // Configure is called after ConfigureServices is called.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
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
