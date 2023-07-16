using TrackWeight.Api.Contracts;
using TrackWeight.Api.Models;

namespace TrackWeight.Api.Mapping;

public static class CalorieRecordMappings
{
    public static CalorieRecordResponse DomainToResponse(this CalorieRecord calorieRecord) =>
        new(
            calorieRecord.Id,
            calorieRecord.UserId,
            calorieRecord.Calories,
            calorieRecord.CreatedAt);

    public static IList<CalorieRecordResponse> DomainToResponse(this IList<CalorieRecord> calorieRecords) =>
        calorieRecords
            .Select(r => new CalorieRecordResponse(
                            r.Id,
                            r.UserId,
                            r.Calories,
                            r.CreatedAt))
            .ToList();
}
