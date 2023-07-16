namespace TrackWeight.Api.Contracts;

public record CalorieRecordResponse(
    int Id,
    Guid UserId,
    int Calories,
    DateTime CreatedAt);
