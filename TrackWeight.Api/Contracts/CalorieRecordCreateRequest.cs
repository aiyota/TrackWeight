namespace TrackWeight.Api.Contracts;

public record CalorieRecordCreateRequest(
    int Calories,
    DateTime CreatedAt);