using TrackWeight.Api.Models;

namespace TrackWeight.Api.Persistence;

public interface ICalorieRepository
{
    Task<CalorieRecord> CreateAsync(
        Guid userId,
        int calories,
        DateTime createdAt);

    Task<CalorieRecord?> GetRecordAsync(int calorieId);

    Task<IEnumerable<CalorieRecord>> GetRecordsAsync(Guid userId);

    Task<CalorieRecord> UpdateAsync(
        int calorieId,
        int? calories,
        DateTime? createdAt);

    Task<CalorieRecord> DeleteAsync(int calorieId);
}
