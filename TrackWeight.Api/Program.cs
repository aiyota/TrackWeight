using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using TrackWeight.Api.Common;
using TrackWeight.Api.Endpointsl;
using TrackWeight.Api.Infrastructure.Auth;
using TrackWeight.Api.Middleware;
using TrackWeight.Api.Persistence;
using TrackWeight.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ProblemDetailsFactory, TrackWeightProblemDetailsFactory>();

builder.Services.Configure<JwtSettings>(
    builder.Configuration.GetSection(JwtSettings.SectionName));

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
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

JwtSettings jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>() 
                          ?? throw new NullReferenceException("JwtSettings is null");

builder.Services
    .AddAuthorization(options =>
    {
        options.FallbackPolicy = new AuthorizationPolicyBuilder()
                                    .RequireAuthenticatedUser()
                                    .AddAuthenticationSchemes("Bearer")
                                    .Build();
    });

builder.Services
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



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseHttpsRedirection();


app.ConfigureUserRoutes();

app.UseAuthorization();
app.UseAuthentication();

app.Run();
