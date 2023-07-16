using Microsoft.AspNetCore.Mvc;
using TrackWeight.Api.Contracts;
using TrackWeight.Api.Mapping;
using TrackWeight.Api.Services;

namespace TrackWeight.Api.Endpoints;

public static class CalorieEndpoints
{
    public static void ConfigureCaloriesRoutes(this WebApplication app)
    {
        app.MapGet("/calories", Get);
        app.MapPost("/calories", Create);
        app.MapPut("/calories/{id}", Update);
        app.MapDelete("/calories/{id}", Delete);
    }

    private static async Task<IResult> Create(
    [FromBody] CalorieRecordCreateRequest request,
    [FromServices] ICalorieService calorieService,
    [FromServices] IUserService userService,
    HttpContext context)
    {
        var userId = userService.GetUserIdFromContext(context);
        if (userId == Guid.Empty)
            return Results.Unauthorized();

        var caloriesRecord = await calorieService.CreateAsync(userId, request.Calories);
        return Results.Ok(new { caloriesRecords = caloriesRecord.DomainToResponse() });
    }

    private static async Task<IResult> Get(
         [FromServices] ICalorieService calorieService,
         [FromServices] IUserService userService,
         HttpContext context)
    {
        var userId = userService.GetUserIdFromContext(context);
        if (userId == Guid.Empty)
            return Results.Unauthorized();

        var caloriesRecord = await calorieService.GetRecordsAsync(userId);
        return Results.Ok(new { caloriesRecords = caloriesRecord.DomainToResponse() });
    }

    private static async Task<IResult> Update(
        int id,
        [FromBody] CalorieRecordUpdateRequest request,
        [FromServices] ICalorieService calorieService,
        [FromServices] IUserService userService,
        HttpContext context)
    {
        var userId = userService.GetUserIdFromContext(context);
        if (userId == Guid.Empty)
            return Results.Unauthorized();

        var caloriesRecords = await calorieService.UpdateAsync(
            userId,
            id,
            request.Calories,
            request.CreatedAt ?? DateTime.Now);
        return Results.Ok(new { calories = caloriesRecords.DomainToResponse() });
    }

    private static async Task<IResult> Delete(
        int id,
        [FromServices] ICalorieService calorieService,
        [FromServices] IUserService userService,
        HttpContext context)
    {
        var userId = userService.GetUserIdFromContext(context);
        if (userId == Guid.Empty)
            return Results.Unauthorized();

        var calorieRecord = await calorieService.DeleteAsync(userId, id);

        return Results.Ok(new { calories = calorieRecord.DomainToResponse() });
    }
}
