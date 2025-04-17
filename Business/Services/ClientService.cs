using Business.Dtos;
using Business.Handlers;
using Business.Mappers;
using Business.Models;
using Data.Repositories;

namespace Business.Services;

public interface IClientService
{
    Task<Client?> CreateClientAsync(AddClientForm formData);
    Task<bool> DeleteClientAsync(int id);
    Task<Client?> GetClientByIdAsync(int id);
    Task<IEnumerable<Client>?> GetClientsAsync();
    Task<Client?> UpdateClientAsync(UpdateClientForm formData);
}

public class ClientService(ClientRepository clientRepository, ICacheHandler<IEnumerable<Client>> cacheHandler) : IClientService
{
    private readonly ClientRepository _clientRepository = clientRepository;
    private readonly ICacheHandler<IEnumerable<Client>> _cacheHandler = cacheHandler;
    private const string _cacheKey = "Clients";

    public async Task<Client?> CreateClientAsync(AddClientForm formData)
    {
        var entity = ClientMapper.ToEntity(formData);
        await _clientRepository.AddAsync(entity);

        var models = await UpdateCacheAsync();
        return models.FirstOrDefault(x => x.ClientName == formData.ClientName);
    }

    public async Task<IEnumerable<Client>?> GetClientsAsync()
    {
        var models = _cacheHandler.GetFromCache(_cacheKey) ?? await UpdateCacheAsync();
        return models;
    }

    public async Task<Client?> GetClientByIdAsync(int id)
    {
        var cached = _cacheHandler.GetFromCache(_cacheKey);

        var match = cached?.FirstOrDefault(x => x.Id == id);
        if (match != null)
            return match;

        var models = await UpdateCacheAsync();
        return models.FirstOrDefault(x => x.Id == id);
    }

    public async Task<Client?> UpdateClientAsync(UpdateClientForm formData)
    {
        var entity = await _clientRepository.GetAsync(x => x.Id == formData.Id);
        if (entity == null)
            return null;

        entity = ClientMapper.ToEntity(formData) ?? entity;
        await _clientRepository.UpdateAsync(entity);

        var models = await UpdateCacheAsync();
        return models.FirstOrDefault(x => x.Id == formData.Id);
    }

    public async Task<bool> DeleteClientAsync(int id)
    {
        var entity = await _clientRepository.GetAsync(x => x.Id == id);
        if (entity == null)
            return false;

        await _clientRepository.DeleteAsync(x => x.Id == id);
        await UpdateCacheAsync();
        return true;
    }

    public async Task<IEnumerable<Client>> UpdateCacheAsync()
    {
        var entities = await _clientRepository.GetAllAsync();
        var models = entities.Select(ClientMapper.ToModel).ToList();

        _cacheHandler.SetCache(_cacheKey, models);
        return models;
    }
}
