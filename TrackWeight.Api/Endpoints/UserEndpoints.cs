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
        app.MapPost("/user/login", Login);
        app.MapPatch("/user/update", Update);
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


    private static async Task<IResult> Login(
        [FromBody] UserLoginRequest request,
        [FromServices] IUserService userService)
    {
        var loginSuccess = await userService.LoginAsync(request.Email, request.Password);
        if (!loginSuccess)
        {
            return Results.Unauthorized();
        }

        return Results.Ok();
    }

    private static async Task<IResult> Update(
        [FromBody] UserUpdateRequest request,
        [FromServices] IUserService userService)
    {
        // TODO: implement getting user by id from token
        var user = await userService.UpdateUserAsync(
            request.Id,
            request.UserName,
            request.FirstName,
            request.LastName,
            request.Password);
        return Results.Ok(user.DomainToResponse());
    }
}
