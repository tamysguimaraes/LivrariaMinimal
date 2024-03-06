using LivrariaAPI.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System;

namespace LivrariaAPI.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static WebApplicationBuilder AddApiSwagger(this WebApplicationBuilder builder)
        {
            builder.Services.AddSwagger();
            return builder;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ApiLivraria", Version = "v1" });

            });
            return services;
        }
        public static WebApplicationBuilder AddPersistence(
             this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<LivrariaContext>(opt =>
                opt.UseInMemoryDatabase("LivrariaDB"));

            return builder;
        }
    }
}
