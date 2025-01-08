﻿using Microsoft.OpenApi.Models;
using StoreHouse360.DTO.Common.Responses.Validation;
using StoreHouse360.Filters;
using System.Text.Json.Serialization;

namespace StoreHouse360
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationControllers(this IServiceCollection services)
        {
            services
                .AddControllers(options => { options.Filters.Add<ExceptionFilter>(); })
                .AddJsonOptions(options => { options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); })
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.InvalidModelStateResponseFactory =
                        actionContext => new BadRequestObjectResult(actionContext.ModelState);
                });

            return services;
        }

        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme."
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme {
                        Reference = new OpenApiReference {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] {}
                }
            });
            });
            return services;
        }

        public static void UseSwaggerMiddlewares(this WebApplication app)
        {
            if (app.Environment.IsDevelopment() || app.Environment.IsStaging() || app.Environment.IsProduction())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "StoreHouse360 API v1");
                    c.RoutePrefix = "swagger";
                });
            }
        }
    }
}
