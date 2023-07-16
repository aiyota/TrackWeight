using TrackWeight.Api.Models;

namespace TrackWeight.Api.Persistence;

public interface IWeightRepository
{
    Task<WeightRecord> CreateAsync(
        Guid userId,
        double weight,
        DateTime createdAt);

    Task<WeightRecord> GetRecordAsync(int weightId);

    Task<IEnumerable<WeightRecord>> GetRecordsAsync(Guid userId);

    Task<WeightRecord> UpdateAsync(
        int weightId,
        double? weight,
        DateTime? createdAt);

    Task<WeightRecord> DeleteAsync(int weightId);
}
