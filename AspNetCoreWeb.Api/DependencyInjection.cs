using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using AutoMapper;
using AspNetCoreWebApi.Core;
using AspNetCoreWebApi.Application.Handlers;
using AspNetCoreWebApi.Application.Common.Identity;
using AspNetCoreWeb.Api.Common.Identity;

namespace AspNetCoreWeb.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {
            services.AddScoped<ICurrentUser, CurrentUser>();
            return services;
        }
    }
}
