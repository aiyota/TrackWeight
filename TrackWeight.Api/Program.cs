using TrackWeight.Api;
using TrackWeight.Api.Endpoints;
using TrackWeight.Api.Endpointsl;
using TrackWeight.Api.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .ConfigureInfrastructure(builder.Configuration)
    .ConfigureApplication(builder.Configuration)
    .ConfigurePresentation(builder.Configuration);


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseHttpsRedirection();


app.ConfigureUserRoutes();
app.ConfigureWeightRoutes();
app.ConfigureCaloriesRoutes();

app.UseAuthorization();
app.UseAuthentication();

app.Run();
