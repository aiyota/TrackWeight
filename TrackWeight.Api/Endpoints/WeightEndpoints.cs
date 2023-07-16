using Microsoft.AspNetCore.Mvc;
using TrackWeight.Api.Contracts;
using TrackWeight.Api.Mapping;
using TrackWeight.Api.Services;

namespace TrackWeight.Api.Endpoints;

public static class WeightEndpoints
{
    public static void ConfigureWeightRoutes(this WebApplication app)
    {
        app.MapPost("/weight", Create);
        app.MapGet("/weight", Get);
        app.MapPatch("/weight/{id}", Update);
        app.MapDelete("/weight/{id}", Delete);
    }

    private static async Task<IResult> Create(
        [FromBody] WeightRecordCreateRequest request,
        [FromServices] IWeightService weightService,
        [FromServices] IUserService userService,
        HttpContext context)
    {
        var userId = userService.GetUserIdFromContext(context);
        if (userId == Guid.Empty)
            return Results.Unauthorized();

        var weightRecord = await weightService.CreateAsync(userId, request.Weight);
        return Results.Ok(new { weight = weightRecord.DomainToResponse() });
    }

    private static async Task<IResult> Get(
        [FromServices] IWeightService weightService,
        [FromServices] IUserService userService,
        HttpContext context)
    {
        var userId = userService.GetUserIdFromContext(context);
        if (userId == Guid.Empty)
            return Results.Unauthorized();

        var weightRecords = await weightService.GetRecordsAsync(userId);
        return Results.Ok(new { weightRecords = weightRecords.DomainToResponse() });
    }

    private static async Task<IResult> Update(
        int id,
        [FromBody] WeightRecordUpdateRequest request,
        [FromServices] IWeightService weightService,
        [FromServices] IUserService userService,
        HttpContext context)
    {
        var userId = userService.GetUserIdFromContext(context);
        if (userId == Guid.Empty)
            return Results.Unauthorized();

        var weightRecord = await weightService.UpdateAsync(
            userId,
            id,
            request.Weight,
            request.CreatedAt ?? DateTime.Now);
        return Results.Ok(new { weight = weightRecord.DomainToResponse() });
    }

    private static async Task<IResult> Delete(
        int id,
        [FromServices] IWeightService weightService,
        [FromServices] IUserService userService,
        HttpContext context)
    {
        var userId = userService.GetUserIdFromContext(context);
        if (userId == Guid.Empty)
            return Results.Unauthorized();

        var weightRecord = await weightService.DeleteAsync(userId, id);

        return Results.Ok(new { weight = weightRecord.DomainToResponse() });
    }
}
