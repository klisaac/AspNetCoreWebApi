using System.Reflection;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using AspNetCoreWebApi.Application.Common.Mappings;
using AspNetCoreWebApi.Application.Handlers;

namespace AspNetCoreWebApi.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddMediatR(typeof(UpdateProductCommandHandler).GetTypeInfo().Assembly);
            return services;
        }
    }
}
