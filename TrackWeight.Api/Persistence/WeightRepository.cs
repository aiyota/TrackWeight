using Microsoft.EntityFrameworkCore;
using TrackWeight.Api.Models;

namespace TrackWeight.Api.Persistence;

public class WeightRepository : IWeightRepository
{
    private readonly AppDbContext _dbContext;

    public WeightRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<WeightRecord> CreateAsync(
        Guid userId,
        double weight,
        DateTime createdAt)
    {
        var record = new WeightRecord
        {
            UserId = userId,
            Weight = weight,
            CreatedAt = createdAt,
        };

        await _dbContext.WeightRecords.AddAsync(record);
        await _dbContext.SaveChangesAsync();

        return record;
    }

    public async Task<IEnumerable<WeightRecord>> GetRecordsAsync(Guid userId)
    {
        return await _dbContext.WeightRecords
                        .Where(r => r.UserId == userId)
                        .ToListAsync();
    }

    public async Task<WeightRecord?> GetRecordAsync(int weightId)
    {
        return await _dbContext.WeightRecords.FindAsync(weightId);
    }

    public async Task<WeightRecord> UpdateAsync(
        int weightId,
        double? weight,
        DateTime? createdAt)
    {
        var record = await _dbContext.WeightRecords.FindAsync(weightId) 
                        ?? throw new InvalidOperationException($"Record with ID {weightId} not found.");

        if (weight is not null)
            record.Weight = weight.Value; 
        
        if (createdAt is not null)
            record.CreatedAt = createdAt.Value;

        await _dbContext.SaveChangesAsync();

        return record;
    }

    public async Task<WeightRecord> DeleteAsync(int weightId)
    {
        var record = await _dbContext.WeightRecords.FindAsync(weightId)
                     ?? throw new InvalidOperationException($"Record with ID {weightId} not found.");

        _dbContext.WeightRecords.Remove(record);
        await _dbContext.SaveChangesAsync();

        return record;
    }
}
