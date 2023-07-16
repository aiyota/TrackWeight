namespace TrackWeight.Api.Contracts;

public record WeightRecordUpdateRequest(
    double? Weight = null,
    DateTime? CreatedAt = null);
