using Business.Dtos;
using Business.Models;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Mappers
{
    public static class MemberMapper
    {
        public static MemberEntity ToEntity(AddMemberForm form)
        {
            return new MemberEntity
            {
                FirstName = form.FirstName,
                LastName = form.LastName,
                Email = form.Email,
                PhoneNumber = form.PhoneNumber,
                Adress = form.Adress,
                PostalCode = form.PostalCode,
                City = form.City,
                JobTitle = form.JobTitle,

            };


        }
        public static MemberEntity ToEntity(UpdateMemberForm form,MemberEntity entity)
        {

            entity.FirstName = form.FirstName;
            entity.LastName = form.LastName;
            entity.PhoneNumber = form.PhoneNumber;
            entity.Email = form.Email;
            entity.Adress = form.Adress;
            entity.PostalCode = form.PostalCode;
            entity.City = form.City;
            entity.JobTitle = form.JobTitle;

            return entity;

            


        }


        public static Member ToModel(MemberEntity? entity)
        {
            if (entity == null) return null!;
            return new Member
            {
                Id = entity.Id,
                ImageUrl = entity.ImageUrl,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Email = entity.Email,
                PhoneNumber = entity.PhoneNumber,
                Adress = entity.Adress,
                PostalCode = entity.PostalCode,
                City = entity.City,
                JobTitle = entity.JobTitle,
            };}
        }


    }
