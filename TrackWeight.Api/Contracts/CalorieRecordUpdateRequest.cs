namespace TrackWeight.Api.Contracts;

public record CalorieRecordUpdateRequest(
    int? Calories = null,
    DateTime? CreatedAt = null);