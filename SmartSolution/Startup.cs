using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebPortal.Services;
using WebPortal.Services.Core;
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

            WebPortal.Configuration.DataBase = DataBase.MongoDB;
            ISensorsService SensorsService;
            ISwitchesService SwitchesService;
            switch (WebPortal.Configuration.DataBase)
            {
                case DataBase.MongoDB:
                    SensorsService = new MongoSensorsService();
                    SwitchesService = new MongoSwitchesService();
                    break;
                case DataBase.SQLite:
                    SensorsService = new SQLiteSensorsService();
                    SwitchesService = new SQLiteSwitchesService();
                    break;
                default:
                    SensorsService = new SQLiteSensorsService();
                    SwitchesService = new SQLiteSwitchesService();
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
