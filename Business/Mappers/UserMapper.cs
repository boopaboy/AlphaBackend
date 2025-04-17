using Business.Dtos;
using Business.Models;
using Data.Entities;

namespace Business.Mappers;

public static class UserMapper
{
    public static UserEntity ToEntity(AddUserForm? formData, string? newImageFileName = null)
    {
        if (formData == null) return null!;
        return new UserEntity 
        { 
            FirstName = formData.FirstName,
            LastName = formData.LastName,
            Email = formData.Email,
            UserName = formData.Email,
            StreetName = formData.StreetName,
            PostalCode = formData.PostalCode,
            City = formData.City
        };
    }

    public static UserEntity ToEntity(UpdateUserForm? formData, string? newImageFileName = null)
    {
        if (formData == null) return null!;
        return new UserEntity
        {
            Id = formData.Id,
            ImageFileName = newImageFileName ?? formData.ImageFileName,
            FirstName = formData.FirstName,
            LastName = formData.LastName,
            Email = formData.Email,
            PhoneNumber = formData.PhoneNumber,
            StreetName = formData.StreetName,
            PostalCode = formData.PostalCode,
            City = formData.City
        };
    }

    public static User ToModel(UserEntity? entity)
    {
        if (entity == null) return null!;
        return new User
        {
            Id = entity.Id,
            ImageFileName = entity.ImageFileName,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            Email = entity.Email!,
            PhoneNumber = entity.PhoneNumber,
            StreetName = entity.StreetName,
            PostalCode = entity.PostalCode,
            City = entity.City
        };
    }
}