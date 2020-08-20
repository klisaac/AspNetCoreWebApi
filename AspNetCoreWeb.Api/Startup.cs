using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using AspNetCoreWebApi.Core.Configuration;
using AspNetCoreWebApi.Infrastructure;
using AspNetCoreWebApi.Infrastructure.Data;
using AspNetCoreWebApi.Application;
using AspNetCoreWeb.Api.Extensions;
using AspNetCoreWeb.Api.Middlewares;

namespace AspNetCoreWeb.Api
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="env"></param>
        /// <param name="configuration"></param>
        public Startup(IWebHostEnvironment env, IConfiguration configuration)
        {
            WebHostEnvironemnt = env;
            Configuration = configuration;
            //AppSettings = configuration.Get<AppSettings>();
        }
        private IWebHostEnvironment WebHostEnvironemnt { get; }
        private IConfiguration Configuration { get; }
        //public AppSettings AppSettings { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCustomMvc();
            services.AddCustomConfiguration();
            services.AddControllers();
            services.AddCustomSwagger();
            services.AddCustomAuthentication(Configuration);
            services.AddApiServices();

            //services.Configure<AppSettings>(Configuration);
            // Add Infrastructure Layer
            services.AddInfrastructureServices();
            // Add Application Layer
            services.AddApplicationServices();
            // Add Miscellaneous
            services.AddHttpContextAccessor();
            ConfigureDatabases(services);
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        public void Configure(IApplicationBuilder app)
        {
            app.UseCors("CorsPolicy");
            if (WebHostEnvironemnt.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
            {
                //app.UseExceptionHandler("/Error");
                //// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                //app.UseHsts();
            }
            app.UseCustomExceptionHandler();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapControllerRoute(
                    name:"default",
                    pattern: "{controller=User}/{action=Authenticate}");
            });
            app.UseCustomSwagger();
            
            //var seed = _configuration.GetValue<bool>("seed");
            //if (seed)
            //    SeedData.Initialize(app);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        protected virtual void ConfigureDatabases(IServiceCollection services)
        {
            if (WebHostEnvironemnt.IsDevelopment() || WebHostEnvironemnt.IsStaging() || WebHostEnvironemnt.IsProduction())
            {
                services.AddDbContext<AppDataContext>(c => c.UseSqlServer(Configuration[Constants.DbConnectionSecret]).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking), ServiceLifetime.Transient);
                //services.AddDbContext<AppDataContext>(c => c.UseSqlServer(Configuration.GetConnectionString(Constants.DbConnectionSecret)).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking), ServiceLifetime.Transient);
            }
            else 
            {
                services.AddDbContext<AppDataContext>(c => c.UseInMemoryDatabase("AspNetCoreWebTest").UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking), ServiceLifetime.Scoped);
            }
        }
    }
}
