using TrackWeight.Api.Models;

namespace TrackWeight.Api.Services;

public interface IWeightService
{
    Task<WeightRecord> CreateAsync(
        Guid userId,
        double weight,
        DateTime? createdAt = null);

    Task<IList<WeightRecord>> GetRecordsAsync(Guid userId);

    Task<WeightRecord> UpdateAsync(
        Guid updatedById,
        int weightId,
        double? weight,
        DateTime? createdAt);

    Task<WeightRecord> DeleteAsync(Guid deletedById, int weightId);
}
