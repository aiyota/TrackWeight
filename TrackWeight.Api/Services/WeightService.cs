using TrackWeight.Api.Common.Errors;
using TrackWeight.Api.Models;
using TrackWeight.Api.Persistence;

namespace TrackWeight.Api.Services;

public class WeightService : IWeightService
{
    private readonly IWeightRepository _weightRepository;
    private readonly IUserRepository _userRepository;

    public WeightService(
        IWeightRepository repository,
        IUserRepository userRepository)
    {
        _weightRepository = repository;
        _userRepository = userRepository;
    }

    public async Task<WeightRecord> CreateAsync(
        Guid userId,
        double weight,
        DateTime? createdAt = null)
    {
        if (!(await UserExistsAsync(userId))) 
            throw new UserNotFoundException(null, userId);

        return await _weightRepository.CreateAsync(
                userId, 
                weight, 
                createdAt ?? DateTime.Now);
    }
    public async Task<IList<WeightRecord>> GetRecordsAsync(Guid userId)
    {
        if (!(await UserExistsAsync(userId)))
            throw new UserNotFoundException(null, userId);
        
        return (await _weightRepository.GetRecordsAsync(userId)).ToList();
    }
    public async Task<WeightRecord> UpdateAsync(
        Guid updatedById,
        int weightId,
        double? weight = null,
        DateTime? createdAt = null)
    {
        var record = await _weightRepository.GetRecordAsync(weightId);
        if (record is null)
            throw new WeightRecordNotFoundException(weightId);

        if (record.UserId != updatedById)
            throw new UserNotAuthorizedException();

        return await _weightRepository.UpdateAsync(
            weightId,
            weight,
            createdAt);
    }

    public async Task<WeightRecord> DeleteAsync(
        Guid deletedById,
        int weightId)
    {
        var record = await _weightRepository.GetRecordAsync(weightId);
        if (!(await WeightRecordExistsAsync(weightId)))
            throw new WeightRecordNotFoundException(weightId);

        if (record!.UserId != deletedById)
            throw new UserNotAuthorizedException();

        return await _weightRepository.DeleteAsync(weightId);
    }

    private async Task<bool> UserExistsAsync(Guid userId)
    {
        return await _userRepository.GetByIdAsync(userId) is not null;
    }

    private async Task<bool> WeightRecordExistsAsync(int weightId)
    {
        return await _weightRepository.GetRecordAsync(weightId) is not null;
    }
}
