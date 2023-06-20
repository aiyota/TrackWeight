using Microsoft.AspNetCore.Mvc;
using TrackWeight.Api.Contracts;
using TrackWeight.Api.Mapping;
using TrackWeight.Api.Services;

namespace TrackWeight.Api.Endpointsl;

public static class UserEndpoints
{
    public static void ConfigureUserRoutes(this WebApplication app)
    {
        app.MapPost("/userRecord/register", Register);
        app.MapPost("/userRecord/login", Login);
        app.MapPatch("/userRecord/update", Update);
    }

    private static async Task<IResult> Register(
        [FromBody] UserRegisterRequest request,
        [FromServices] IAuthService authService,
        [FromServices] IUserService userService)
    {
        var userRecord = await userService.RegisterAsync(request.FirstName,
            request.LastName,
            request.Email,
            request.Password);

        var user = userRecord.DomainToResponse();
        var token = authService.GenerateUserToken(userRecord);
        return Results.Ok(new { user, token });
    }


    private static async Task<IResult> Login(
        [FromBody] UserLoginRequest request,
        [FromServices] IAuthService authService,
        [FromServices] IUserService userService)
    {
        var loginSuccess = await userService.LoginAsync(request.Email, request.Password);
        if (!loginSuccess)
        {
            return Results.Unauthorized();
        }

        var userRecord = await userService.GetByEmailAsync(request.Email);
        if (userRecord is null)
        {
            return Results.NotFound();
        }
        
        var user = userRecord.DomainToResponse();
        var token = authService.GenerateUserToken(userRecord);

        return Results.Ok(new { user, token });
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
