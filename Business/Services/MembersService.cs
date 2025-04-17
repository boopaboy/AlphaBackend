using Business.Dtos;
using Business.Handlers;
using Business.Mappers;
using Business.Models;
using Business.Services;
using Data.Entities;
using Data.Repositories;
using System.Linq.Expressions;

public interface IMembersService
{
    Task<IEnumerable<Member>> GetAllMembersAsync();
    Task<Member?> CreateMemberAsync(AddMemberForm form);
    Task<Member?> GetMemberByIdAsync(int id);
    Task<Member?> UpdateMemberAsync(UpdateMemberForm formData);
    Task<bool> DeleteMemberAsync(int id);
}

public class MembersService(MembersRepository membersRepository, ICacheHandler<IEnumerable<Member>> cacheHandler, IImageService imageService) : IMembersService
{
    private readonly MembersRepository _membersRepository = membersRepository;
    private readonly ICacheHandler<IEnumerable<Member>> _cacheHandler = cacheHandler;
    private readonly IImageService _imageService = imageService;

    private readonly string _cacheKey = "Members";

    public async Task<IEnumerable<Member>> GetAllMembersAsync()
    {
        var members = _cacheHandler.GetFromCache(_cacheKey) ?? await UpdateCacheAsync();
        return members;
    }

    public async Task<Member?> CreateMemberAsync(AddMemberForm form)
    {
        if (await ExistsAsync(x => x.Email == form.Email))
            return null;

        string? imageUrl = null;
        if (form.ImageFile != null)
        {
             imageUrl = await _imageService.UploadImageAsync(form.ImageFile);
        }

        var entity = MemberMapper.ToEntity(form);
        entity.ImageUrl = imageUrl;
        var member = await _membersRepository.AddAsync(entity);
        var models = await UpdateCacheAsync();
        return models.FirstOrDefault(x => x.Email == form.Email);
    }

  

    public async Task<Member?> GetMemberByIdAsync(int id)
    {
        var cached = _cacheHandler.GetFromCache(_cacheKey);
        var match = cached?.FirstOrDefault(x => x.Id == id);
        if (match != null)
        return match;

        var models = await UpdateCacheAsync();
        return models.FirstOrDefault(x => x.Id == id);
    }

    public async Task<Member?> UpdateMemberAsync(UpdateMemberForm formData)
    {
        var entity = await _membersRepository.GetAsync(x => x.Id == formData.Id);
        if (entity == null)
            return null;

        entity = MemberMapper.ToEntity(formData, entity);
        var member = await _membersRepository.UpdateAsync(entity);
        var models = await UpdateCacheAsync();
        return models.FirstOrDefault(x => x.Id == formData.Id);
    }


    public async Task<bool> DeleteMemberAsync(int id)
    {
        await _membersRepository.DeleteAsync(x => x.Id == id);
        var models = await UpdateCacheAsync();
        return true;
    }


    public async Task<bool> ExistsAsync(Expression<Func<MemberEntity, bool>> expression)
    {
        var result = await _membersRepository.ExistsAsync(expression);
        return result;
    }
    
    public async Task<IEnumerable<Member>> UpdateCacheAsync()
    {
        var entities = await _membersRepository.GetAllAsync();
        var models = entities.Select(MemberMapper.ToModel).ToList();
        _cacheHandler.SetCache(_cacheKey, models);
        return models;
    }



}