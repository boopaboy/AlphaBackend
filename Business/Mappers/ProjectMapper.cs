using Business.Dtos;
using Business.Models;
using Business.Services;
using Data.Entities;
using Microsoft.SqlServer.Server;

namespace Business.Mappers;

public static class ProjectMapper
{
    

    public static ProjectEntity ToEntity(AddProjectForm? formData, string? newImageFileName = null,string userId = null)
    {


        if (formData == null) return null!;
        return new ProjectEntity
        {   

            ImageFileName = newImageFileName ?? null,
            ProjectName = formData.ProjectName,
            ClientId = formData.ClientId,
            Description = formData.Description,
            StartDate = formData.StartDate,
            EndDate = formData.EndDate,
            Budget = formData.Budget,
            UserId = userId,
            StatusId = 1
        };
    }


    public static ProjectEntity ToUpdateEntity(ProjectEntity? entity, UpdateProjectForm? formData)
    {
        if (entity == null) return null!;

            entity.ProjectName = formData.ProjectName;
            entity.ClientId = formData.ClientId;
            entity.Description = formData.Description;
            entity.StartDate = formData.StartDate;
            entity.EndDate = formData.EndDate;
            entity.Budget = formData.Budget;
            entity.StatusId = formData.StatusId;
            
        return entity;


    }




    public static Project ToModel(ProjectEntity? entity)
    {
        if (entity == null) return null!;
        return new Project
        {
            Id = entity.Id,
            ImageFileName = entity.ImageFileName,
            ProjectName = entity.ProjectName,
            Client = ClientMapper.ToModel(entity.Client),
            Description = entity.Description,
            StartDate = entity.StartDate,
            EndDate = entity.EndDate,
            Budget = entity.Budget,
            User = UserMapper.ToModel(entity.User),
            Status = StatusMapper.ToModel(entity.Status),
        };
    }
}
