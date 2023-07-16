using Microsoft.EntityFrameworkCore;
using TrackWeight.Api.Models;

namespace TrackWeight.Api.Persistence;

public class CalorieRepository : ICalorieRepository
{
    private readonly AppDbContext _dbContext;

    public CalorieRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<CalorieRecord> CreateAsync(
        Guid userId,
        int calories,
        DateTime createdAt)
    {
        var record = new CalorieRecord
        {
            UserId = userId,
            Calories = calories,
            CreatedAt = createdAt
        };

        await _dbContext.CalorieRecords.AddAsync(record);
        await _dbContext.SaveChangesAsync();

        return record;
    }

    public async Task<CalorieRecord?> GetRecordAsync(int calorieId)
    {
        return await _dbContext.CalorieRecords.FindAsync(calorieId);
    }

    public async Task<IEnumerable<CalorieRecord>> GetRecordsAsync(Guid userId)
    {
        return await _dbContext.CalorieRecords
                        .Where(r => r.UserId == userId)
                        .ToListAsync();
    }


    public async Task<CalorieRecord> UpdateAsync(
        int calorieId,
        int? calories,
        DateTime? createdAt)
    {
        var record = await _dbContext.CalorieRecords.FindAsync(calorieId)
                        ?? throw new InvalidOperationException($"Record with ID {calorieId} not found.");

        if (calories is not null)
            record.Calories = calories.Value;

        if (createdAt is not null)
            record.CreatedAt = createdAt.Value;

        await _dbContext.SaveChangesAsync();

        return record;
    }

    public async Task<CalorieRecord> DeleteAsync(int calorieId)
    {
        var record = await _dbContext.CalorieRecords.FindAsync(calorieId)
                     ?? throw new InvalidOperationException($"Record with ID {calorieId} not found.");

        _dbContext.CalorieRecords.Remove(record);
        await _dbContext.SaveChangesAsync();

        return record;
    }
}
