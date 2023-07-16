namespace TrackWeight.Api.Contracts;

public record WeightRecordCreateRequest(
    double Weight,
    DateTime CreatedAt);