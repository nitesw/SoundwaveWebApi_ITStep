﻿using Core.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SoundwaveWebApi_ITStep.Middlewares;
using System.Text;

namespace SoundwaveWebApi_ITStep.ServiceExtensions
{
    public static class ServiceExtensions
    {
        public static void AddExceptionHandler(this IServiceCollection services)
        {
            services.AddExceptionHandler<HttpExceptionHandler>();
            services.AddProblemDetails();
        }
        public static void AddJWT(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(_ =>
              configuration
                  .GetSection(nameof(JwtOptions))
                  .Get<JwtOptions>()!);

            var jwtOpts = configuration.GetSection(nameof(JwtOptions)).Get<JwtOptions>()!;

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                            .AddJwtBearer(o =>
                            {
                                o.TokenValidationParameters = new TokenValidationParameters
                                {
                                    ValidateIssuer = true,
                                    ValidateAudience = false,
                                    ValidateLifetime = true,
                                    ValidateIssuerSigningKey = true,
                                    ValidIssuer = jwtOpts.Issuer,
                                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOpts.Key)),
                                    ClockSkew = TimeSpan.Zero
                                };
                            });
        }
        public static void AddSwaggerJWT(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });
        }
        public static void AddCorsPolicies(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: "front-end-cors-policy",
                                  policy =>
                                  {
                                      /*policy.WithOrigins("https://localhost:4200",
                                                          "http://localhost:4200",
                                                          "http://localhost:5173",
                                                          "https://localhost:5173");*/
                                      policy.AllowAnyMethod();
                                      policy.AllowAnyHeader();
                                      policy.AllowAnyOrigin();
                                  });
            });
        }
    }
}
