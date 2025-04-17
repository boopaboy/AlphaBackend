using Business.Dtos;
using Business.Handlers;
using Business.Mappers;
using Business.Models;
using Data.Repositories;
namespace Business.Services;

public interface IProjectService
{
    Task<Project?> CreateProjectAsync(AddProjectForm formData);
    Task<bool> DeleteProjectAsync(string id);
    Task<Project?> GetProjectByIdAsync(string id);
    Task<IEnumerable<Project>?> GetProjectsAsync();
    Task<IEnumerable<Project>?> GetProjectsAsync(string expression);


    Task<Project?> UpdateProjectAsync(UpdateProjectForm formData);
}

public class ProjectService(ProjectRepository projectRepository, ICacheHandler<IEnumerable<Project>> cacheHandler, IImageService imageService, IUserService userService) : IProjectService
{
    private readonly ProjectRepository _projectRepository = projectRepository;
    private readonly ICacheHandler<IEnumerable<Project>> _cacheHandler = cacheHandler;
    private readonly IImageService _imageService = imageService;
    private readonly IUserService _userService = userService;
    private const string _cacheKey = "Projects";

    public async Task<Project?> CreateProjectAsync(AddProjectForm formData)
    {
        string? imageUrl = null;
        string? userId = _userService.GetUserByEmailAsync(formData.UserEmail).Result?.Id;

        if (formData.ImageFile != null)
        {
            imageUrl = await _imageService.UploadImageAsync(formData.ImageFile);
        }

        var entity = ProjectMapper.ToEntity(formData, imageUrl, userId);

        await _projectRepository.AddAsync(entity);

        var models = await UpdateCacheAsync();
        return models.FirstOrDefault(x => x.ProjectName == formData.ProjectName);
    }

    public async Task<IEnumerable<Project>?> GetProjectsAsync()
    {
        var models = _cacheHandler.GetFromCache(_cacheKey) ?? await UpdateCacheAsync();
        return models;
    }

    public async Task<IEnumerable<Project>?> GetProjectsAsync(string expression)
    {
        
        if (expression == "asc")
        {
          return _cacheHandler.GetFromCache(_cacheKey,x => x.OrderBy(x => x.Created)) ?? await UpdateCacheAsync();
        }
        else
        {
            return _cacheHandler.GetFromCache(_cacheKey, x => x.OrderByDescending(x => x.Created)) ?? await UpdateCacheAsync();

        }
    }

    public async Task<Project?> GetProjectByIdAsync(string id)
    {
        var cached = _cacheHandler.GetFromCache(_cacheKey);

        var match = cached?.FirstOrDefault(x => x.Id == id);
        if (match != null)
            return match;

        var models = await UpdateCacheAsync();
        return models.FirstOrDefault(x => x.Id == id);
    }

    public async Task<Project?> UpdateProjectAsync(UpdateProjectForm formData)
    {
        var entity = await _projectRepository.GetAsync(x => x.Id == formData.Id);

        if (entity == null)
            return null;

        var newEntity = ProjectMapper.ToUpdateEntity(entity, formData);

        if (await _projectRepository.UpdateAsync(newEntity))
        {
            var models = await UpdateCacheAsync();
            return models.FirstOrDefault(x => x.Id == formData.Id);

        }

        return null;


    }

    public async Task<bool> DeleteProjectAsync(string id)
    {
        var entity = await _projectRepository.GetAsync(x => x.Id == id);
        if (entity == null)
            return false;

        await _projectRepository.DeleteAsync(x => x.Id == id);
        await UpdateCacheAsync();
        return true;
    }

    public async Task<IEnumerable<Project>> UpdateCacheAsync()
    {
        var entities = await _projectRepository.GetAllAsync();
        var models = entities.Select(ProjectMapper.ToModel).ToList();
        _cacheHandler.SetCache(_cacheKey, models);
        return models;
    }

}
