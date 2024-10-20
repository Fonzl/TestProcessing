using Database;
using DTO.GroupDto;
using DTO.UserDto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

        public DetailsGroupDto GetTestDto(long id)
        {
           var group = context.Groups
                .Include(x => x.Course)
                .Include(x => x.Speciality)
                .Include(x => x.Users)
                .First(x => x.Id == id);
            return new DetailsGroupDto
            {
                Id = group.Id,
                Name = group.Name,
                EndOfTraining = group.EndOfTraining,
                StartDateOfTraining = group.StartDateOfTraining,
                Users = group.Users.Select(x => new UserDto()
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    Surname = x.Surname,
                    LastName = x.LastName,
                }).ToList(),
                Course = new DTO.CourseDto.CourseDto()
                {
                    Id = group.Course.Id,
                },
                Speciality = new DTO.SpecialityDto.SpecialityDto()
                {
                    Id= group.Speciality.Id,
                    Name= group.Speciality.Name,
                }
            };
        }

        public List<GroupDto> GetTestList()
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

        public void Insert(CreateGroupDto dto)
        {
            var group = new Group
            {
                Name = dto.Name,
                EndOfTraining = dto.EndOfTraining,
                StartDateOfTraining = dto.StartDateOfTraining,
                Course = context.Courses.First(x => x.Id == dto.CourseId),
                Speciality = context.Specialities.First(x => x.Id == dto.SpecialityId),
                Users = context.Users.Where(x => dto.Users.Contains(x.Id)).ToList(),

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
            group.Course = context.Courses.First(x => x.Id == dto.CourseId);
            group.Speciality = context.Specialities.First(x => x.Id == dto.SpecialityId);
            group.Users = context.Users.Where(x => dto.Users.Contains(x.Id)).ToList();
            context.Groups.Add(group);
            context.SaveChanges();
        }
        
    }
}
