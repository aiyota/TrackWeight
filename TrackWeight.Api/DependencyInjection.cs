using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using TrackWeight.Api.Common;
using TrackWeight.Api.Infrastructure.Auth;
using TrackWeight.Api.Persistence;
using TrackWeight.Api.Services;

namespace TrackWeight.Api;

public static class DependencyInjection
{
    public static IServiceCollection ConfigurePresentation(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services.AddSingleton<ProblemDetailsFactory, TrackWeightProblemDetailsFactory>();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Description = "Authorization header using the Bearer token",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new()
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });

        JwtSettings jwtSettings = configuration.GetSection("JwtSettings").Get<JwtSettings>()
                                  ?? throw new NullReferenceException("JwtSettings is null");
        services.AddSingleton(jwtSettings);
        services
            .AddAuthorization(options =>
            {
                options.FallbackPolicy = new AuthorizationPolicyBuilder()
                                            .RequireAuthenticatedUser()
                                            .AddAuthenticationSchemes("Bearer")
                                            .Build();
            });

        services
            .AddAuthentication("Bearer")
            .AddJwtBearer(options =>
            {
                options.Events = AuthEventsHandler.Instance;
                options.TokenValidationParameters = new()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret)),
                };
            });

        return services;
    }

    public static IServiceCollection ConfigureApplication(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services.Configure<JwtSettings>(
            configuration.GetSection(JwtSettings.SectionName));
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IWeightService, WeightService>();
        services.AddScoped<ICalorieService, CalorieService>();

        return services;
    }

    public static IServiceCollection ConfigureInfrastructure(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("Default")));

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IWeightRepository, WeightRepository>();
        services.AddScoped<ICalorieRepository, CalorieRepository>();

        return services;
    }
}
