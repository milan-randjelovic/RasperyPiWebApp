using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
            IDbContext dbContext;
            ISensorsService SensorsService;
            ISwitchesService SwitchesService;
            IUsersService UsersService;
            ApplicationConfiguration configuration = new ApplicationConfiguration();
            configuration.Load();

            //configure autentication
            services.AddAuthentication(options =>
            {
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddCookie(options =>
            {
                options.LoginPath = new PathString("/Users/SignIn");
                options.LogoutPath = new PathString("/Users/SignIn");
            });

            //configure mvc
            services.AddMvc();

            //configure database context
            switch (configuration.DataBase)
            {
                case DataBase.MongoDB:
                    dbContext = new MongoDbContext(configuration);
                    SensorsService = new MongoSensorsService((IMongoDbContext)dbContext, configuration);
                    SwitchesService = new MongoSwitchesService((IMongoDbContext)dbContext, configuration);
                    UsersService = new MongoUserService((IMongoDbContext)dbContext, configuration);
                    break;
                case DataBase.SQLite:
                    dbContext = new SQLiteDbContext(configuration);
                    SensorsService = new SQLiteSensorsService((ISQLiteDbContext)dbContext, configuration);
                    SwitchesService = new SQLiteSwitchesService((ISQLiteDbContext)dbContext, configuration);
                    UsersService = new SQLiteUsersService((ISQLiteDbContext)dbContext, configuration);
                    break;
                default:
                    dbContext = new SQLiteDbContext(configuration);
                    SensorsService = new SQLiteSensorsService((ISQLiteDbContext)dbContext, configuration);
                    SwitchesService = new SQLiteSwitchesService((ISQLiteDbContext)dbContext, configuration);
                    UsersService = new SQLiteUsersService((ISQLiteDbContext)dbContext, configuration);
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
            }

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
