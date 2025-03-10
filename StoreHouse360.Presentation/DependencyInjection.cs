﻿using Microsoft.OpenApi.Models;
using StoreHouse360.Application.Services.Identity;
using StoreHouse360.DTO.Common.Responses.Validation;
using StoreHouse360.Filters;
using StoreHouse360.Services;
using Swashbuckle.AspNetCore.SwaggerUI;
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
                    Description = "Enter the token in this format: 'Bearer YOUR_TOKEN_HERE'. Example: 'Bearer abc123xyz'. \n JWT Authorization header using the Bearer scheme."
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme {
                            Reference = new OpenApiReference {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
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
                    c.DocExpansion(DocExpansion.None);
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "StoreHouse360 API v1");
                    c.RoutePrefix = "swagger";
                });
            }
        }

        public static IServiceCollection AddCurrentUserService(this IServiceCollection services)
        {
            return services.AddScoped<ICurrentUserService, ApiCurrentUserService>();
        }
    }
}
