﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using AspNetCoreWebApi.Core.Configuration;

namespace AspNetCoreWeb.Api.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class CustomAuthenticationExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            // configure strongly typed settings objects
            //var appSettingsSection = _configuration.GetSection("AppSettings").Get<AppSettings>();
            var jwtIssuerOptionsSection = configuration.GetSection(nameof(JwtIssuerOptions));
            var jwtSettings = jwtIssuerOptionsSection.Get<JwtIssuerOptions>();
            //var jwtSettings = new JwtIssuerOptions() {Issuer = configuration[Constants.JwtIssuerSecret], SigningKey = configuration[Constants.JwtSigningKeySecret], Audience = configuration[Constants.JwtAudienceSecret]};

            services.Configure<JwtIssuerOptions>(jwtIssuerOptionsSection);
            //services.Configure<JwtIssuerOptions>(options => { options.Issuer = jwtSettings.Issuer; options.SigningKey = jwtSettings.SigningKey; options.Audience = jwtSettings.Audience; });

            // configure jwt authentication
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
           .AddJwtBearer(x =>
           {
               x.Events = new JwtBearerEvents
               {
                   OnTokenValidated = context =>
                   {
                       //var userService = context.HttpContext.RequestServices.GetRequiredService<IUserService>();
                       //if (!userService.IsValidUser(context.Principal.Identity.Name))
                       if (string.IsNullOrEmpty(context.Principal.Identity.Name))
                       {
                           // return unauthorized if user no longer exists
                           context.Fail("Unauthorized");
                       }
                       return Task.CompletedTask;
                   }
               };

               x.RequireHttpsMetadata = false;
               x.SaveToken = true;
               x.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuerSigningKey = true,
                   ValidateIssuer = true,
                   ValidateAudience = true,
                   ValidateActor = false,
                   ValidateLifetime = true,
                   ValidIssuer = jwtSettings.Issuer,
                   ValidAudience = jwtSettings.Audience,
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.SigningKey))
               };
           });
            return services;
        }
    }
}