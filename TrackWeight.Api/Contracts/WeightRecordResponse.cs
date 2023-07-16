namespace TrackWeight.Api.Contracts;

public record WeightRecordResponse(
    int Id,
    Guid UserId,
    double Weight,
    DateTime CreatedAt);
