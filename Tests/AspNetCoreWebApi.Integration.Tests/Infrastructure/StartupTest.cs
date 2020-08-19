using System;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using AspNetCoreWebApi.Core.Configuration;
using AspNetCoreWebApi.Infrastructure.Data;
using AspNetCoreWeb.Api;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace AspNetCoreWebApi.Integration.Tests.Infrastructure
{
    public class StartupTest : Startup
    {
        public StartupTest(IWebHostEnvironment env, IConfiguration configuration) : base (env, configuration)
        {
            WebHostEnvironemnt = env;
            Configuration = configuration;
        }

        private IWebHostEnvironment WebHostEnvironemnt { get; }
        private IConfiguration Configuration { get; }
        protected override void ConfigureDatabases(IServiceCollection services)
        {
            services.AddDbContext<AppDataContext>(c => c.UseInMemoryDatabase("AspNetCoreWebApiTest").UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking), ServiceLifetime.Transient);
            using (var scope = services.BuildServiceProvider().CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var AspNetCoreWebApiContext = scopedServices.GetRequiredService<AppDataContext>();
                AspNetCoreWebApiContext.Database.EnsureCreated();
                AppDataSeed.SeedAsync(AspNetCoreWebApiContext, false).Wait();
            }
        }
    }
}
