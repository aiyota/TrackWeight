using TrackWeight.Api.Contracts;
using TrackWeight.Api.Models;

namespace TrackWeight.Api.Mapping;

public static class WeightRecordMappings
{
    public static WeightRecordResponse DomainToResponse(this WeightRecord weightRecord) =>
        new(
            weightRecord.Id,
            weightRecord.UserId,
            weightRecord.Weight,
            weightRecord.CreatedAt);

    public static IList<WeightRecordResponse> DomainToResponse(this IList<WeightRecord> weightRecords) =>
        weightRecords
            .Select(r => new WeightRecordResponse(
                            r.Id,
                            r.UserId,
                            r.Weight,
                            r.CreatedAt))
            .ToList();
}
