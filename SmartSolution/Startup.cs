using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebPortal.Services.Core;
using WebPortal.Services.Core.Sensors;
using WebPortal.Services.Core.Switches;
using WebPortal.Services.Mongo;
using WebPortal.Services.SQLite;

namespace WebPortal
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            IDbContext dbContext;
            ISensorsService SensorsService;
            ISwitchesService SwitchesService;

            switch (WebPortal.Configuration.DataBase)
            {
                case DataBase.MongoDB:
                    dbContext = new MongoDbContext(WebPortal.Configuration.DatabaseConnection, WebPortal.Configuration.DatabaseName);
                    SensorsService = new MongoSensorsService(dbContext);
                    SwitchesService = new MongoSwitchesService(dbContext);
                    break;
                case DataBase.SQLite:
                    dbContext = new SQLiteDbContext(WebPortal.Configuration.DatabaseConnection, WebPortal.Configuration.DatabaseName);
                    SensorsService = new SQLiteSensorsService(dbContext);
                    SwitchesService = new SQLiteSwitchesService(dbContext);
                    break;
                default:
                    dbContext = new SQLiteDbContext(WebPortal.Configuration.DatabaseConnection, WebPortal.Configuration.DatabaseName);
                    SensorsService = new SQLiteSensorsService(dbContext);
                    SwitchesService = new SQLiteSwitchesService(dbContext);
                    break;
            }

            services.AddSingleton(SensorsService);
            services.AddSingleton(SwitchesService);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
               // app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
