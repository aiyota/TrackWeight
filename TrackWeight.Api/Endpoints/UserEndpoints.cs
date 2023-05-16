using Microsoft.AspNetCore.Mvc;
using TrackWeight.Api.Contracts;
using TrackWeight.Api.Mapping;
using TrackWeight.Api.Services;

namespace TrackWeight.Api.Endpointsl;

public static class UserEndpoints
{
    public static void ConfigureUserRoutes(this WebApplication app)
    {
        app.MapPost("/user/register", Register);
    }

    private static async Task<IResult> Register(
        [FromBody] UserRegisterRequest request,
        [FromServices] IUserService userService)
    {
        var user = await userService.RegisterAsync(request.FirstName,
            request.LastName,
            request.Email,
            request.Password);

        return Results.Ok(user.DomainToResponse());
    }
}
