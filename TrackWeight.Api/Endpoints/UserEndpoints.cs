using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TrackWeight.Api.Common;
using TrackWeight.Api.Contracts;
using TrackWeight.Api.Infrastructure.Auth;
using TrackWeight.Api.Mapping;
using TrackWeight.Api.Services;

namespace TrackWeight.Api.Endpointsl;



public static class UserEndpoints
{
    public static void ConfigureUserRoutes(this WebApplication app)
    {
        app.MapPost("/userRecord/register", Register);
        app.MapPost("/userRecord/login", Login);
        app.MapPost("/userRecord/logout", Logout);
        app.MapPatch("/userRecord/update", Update);
    }

    [AllowAnonymous]
    private static async Task<IResult> Register(
        [FromBody] UserRegisterRequest request,
        [FromServices] IAuthService authService,
        [FromServices] IUserService userService,
        [FromServices] JwtSettings jwtSettings,
        HttpContext context)
    {
        var userRecord = await userService.RegisterAsync(request.FirstName,
            request.LastName,
            request.Email,
            request.Password);

        var user = userRecord.DomainToResponse();
        var token = authService.GenerateUserToken(userRecord);
        SetTokenAsCookie(context, token, jwtSettings.ExpiryMinutes);

        return Results.Ok(new { user, token });
    }

    [AllowAnonymous]
    private static async Task<IResult> Login(
        [FromBody] UserLoginRequest request,
        [FromServices] IAuthService authService,
        [FromServices] IUserService userService,
        [FromServices] JwtSettings jwtSettings,
        HttpContext context)
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
        
        SetTokenAsCookie(context, token, jwtSettings.ExpiryMinutes);

        return Results.Ok(new { user, token });
    }

    [AllowAnonymous]
    private static IResult Logout(HttpContext context)
    {
        RemoveTokenFromCookies(context);
        return Results.Ok();
    }

    private static async Task<IResult> Update(
        [FromBody] UserUpdateRequest request,
        [FromServices] IUserService userService,
        HttpContext context)
    {
        var userId = GetUserIdFromContext(context);
        if (userId is null)
            return Results.Unauthorized();
        
        var user = await userService.UpdateUserAsync(
            Guid.Parse(userId),
            request.Email,
            request.FirstName,
            request.LastName,
            request.Password);
        return Results.Ok(user.DomainToResponse());
    }

    private static void SetTokenAsCookie(
        HttpContext context,
        string token,
        int expiresInMinutes)
    {
        context.Response.Cookies.Append(
            CustomHeaders.AccessToken, 
            token,
            new CookieOptions()
            {
                HttpOnly = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.Now.AddMinutes(expiresInMinutes),
                Secure = true
            });
    }

    private static void RemoveTokenFromCookies(HttpContext context)
    {
        context.Response.Cookies.Append(
            CustomHeaders.AccessToken, 
            string.Empty, 
            new CookieOptions()
            {
                HttpOnly = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.Now.AddDays(-1),
                Secure = true
            });
    }

    private static string? GetUserIdFromContext(HttpContext context) =>
        context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
}
