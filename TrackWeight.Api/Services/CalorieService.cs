using TrackWeight.Api.Common.Errors;
using TrackWeight.Api.Models;
using TrackWeight.Api.Persistence;

namespace TrackWeight.Api.Services;

public class CalorieService : ICalorieService
{
    private readonly ICalorieRepository _calorieRepository;
    private readonly IUserRepository _userRepository;

    public CalorieService(
        ICalorieRepository repository,
        IUserRepository userRepository)
    {
        _calorieRepository = repository;
        _userRepository = userRepository;
    }

    public async Task<CalorieRecord> CreateAsync(
        Guid userId,
        int calories,
        DateTime? createdAt = null)
    {
        if (!(await UserExistsAsync(userId)))
            throw new UserNotFoundException(null, userId);

        return await _calorieRepository.CreateAsync(
                userId,
                calories,
                createdAt ?? DateTime.Now);
    }


    public async Task<IList<CalorieRecord>> GetRecordsAsync(Guid userId)
    {
        if (!(await UserExistsAsync(userId)))
            throw new UserNotFoundException(null, userId);

        return (await _calorieRepository.GetRecordsAsync(userId)).ToList();
    }

    public async Task<CalorieRecord> UpdateAsync(
        Guid updatedById,
        int calorieId,
        int? calories,
        DateTime? createdAt)
    {
        var record = await _calorieRepository.GetRecordAsync(calorieId);
        if (record is null)
            throw new CalorieRecordNotFoundException(calorieId);

        if (record.UserId != updatedById)
            throw new UserNotAuthorizedException();

        return await _calorieRepository.UpdateAsync(
            calorieId,
            calories,
            createdAt);
    }

    public async Task<CalorieRecord> DeleteAsync(
        Guid deletedById,
        int calorieId)
    {
        var record = await _calorieRepository.GetRecordAsync(calorieId);
        if (!(await CalorieRecordExistsAsync(calorieId)))
            throw new WeightRecordNotFoundException(calorieId);

        if (record!.UserId != deletedById)
            throw new UserNotAuthorizedException();

        return await _calorieRepository.DeleteAsync(calorieId);
    }

    private async Task<bool> UserExistsAsync(Guid userId)
    {
        return await _userRepository.GetByIdAsync(userId) is not null;
    }

    private async Task<bool> CalorieRecordExistsAsync(int weightId)
    {
        return await _calorieRepository.GetRecordAsync(weightId) is not null;
    }
}
