using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebPortal.Services.Core;
using WebPortal.Services.Core.Sensors;
using WebPortal.Services.Core.Switches;
using WebPortal.Services.Core.Users;
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

            ApplicationConfiguration configuration = new ApplicationConfiguration();
            configuration.Load();

            IDbContext dbContext;
            ISensorsService SensorsService;
            ISwitchesService SwitchesService;
            IUsersService UsersService;

            switch (configuration.DataBase)
            {
                case DataBase.MongoDB:
                    dbContext = new MongoDbContext(configuration);
                    SensorsService = new MongoSensorsService(dbContext, configuration);
                    SwitchesService = new MongoSwitchesService(dbContext, configuration);
                    UsersService = new MongoUserService(dbContext, configuration);
                    break;
                case DataBase.SQLite:
                    dbContext = new SQLiteDbContext(configuration);
                    SensorsService = new SQLiteSensorsService(dbContext, configuration);
                    SwitchesService = new SQLiteSwitchesService(dbContext, configuration);
                    UsersService = new SQLiteUsersService(dbContext, configuration);
                    break;
                default:
                    dbContext = new SQLiteDbContext(configuration);
                    SensorsService = new SQLiteSensorsService(dbContext, configuration);
                    SwitchesService = new SQLiteSwitchesService(dbContext, configuration);
                    UsersService = new SQLiteUsersService(dbContext, configuration);
                    break;
            }

            services.AddSingleton(SensorsService);
            services.AddSingleton(SwitchesService);
            services.AddSingleton(UsersService);

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
