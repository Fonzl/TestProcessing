using Database;
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
            var allGroups = context.Groups
    .Include(x => x.Direction.Schedule)
    .AsEnumerable();
    var groups = allGroups
    .Where(x => discipline.Schedules
        .Select(s => new { s.DirectionId, s.Cours })
        .Contains(new
        {
            x.Schedule.DirectionId,
            x.Schedule.Cours
        }))
    .ToList();

           


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
                .Include(x => x.Direction)
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
                Cours = group.Cours,
                Direction = group.Direction.Name,


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
                Users = null,
                Cours = group.Cours,
                
                

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
                Direction = context.Directions.First(x => x.Id == dto.Direction),
                Schedule = context.Schedules.First(x => x.DirectionId == dto.Direction && x.Cours == dto.Cours)


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
            group.Cours = dto.Cours;
            group.Direction = context.Directions.First(x => x.Id == dto.Direction);
            group.Schedule = context.Schedules.First(x=>x.DirectionId == dto.Direction && x.Cours == dto.Cours);
            context.Groups.Update(group);
            context.SaveChanges();
        }
        
    }
}
