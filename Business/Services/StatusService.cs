using Business.Dtos;
using Business.Handlers;
using Business.Mappers;
using Business.Models;
using Data.Repositories;

namespace Business.Services;

public interface IStatusService
{
    Task<Status?> CreateStatusAsync(AddStatusForm formData);
    Task<bool> DeleteStatusAsync(int id);
    Task<Status?> GetStatusByIdAsync(int id);
    Task<IEnumerable<Status>?> GetStatusesAsync();
    Task<Status?> UpdateStatusAsync(UpdateStatusForm formData);
}

public class StatusService(StatusRepository statusRepository, ICacheHandler<IEnumerable<Status>> cacheHandler) : IStatusService
{
    private readonly StatusRepository _statusRepository = statusRepository;
    private readonly ICacheHandler<IEnumerable<Status>> _cacheHandler = cacheHandler;
    private const string _cacheKey = "Statuses";


    public async Task<Status?> CreateStatusAsync(AddStatusForm formData)
    {
        var entity = StatusMapper.ToEntity(formData);
        await _statusRepository.AddAsync(entity);

        var models = await UpdateCacheAsync();
        return models.FirstOrDefault(x => x.StatusName == formData.StatusName);
    }

    public async Task<IEnumerable<Status>?> GetStatusesAsync()
    {
        var models = _cacheHandler.GetFromCache(_cacheKey) ?? await UpdateCacheAsync();
        return models;
    }

    public async Task<Status?> GetStatusByIdAsync(int id)
    {
        var cached = _cacheHandler.GetFromCache(_cacheKey);

        var match = cached?.FirstOrDefault(x => x.Id == id);
        if (match != null)
            return match;

        var models = await UpdateCacheAsync();
        return models.FirstOrDefault(x => x.Id == id);
    }

    public async Task<Status?> UpdateStatusAsync(UpdateStatusForm formData)
    {
        var entity = await _statusRepository.GetAsync(x => x.Id == formData.Id);
        if (entity == null)
            return null;

        entity = StatusMapper.ToEntity(formData) ?? entity;
        await _statusRepository.UpdateAsync(entity);

        var models = await UpdateCacheAsync();
        return models.FirstOrDefault(x => x.Id == formData.Id);
    }

    public async Task<bool> DeleteStatusAsync(int id)
    {
        var entity = await _statusRepository.GetAsync(x => x.Id == id);
        if (entity == null)
            return false;

        await _statusRepository.DeleteAsync(x => x.Id == id);
        await UpdateCacheAsync();
        return true;
    }

    public async Task<IEnumerable<Status>> UpdateCacheAsync()
    {
        var entities = await _statusRepository.GetAllAsync();
        var models = entities.Select(StatusMapper.ToModel).ToList();

        _cacheHandler.SetCache(_cacheKey, models);
        return models;
    }

}
