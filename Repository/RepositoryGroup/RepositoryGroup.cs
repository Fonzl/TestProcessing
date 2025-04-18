﻿using Database;
using DTO.GroupDto;
using DTO.UserDto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.RepositoryGroup
{
    public class RepositoryGroup(ApplicationContext context) : IRepositoryGroup
    {
        public void Delete(long id)
        {
            var group = context.Groups.First(g => g.Id == id);
            context.Groups.Remove(group);
            context.SaveChanges();
        }

        public List<GroupDto> GetDisciplineGroupList(long idDiscipline)
        {
            
            var discipline = context.Disciplines
                .Include(x => x.Schedules)
                .First(x => x.Id == idDiscipline);
            var groups = context.Groups
                .Where(x => discipline.Schedules.Contains(x.Schedule)).ToList();
            var listGroup = new List<GroupDto>();
            foreach (var group in groups)
            {
                listGroup.Add(new GroupDto
                {
                    Id = group.Id,
                    Name = group.Name,
                });
            }
            return listGroup;
          
        }

        public DetailsGroupDto GetGroupDto(long id)
        {
           var group = context.Groups
                .Include(x => x.Users)
                .First(x => x.Id == id);
            return new DetailsGroupDto
            {
                Id = group.Id,
                Name = group.Name,
                EndOfTraining = group.EndOfTraining,
                StartDateOfTraining = group.StartDateOfTraining,
                Users = group.Users.Select(x => new StudentUserDto()
                {
                    Id = x.Id,
                  FullName = x.FullName,
                }).ToList(),
                Direction = group.Direction.ToString(),
               
                
            };
        }

        public List<GroupDto> GetGroupList()
        {
            var groups = context.Groups.ToList();
            var listGroup= new List<GroupDto>();
            foreach (var group in groups)
            {
                listGroup.Add(new GroupDto
                {
                    Id = group.Id,
                    Name = group.Name,
                });
            }
            return listGroup;
        }

        public DetailsGroupDto GetGroupUser(long userId)
        {
            var user = context.Users.First(x => x.Id == userId);
            var group = context.Groups
                .Include(x => x.Direction)
                .Where(x => x.Users.Contains(user)).First();

            return new DetailsGroupDto
            {
                Id = group.Id,
                Name = group.Name,
                EndOfTraining = group.EndOfTraining,
                StartDateOfTraining = group.StartDateOfTraining,
                Direction = group.Direction.Name.ToString(),
                Users = null
                

            };
        

        }

        public void Insert(CreateGroupDto dto)
        {
            var group = new Group
            {
                Name = dto.Name,
                EndOfTraining = dto.EndOfTraining,
                StartDateOfTraining = dto.StartDateOfTraining,
                Users = context.Users.Where(x => dto.Users.Contains(x.Id)).ToList(),
                Cours = dto.Cours,
                Schedule = (Schedule)context.Schedules.Where(x => x.Cours == dto.Cours && x.DirectionId == dto.Direction),
               
                

            };
            context.Groups.Add(group);
            context.SaveChanges();
        }

        public void Update(UpdateGroupDto dto)
        {
           var group = context.Groups.First(x=>x.Id == dto.Id);
            group.Name = dto.Name;
            group.StartDateOfTraining = dto.StartDateOfTraining;
            group.EndOfTraining = dto.EndOfTraining;
            group.Users = context.Users.Where(x => dto.Users.Contains(x.Id)).ToList();
            group.Schedule = context.Schedules.FirstOrDefault(x => x.Cours == dto.Cours && x.DirectionId == dto.Direction);
            context.Groups.Update(group);
            context.SaveChanges();
        }
        
    }
}
