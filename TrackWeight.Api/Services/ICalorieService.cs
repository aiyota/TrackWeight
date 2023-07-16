using TrackWeight.Api.Models;

namespace TrackWeight.Api.Services;

public interface ICalorieService
{
    Task<CalorieRecord> CreateAsync(
    Guid userId,
    int calories,
    DateTime? createdAt = null);

    Task<IList<CalorieRecord>> GetRecordsAsync(Guid userId);

    Task<CalorieRecord> UpdateAsync(
        Guid updatedById,
        int calorieId,
        int? calories,
        DateTime? createdAt);

    Task<CalorieRecord> DeleteAsync(Guid deletedById, int calorieId);
}
